using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsApp.Repository.IRepos
{
    public interface IAppRepo<T>
    {
        Task<bool> Create(T obj);
        Task<T> Get(int id);
        Task<ICollection<T>> GetAll();
        Task<bool> Update(T obj);
        Task<bool> Delete(int id);
    }
}