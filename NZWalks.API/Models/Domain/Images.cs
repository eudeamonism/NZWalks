using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.Domain
{
    public class Images
    {
        public Guid Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? Description { get; set; }
        public  string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
    }
}

//The properties for uploading an image to the Image model state that it will not be sent to the database.
//It has a unique property of IFormFile
//We have two properties for the name of the file and its exension while having an optional property for a description
//We also have a long property which shows the size of the image and have string that will give the file path

//Next up is making changes to the DbContext before migrating

//We did this by simply adding another property below the class, nothing more.

//Open Package Console Manager
// Add-Migration "Adding Images Table" -Context "NZWalksDbContext"
// Update-Database -Context "NZWalksDbContext"
