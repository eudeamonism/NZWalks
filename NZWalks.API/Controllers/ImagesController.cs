using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        // POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            //Validate if correct, if not don't move on.
            ValidateFileUpload(request);

            if(ModelState.IsValid)
            {
                //Convert DTO to Domain Model
                //Note: Image Model and Dto must match per used. We're going to attach path later.
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    Description = request.Description,
                };

                // User repository to upload image
               await imageRepository.Upload(imageDomainModel);


                return Ok(imageDomainModel);

            }

            return BadRequest(ModelState);
        }

        //This private method is to check if data is correct before moving along with the controller
        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[]
            {
                ".jpg", ".jpeg", ".png"
            };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName))) 
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size is more than 10MB. Please upload a smaller size.");
            }
        }
    }
}

