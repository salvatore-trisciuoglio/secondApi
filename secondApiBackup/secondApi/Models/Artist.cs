using System;
namespace secondApi.Models
{
	public class Artist
	{

		public int Id { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string? ImageUrl { get; set; }

        public ICollection<Album>? Albums { get; set; }//setup new links between entities 1:M

        public ICollection<Song>? Songs { get; set; }
    }
}