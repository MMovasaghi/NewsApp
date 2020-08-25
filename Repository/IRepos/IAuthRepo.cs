using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewsApp.Models;

namespace NewsApp.Repository.IRepos
{
    public interface IAuthRepo
    {
        /// <summary>
        /// it will return object of user that created just now, if error ocure return null
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<User> Register(User user, string password);
        /// <summary>
        /// it will return object of user, if error ocure or notfound return null
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
        /// <summary>
        /// it will be return the object of updated user, if error ocure or notfound return null
        /// </summary>
        /// <param name="oldUsername"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newUsername"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task<User> UpdateUser(string oldUsername, string oldPassword, string newUsername, string newPassword);
        /// <summary>
        /// it will be delete the user if input data is correct and return TRUE, if error ocure or notfound return FALSE
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> DeleteUser(string username, string password);
    }
}