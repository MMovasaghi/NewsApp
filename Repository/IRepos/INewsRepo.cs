using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewsApp.Models;
using NewsApp.Helpers;

namespace NewsApp.Repository.IRepos
{
    public interface INewsRepo
    {
        Task<bool> Create(News obj);
        Task<News> Get(Guid id);
        Task<ICollection<News>> GetAll();
        Task<ICollection<News>> Search(string item, SearchParam param);
        Task<ICollection<News>> GetAllNotExpired();
        Task<ICollection<News>> GetAllExpired();
        Task<bool> ArchiveExpireds();
        Task<ICollection<ArchivedNews>> GetArchive();
        Task<bool> Update(News obj);
        Task<bool> Delete(Guid id);
    }
    public enum SearchParam
    {
        all,
        title,
        content,
        created_at,
        expired_at
    }
}