using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyHealthPlus.Core.Extensions;
using MyHealthPlus.Core.Extensions.Dates;
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
        private readonly SignInManager<Account> _signInManager;
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(
            AppDbContext appDbContext,
            UserManager<Account> userManager,
            SignInManager<Account> signInManager,
            ILogger<AppointmentController> logger)
        {
            _logger = logger;
            _userManager = userManager;
            _appDbContext = appDbContext;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetByDate(DateTime date)
        {
            var appointments = _appDbContext.Appointments
                .Where(x => x.Date.Date == date.Date);

            var list = await appointments.ToListAsync();

            return Ok(list);
        }

        [HttpGet("current-month")]
        public async Task<IActionResult> GetCurrentMonth()
        {
            var dateUtcNow = DateTime.UtcNow;
            var dateRange = new DateTimeRange(dateUtcNow.StartOfMonth(), dateUtcNow.EndOfMonth());

            var appointments = await _appDbContext.Appointments.ToListAsync();

            var list = appointments
                .Where(x => dateRange.Includes(x.Date))
                .ToList();

            return Ok(list);
        }


        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctor(int doctorId)
        {
            var account = await _userManager.GetUserAsync(User);
            var isDoctor = await _userManager.IsInRoleAsync(account, "Doctor");

            if (isDoctor)
            {
                var appointments = _appDbContext.Appointments
                    .Where(x => x.Date.Date == DateTime.UtcNow.Date)
                    .Include(x => x.Account);


                var list = await appointments.ToListAsync();

                return Ok(list);
            }

            return Unauthorized("You have no permission to access this resource.");
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]AppointmentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = await _userManager.GetUserAsync(User);
            var isPatient = await _userManager.IsInRoleAsync(account, "Doctor");

            if (isPatient)
            {
                var appointments = await _appDbContext.Appointments
                    .Where(x => x.Date.Date == model.AppointmentDate.Date
                        && x.Time.Hour == model.AppoinmentTime.Hour).ToListAsync();

                if (appointments.IsNotNullOrEmpty())
                {
                    var appointment = new Appointment
                    {
                        Status = AppointmentStatus.Pending,
                        CheckupType = model.CheckupType,
                        Date = model.AppointmentDate,
                        Time = model.AppoinmentTime,
                        Note = model.Note,
                        Account = account
                    };

                    _appDbContext.Appointments.Add(appointment);
                    await _appDbContext.SaveChangesAsync();

                    return Ok("Successfully created an appointment.");
                }

                return BadRequest("Appointment has already been created.");
            }

            return Unauthorized("You have no permission to access this resource.");
        }

        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateStatus(int appointmentId, AppointmentStatus status)
        {
            var account = await _userManager.GetUserAsync(User);
            var isDoctor = await _userManager.IsInRoleAsync(account, "Doctor");

            if (isDoctor)
            {
                var appointment = await _appDbContext.Appointments
                    .FirstOrDefaultAsync(x => x.Id == appointmentId);

                appointment.Status = status;
                await _appDbContext.SaveChangesAsync();

                return Ok("Successfully updated the appointment status.");
            }

            return Unauthorized("You have no permission to access this resource.");
        }
    }
}