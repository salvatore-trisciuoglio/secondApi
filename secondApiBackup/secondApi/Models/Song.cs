using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace secondApi.Models
{
	public class Song
	{
		
		    [Required(ErrorMessage ="Title cannot be empty")] //title cannot be null
            public string Title{ get; set; }

		    public int Id { get; set; }

            public string Duration { get; set; }

            public DateTime UploadedDate { get; set; }

            public bool? IsFeatured { get; set; }

            public string? Language { get; set; }

            [NotMapped]//it is not mapped if linked with migration in a database
		    public IFormFile? Image { get; set; }

            [NotMapped]
            public IFormFile? AudioFile { get; set; }

            public string? ImageUrl { get; set; }

            public string? AudioUrl { get; set; }

            public int ArtistId { get; set; }

            public int? AlbumId { get; set; } //may be no album for a song so question mark after type

    }
}

