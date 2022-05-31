using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using secondApi.Models;
using secondApi.Helpers;
using System.Data;
using Npgsql;
using secondApi.Data;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
//Songs controller convention SongsController
namespace secondApi.Controllers
{


    [Route("api/[controller]")] //the routing to access this controller is localhost[...]/api/songs
    public class SongsController : Controller
    {

        private readonly IConfiguration _configuration;
        private ApiDbContext _dbContext;
        /*
        private static List<Song> songs = new List<Song>() {
            new Song(){Id=0 , Title = "Willow", Language="English"},
             new Song(){Id=1 , Title = "After Glow", Language="English"}
             
        };

        */
        public SongsController(IConfiguration configuration,ApiDbContext dbContext) {
            _configuration = configuration;
            _dbContext = dbContext;
        }




        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Song song)
        {
            //var imageUrl = await Helpers.UploadImage(artist.Image);
            //artist.ImageUrl = imageUrl;
            song.UploadedDate = DateTime.UtcNow;
            await _dbContext.Songs.AddAsync(song);//track artist and when saveChanges() is called changes will affect db
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);

        }



        [HttpGet]
        public IActionResult GetAllSongs(int? pageNumber, int? pageSize) {

          int currentPageNumber=  pageNumber ?? 1;
          int currentPageSize= pageSize ?? 5;
            var result = from song in _dbContext.Songs
                         select new
                         {
                             Id = song.Id,
                             Title = song.Title,
                             Duration = song.Duration



                         };

            return Ok(result.Skip((currentPageNumber-1)*currentPageSize).Take(currentPageSize));


        }


        [HttpGet("{action}")]
        public IActionResult FeaturedSongs()
        {

            var result = from song in _dbContext.Songs
                         where song.IsFeatured==true
                         select new
                         {
                             Id = song.Id,
                             Title = song.Title,
                             Duration = song.Duration



                         };

            return Ok(result);


        }





        /*
         * NEXT Services are implemented with queries
         * 
         * 
         * 
         * 
         */

        /*


        [HttpGet]
        public JsonResult Get() {
            string query =@"select * from songs";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SongsAppConn");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();

                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult(table);

        }

        [HttpPost]
        public JsonResult Post([FromForm]Song song)
        {
            string query = @"insert into songs (title)
                          values(@songTitle)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SongsAppConn");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();

                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue(@"songTitle", song.Title);//ord Add() to insert all attributes of input parameters
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult("Added successfully");

        }

        [HttpPut]
        public JsonResult Put(Song song)
        {
            string query = @"update songs
set title=@songTitle
where id=@songId ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SongsAppConn");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();

                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue(@"songTitle", song.Title);
                    myCommand.Parameters.AddWithValue(@"songId", song.Title);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult("Updated successfully");

        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from songs where songs.id=@songId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SongsAppConn");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();

                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue(@"songId", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult("Deleted successfully");

        }
        */
    }
}

