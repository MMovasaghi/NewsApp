using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Models;
using NewsApp.Repository.IRepos;

namespace NewsApp.Repository.Repos
{
    public class NewsRepo : INewsRepo
    {
        private readonly DataContext _context;

        public NewsRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> ArchiveExpireds()
        {
            try
            {
                var news = await this.GetAllExpired();                
                foreach (var item in news)
                {
                    var archive = new ArchivedNews()
                    {
                        title = item.title,
                        content = item.content,
                        imageUrl = item.imageUrl,
                        videoUrl = item.videoUrl,
                        publicImageId = item.publicImageId,
                        publicVideoId = item.publicVideoId,
                        created_at = item.created_at,
                        expired_at = item.expired_at
                    };
                    await _context.Archive.AddAsync(archive);
                    _context.News.Remove(item);
                }
                if(await _context.SaveChangesAsync() > 0)
                {
                    return true;
                }
                throw new Exception();
            }
            catch (System.Exception)
            {
                throw new Exception("Archive Failed.");
            }
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

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<News> Get(Guid id)
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
                var news = await _context.News
                    .OrderByDescending(x => x.created_at)
                    .ToListAsync();
                return news;
            }
            catch (System.Exception)
            {
                throw new Exception("Get All Failed.");
            }
        }

        public async Task<ICollection<News>> GetAllExpired()
        {
            try
            {
                var news = await _context.News
                    .Where(x => x.expired_at < DateTime.Now)
                    .OrderByDescending(x => x.created_at)
                    .ToListAsync();
                return news;
            }
            catch (System.Exception)
            {
                throw new Exception("Get All Failed.");
            }
        }

        public async Task<ICollection<News>> GetAllNotExpired()
        {
            try
            {
                var news = await _context.News
                    .Where(x => x.expired_at >= DateTime.Now)
                    .OrderByDescending(x => x.created_at)
                    .ToListAsync();
                return news;
            }
            catch (System.Exception)
            {
                throw new Exception("Get All Failed.");
            }
        }

        public async Task<ICollection<ArchivedNews>> GetArchive()
        {
            try
            {
                var news = await _context.Archive
                    .OrderByDescending(x => x.created_at)
                    .ToListAsync();
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
                if (obj.expired_at == null)
                {
                    obj.expired_at = DateTime.Now.AddDays(7);
                }
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