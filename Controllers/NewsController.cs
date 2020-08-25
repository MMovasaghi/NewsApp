using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsApp.Dtos.News;
using NewsApp.Helpers;
using NewsApp.Models;
using NewsApp.Repository.IRepos;

namespace NewsApp.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]    
    public class NewsController : ControllerBase
    {
        private readonly IAppRepo<News> _repo;
        private readonly IMapper _mapper;
        private readonly IUploader _uploader;

        public NewsController(IAppRepo<News> repo, IMapper mapper, IUploader uploader)
        {
            _repo = repo;
            _mapper = mapper;
            _uploader = uploader;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var news = await _repo.GetAll();
                var newsToShow = _mapper.Map<List<NewsToShow>>(news);
                return Ok(newsToShow);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var news = await _repo.Get(id);
                var newsToShow = _mapper.Map<NewsToShow>(news);
                return Ok(newsToShow);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm]NewsToCreate newsToCreate)
        {
            try
            {
                var news = _mapper.Map<News>(newsToCreate);
                
                if (newsToCreate.image != null)
                {
                    var imageUrl = await _uploader.Upload(newsToCreate.image);
                    if(imageUrl != null)
                    {
                        news.imageUrl = imageUrl;
                    }
                }
                if (newsToCreate.video != null)
                {
                    var videoUrl = await _uploader.Upload(newsToCreate.video);
                    if(videoUrl != null)
                    {
                        news.videoUrl = videoUrl;
                    }
                }
                if(await _repo.Create(news))
                {
                    return Ok();
                }
                return BadRequest("Error");
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost("update")]
        public async Task<IActionResult> Update(News news)
        {
            try
            {
                if(await _repo.Update(news))
                {
                    return Ok(news);
                }
                return BadRequest("Error");
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
    }
}
