namespace NZWalks.API.Models.DTO
{
    public class LoginResponseDto
    {
        public string JwtToken { get; set; }
    }
}

// NOTE: in our AuthController, we have the actual jwtToken. Notice it is starts with a lowercase. In our DTO
//however, we are starting with a capital letter. This is because we are setting J to the actual token j.