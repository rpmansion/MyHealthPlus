using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyHealthPlus.Web.Dtos;

namespace MyHealthPlus.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(ILogger<AppointmentController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Create(AppointmentDto dto)
        {
            return Ok();
        }
    }
}