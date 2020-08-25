using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewsApp.Models;

namespace NewsApp.Repository.IRepos
{
    public interface INewsRepo
    {
        Task<bool> Create(News obj);
        Task<News> Get(Guid id);
        Task<ICollection<News>> GetAll();
        Task<ICollection<News>> GetAllNotExpired();
        Task<ICollection<News>> GetAllExpired();
        Task<bool> ArchiveExpireds();
        Task<ICollection<ArchivedNews>> GetArchive();
        Task<bool> Update(News obj);
        Task<bool> Delete(Guid id);
    }
}