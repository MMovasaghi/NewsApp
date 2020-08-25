using System.Linq;
using AutoMapper;
using NewsApp.Dtos.News;
using NewsApp.Models;

namespace NewsApp.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<NewsToCreate, News>();
            CreateMap<News, NewsToShow>();
            CreateMap<News, NewsAdminPanel>();
            CreateMap<NewsToUpdate, News>();
            CreateMap<News, ArchivedNews>();
        }
    }
}