using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyHealthPlus.Data.Contexts;
using MyHealthPlus.Data.Enums;
using MyHealthPlus.Data.Models;
using MyHealthPlus.Web.Models;

namespace MyHealthPlus.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly UserManager<Account> _userManager;
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(
            UserManager<Account> userManager,
            AppDbContext appDbContext,
            ILogger<AppointmentController> logger)
        {
            _logger = logger;
            _userManager = userManager;
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

        [HttpGet("patients")]
        public async Task<IActionResult> GetCurrentToEndDate()
        {
            var appointments = _appDbContext.Appointments
                .Where(x => x.Date.Date == DateTime.UtcNow.Date)
                .Include(x => x.Account);


            var list = await appointments.ToListAsync();

            return Ok(list);
        }

        [HttpGet("admin/{adminId}")]
        public async Task<IActionResult> GetByAdmin(int adminId)
        {
            if (User.IsInRole("Admin"))
            {
                return Ok();
            }

            return Unauthorized();
        }


        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctor(int doctorId)
        {
            var appointments = _appDbContext.Appointments
                .Where(x => x.Date.Date == DateTime.UtcNow.Date)
                .Include(x => x.Account);


            var list = await appointments.ToListAsync();

            return Ok(list);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]AppointmentModel model)
        {
            // var account = _userManager.FindByNameAsync(User.Identity.Name);

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