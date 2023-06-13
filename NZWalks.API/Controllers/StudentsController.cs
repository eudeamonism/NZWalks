using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    // https://localhost:portnumber/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        //GET: https://localhost:portnumber/api/students
        [HttpGet]
        //Creating an action method
        public IActionResult GetAllStudents()
        {
            //In C#, you have to define type then declare a variable and then create an instance of it with new before using it? 
            string[] studentNames = new string[] { "Jonesy", "Doug", "SpiderHam", "Dostoevsky", "Uncle Roger" };


            return Ok(studentNames);
            
        }
    }
}
