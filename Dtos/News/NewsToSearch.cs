using System;
using NewsApp.Repository.IRepos;

namespace NewsApp.Dtos.News
{
    public class NewsToSearch
    {
        public string item { get; set; }
        public SearchParam param { get; set; }
    }
}