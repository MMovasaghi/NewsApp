using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NewsApp.Helpers
{
    public interface IUploader
    {
        Task<string> Upload(IFormFile file);
        Task<string> Update(string url, IFormFile newFile);
        bool delete(string url);
    }
}