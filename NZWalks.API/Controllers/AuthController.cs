using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        //Controller ctor
        //Initialize Field
        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        // POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            // DTO data extracted.
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            //Create a User
           var identityResult =  await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                //Add roles to this User
                if(registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    // Adds User and Roles
                   identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        //Success
                        return Ok("User was registered. Please login.");
                    }
                }
                
            }
            //Failure, with vague message
            return BadRequest("Something went wrong");
        }
    }
}

//Created a new Controller as API Controller
// Route accepts Register to trigger Register Controller
//Create a DTO called RegisterRequestDto and pass it as a parameter
