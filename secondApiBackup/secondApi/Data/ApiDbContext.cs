

using System;
using Microsoft.EntityFrameworkCore;
using secondApi.Models;

namespace secondApi.Data
{



	public class ApiDbContext:DbContext
	{

        public ApiDbContext(DbContextOptions<ApiDbContext> options): base(options)
        {


        }

        public DbSet<Song>? Songs { get; set; }
        public DbSet<Artist>? Artists { get; set; }
        public DbSet<Album>? Albums { get; set; }

    }
}

