using System;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Azure.Storage.Blobs;
namespace secondApi.Helpers
{


	public  class MyHelpers
	{

		//get Configuration by putting it in a constructor in order to reach connectionString
		private IConfiguration Configuration;

		public MyHelpers(IConfiguration _configuration)
        {
			Configuration = _configuration;
		}

		//
		public static bool CheckIsBound(int id, int length)
		{

			if (id < 0 || id > length-1) return false;

			return true;
		}


		public string getConnectionString(string connectionName) {
			return Configuration.GetConnectionString(connectionName).ToString();
		}


		/*
		 * UPLOADING IMAGE WITH AZURE NOT COMPLETE
		public async void UploadImage(IFormFile file) {

			string connectionString = getConnectionString("SongsAppConn");
			string containerName = "";//container of azure storage (a cloud folder)

			BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString, containerName);
			BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);
			var memoryStream = new MemoryStream();
			await song.Image.CopyToAsync(memoryStream);
			memoryStream.Position = 0;
			await blobClient.UpploadAsync(memoryStream);
		}
		*/
		
	}
}

