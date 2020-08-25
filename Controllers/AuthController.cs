using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NewsApp.Dtos.User;
using NewsApp.Models;
using NewsApp.Repository.IRepos;
using Microsoft.Extensions.Configuration;

namespace NewsApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepo repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {

            userForRegisterDto.email = userForRegisterDto.email.ToLower();

            if (await _repo.UserExists(userForRegisterDto.email))
                return BadRequest("username is already exists");

            var userToCreate = new User
            {
                email = userForRegisterDto.email
            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.password);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForAuthDto userForLoginDto)
        {
            userForLoginDto.email = userForLoginDto.email.ToLower();

            var userFromRepo = await _repo.Login(userForLoginDto.email, userForLoginDto.password);

            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.id.ToString()),
                new Claim(ClaimTypes.Name,userFromRepo.email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:secretKey").Value));

            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescripter = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescripter);

            return Ok(new{
                token = tokenHandler.WriteToken(token)
            });
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteUser(UserForAuthDto userForDeleteDto)
        {
             userForDeleteDto.email = userForDeleteDto.email.ToLower();

            var userFromRepo = await _repo.Login(userForDeleteDto.email, userForDeleteDto.password);

            if (userFromRepo == null)
                return Unauthorized();

            if(await _repo.DeleteUser(userForDeleteDto.email, userForDeleteDto.password))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateUser(UserForUpdateDto userForUpdateDto)
        {
            userForUpdateDto.oldEmail = userForUpdateDto.oldEmail.ToLower();

            var userFromRepo = await _repo.Login(userForUpdateDto.oldEmail, userForUpdateDto.oldPassword);

            if (userFromRepo == null)
                return Unauthorized();

            userForUpdateDto.newEmail = userForUpdateDto.newEmail.ToLower();

            // var userToUpdate = new User
            // {
            //     Username = userForUpdateDto.newUsername
            // };
            // if(userToUpdate.Username==null)
            // userToUpdate.Username=userForUpdateDto.oldUsername;

            if (await _repo.UpdateUser(userForUpdateDto.oldEmail, userForUpdateDto.oldPassword, userForUpdateDto.newEmail, userForUpdateDto.newPassword) != null)
            {
                return Ok(userForUpdateDto);
            } 
            else
            {
                return BadRequest();
            }            
        }
    }
}
