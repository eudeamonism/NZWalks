using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        //Controller ctor
        //Initialize Field
        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
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
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                //Add roles to this User
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
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

        // POST Login w/ Token /api/auth/login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            //We will receive a DTO
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {

                //Check if password is valid
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    //Create Token: needs user and roles object
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        //NOTE: roles is of type IList so we converted it to List
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        //Creating a new response type for our jwtToken, not sending it as response as it is

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken,

                        };

                        return Ok(jwtToken);
                    }
                }


            }

            return BadRequest("Username or password is incorrect");
        }
    }
}

//Created a new Controller as API Controller
// Route accepts Register to trigger Register Controller
//Create a DTO called RegisterRequestDto and pass it as a parameter
//Create a Repository Method to lighten Controller
//Create a new DTO for jwtToken so we can pass more things in it
