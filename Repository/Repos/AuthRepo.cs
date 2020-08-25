using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Models;
using NewsApp.Repository.IRepos;

namespace NewsApp.Repository.Repos
{
    public class AuthRepo : IAuthRepo
    {
        private readonly DataContext _context;

        public AuthRepo(DataContext context)
        {
            _context = context;

        }
        public async Task<bool> DeleteUser(string email, string password)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.email == email);

                if (user == null)
                    return false;

                if (!VerfyPassword(password, user.passwordHash, user.passwordSalt))
                    return false;

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }                     
        }
        public async Task<User> Login(string email, string password)
        {
            /*
             * return value: 
             *      obj: successful
             *      null: notfound-Error
             */
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.email == email);

                if (user == null)
                    return null;

                if (!VerfyPassword(password, user.passwordHash, user.passwordSalt))
                    return null;

                return user;
            } 
            catch (Exception)
            {
                return null;
            }
            
        }

        private bool VerfyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            
        }

        public async Task<User> Register(User user, string password)
        {
            try
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);
                user.passwordHash = passwordHash;
                user.passwordSalt = passwordSalt;

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception)
            {
                return null;
            }

        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<User> UpdateUser(string oldEmail, string oldPassword, string newEmail, string newPassword)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.email == oldEmail);

                if (user == null)
                    return null;

                if (!VerfyPassword(oldPassword, user.passwordHash, user.passwordSalt))
                    return null;

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);
                user.passwordHash = passwordHash;
                user.passwordSalt = passwordSalt;
                user.email = newEmail;
                user.updated_at = DateTime.Now;

                _context.Update(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(x=>x.email==email))
            return true;

            return false;
        }
    }
}