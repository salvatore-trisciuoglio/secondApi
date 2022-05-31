using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using secondApi.Data;
using secondApi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace secondApi.Controllers
{
    [Route("api/[controller]")]
    public class AlbumsController : Controller
    {
        private ApiDbContext _dbContext;

        public AlbumsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Album album)
        {
            //var imageUrl = await Helpers.UploadImage(artist.Image);
            //artist.ImageUrl = imageUrl;
            await _dbContext.Albums.AddAsync(album);//track artist and when saveChanges() is called changes will affect db
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);

        }
        [HttpGet]
        public IActionResult Get()
        {
            var albums = _dbContext.Albums;
            return Ok(albums);

        }

        [HttpGet("{action}")]
        public IActionResult AlbumDetails() {
           var albumDetails= _dbContext.Albums.Include(a => a.Songs);
            return Ok(albumDetails);
        }
    }
}

