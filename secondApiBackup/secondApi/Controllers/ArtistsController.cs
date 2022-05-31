using System;
using System.Collections.Generic;
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
    public class ArtistsController : Controller
    {

        private ApiDbContext _dbContext;
        public ArtistsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;//dbContext is used to access dbSet prop in ApiDbContext class
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Artist artist) {
            //var imageUrl = await Helpers.UploadImage(artist.Image);
            //artist.ImageUrl = imageUrl;
            await _dbContext.Artists.AddAsync(artist);//track artist and when saveChanges() is called changes will affect db
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);

        }

        /*
        [HttpGet]
        public IActionResult GetAllArtists() {
            var artists = _dbContext.Artists;
            return Ok(artists);
        }
        */
        [HttpGet]
        public async Task<IActionResult> GetArtistsFiltered(int? pageNumber, int? pageSize)
        {
            int currentPageNumber = pageNumber ?? 1;
            int currentPageSize = pageSize ?? 5;

           var artists =await (from artist in _dbContext.Artists
            select new {//declare an anonymous type that declares a hidden class,
                        //extracting fata from dbSet Artists in dbContext filtered by Id and name
                Id = artist.Id,
                Name = artist.Name
            }).ToListAsync();

            return Ok(artists.Skip((currentPageNumber-1)*currentPageSize).Take(currentPageSize));
        }

        [HttpGet("{action}")]
        public async Task<IActionResult> ArtistDetails(int artistId) {
            //Include() allows you to indicate which related entities should be read from the database as part of the same query.
            //similiarly to a join in a query that merge entities    var artistDetails =_dbContext.Artists.Include(a => a.Songs);
            var artistDetails =await _dbContext.Artists.Where(a=>a.Id==artistId).Include(a => a.Songs).ToListAsync();

            return Ok(artistDetails);

        }
        
    }
}

