using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interface;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly IConfiguration configuration;
        private readonly IServiceWrapper services;

        public TestController(IConfiguration configuration, IServiceWrapper services)
        {
            this.configuration = configuration;
            this.services = services;
        }

        [HttpGet("DbString")]
        public async Task<IActionResult> DbString()
        {
            bool IsInMemory = configuration["ConnectionStrings:InMemory"].ToLower() == "true";
            if (IsInMemory)
            {
                return Ok("In Memory");
            }
            return Ok(configuration.GetConnectionString("Default"));
        }
        [HttpGet("NukeDB")]
        public async Task<IActionResult> Nuke()
        {
            bool IsInMemory = configuration["ConnectionStrings:InMemory"].ToLower() == "true";
            if (IsInMemory)
            {
                return Ok("In Memory");
            }
            var list = services.System.Nuke();
            return Ok();
        }
    }
}
