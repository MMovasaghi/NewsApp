using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsApp.Dtos;
using NewsApp.Models;
using NewsApp.Repository.IRepos;

namespace NewsApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly IAppRepo<News> _repo;
        private readonly IMapper _mapper;

        public NewsController(IAppRepo<News> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var news = await _repo.GetAll();
                return Ok(news);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var news = await _repo.Get(id);
                return Ok(news);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm]NewsToCreate newsToCreate)
        {
            try
            {
                var news = _mapper.Map<News>(newsToCreate);
                if(await _repo.Create(news))
                {
                    return Ok(news);
                }
                return BadRequest("Error");
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost("update")]
        public async Task<IActionResult> Update(News news)
        {
            try
            {
                if(await _repo.Update(news))
                {
                    return Ok(news);
                }
                return BadRequest("Error");
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
    }
}
