using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyFirstAPI.Controllers
{
    [Route("api/SimplExampleAPI")]
    [ApiController]
    public class SimplExampleAPIController : ControllerBase
    {

        [HttpGet]
        public string GetName()
        {
            return "Hello World";
        }

        [HttpGet("greeting", Name = "GetGreeting")]
        public int GetGreeting()
        {
            return 42;
        }

    }
}
