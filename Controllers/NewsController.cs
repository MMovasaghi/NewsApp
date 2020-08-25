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
        private readonly INewsRepo _repo;
        private readonly IMapper _mapper;
        private readonly IUploader _uploader;

        public NewsController(INewsRepo repo, IMapper mapper, IUploader uploader)
        {
            _repo = repo;
            _mapper = mapper;
            _uploader = uploader;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
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
        [HttpGet("notex")]
        public async Task<IActionResult> GetAllNotExpired()
        {
            try
            {
                var news = await _repo.GetAllNotExpired();
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
        public async Task<IActionResult> Get(Guid id)
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
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm]NewsToUpdate newsToUpdate)
        {
            try
            {
                var oldNews = await _repo.Get(newsToUpdate.id);
                _mapper.Map(newsToUpdate, oldNews);
                if (newsToUpdate.image != null)
                {
                    var imageUrl = await _uploader.Upload(newsToUpdate.image);
                    if(imageUrl != null)
                    {
                        oldNews.imageUrl = imageUrl;
                    }
                }
                if (newsToUpdate.video != null)
                {
                    var videoUrl = await _uploader.Upload(newsToUpdate.video);
                    if(videoUrl != null)
                    {
                        oldNews.videoUrl = videoUrl;
                    }
                }
                if(await _repo.Update(oldNews))
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
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var oldNews = await _repo.Get(id);

                await _uploader.delete(oldNews.imageUrl);
                await _uploader.delete(oldNews.videoUrl);

                if(await _repo.Delete(id))
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
        [HttpGet("archiving")]
        public async Task<IActionResult> Archiving()
        {
            try
            {
                if (await _repo.ArchiveExpireds())
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
        [HttpGet("archives")]
        public async Task<IActionResult> Archives()
        {
            try
            {
                var archives = await _repo.GetArchive();
                return Ok(archives);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

    }
}
