using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Models;
using NewsApp.Repository.IRepos;

namespace NewsApp.Repository.Repos
{
    public class NewsRepo : IAppRepo<News>
    {
        private readonly DataContext _context;

        public NewsRepo(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(News obj)
        {
            try
            {
                await _context.News.AddAsync(obj);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return true;
                }
                throw new Exception();
            }
            catch (System.Exception)
            {
                throw new Exception("Create Failed.");
            }
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<News> Get(int id)
        {
            try
            {
                var news = await _context.News.FindAsync(id);
                return news;
            }
            catch (System.Exception)
            {
                throw new Exception("Get Failed.");
            }
        }

        public async Task<ICollection<News>> GetAll()
        {
            try
            {
                var news = await _context.News.ToListAsync();
                return news;
            }
            catch (System.Exception)
            {
                throw new Exception("Get All Failed.");
            }
        }

        public async Task<bool> Update(News obj)
        {
            try
            {
                _context.News.Update(obj);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return true;
                }
                throw new Exception();
            }
            catch (System.Exception)
            {
                throw new Exception("Update Failed.");
            }
        }
    }
}