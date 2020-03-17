using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyHealthPlus.Data.Contexts;
using MyHealthPlus.Data.Enums;
using MyHealthPlus.Data.Models;
using MyHealthPlus.Web.Models;

namespace MyHealthPlus.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(
            AppDbContext appDbContext,
            ILogger<AppointmentController> logger)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetByDate(DateTime date)
        {
            var appointments = await _appDbContext.Appointments
                .Where(x => x.Date.Date == date.Date)
                .ToListAsync();


            return Ok(appointments);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(AppointmentModel model)
        {
            var appointment = new Appointment
            {
                CheckupType = model.CheckupType,
                Date = model.AppointmentDate,
                Time = model.AppoinmentTime,
                Note = model.Note
            };

            _appDbContext.Appointments.Add(appointment);
            await _appDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateStatus(int appointmentId, AppointmentStatus status)
        {
            var appointment = await _appDbContext.Appointments
                .FirstOrDefaultAsync(x => x.Id == appointmentId);

            appointment.Status = status;
            await _appDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}