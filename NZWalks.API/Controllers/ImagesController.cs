using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        // POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            //Validate if correct, if not don't move on.
            ValidateFileUpload(imageUploadRequestDto);

            if(ModelState.IsValid)
            {
                // User repository to upload image
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

