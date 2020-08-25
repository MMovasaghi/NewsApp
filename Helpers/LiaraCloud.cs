using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NewsApp.Helpers
{
    public class LiaraCloud : IUploader
    {
        public bool delete(string url)
        {
            throw new NotImplementedException();
        }

        public Task<string> Update(string url, IFormFile newFile)
        {
            throw new NotImplementedException();
        }

        public Task<string> Upload(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}