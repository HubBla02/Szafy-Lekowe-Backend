using Microsoft.AspNetCore.Mvc;
using SzafyNaLeki.Entities;
using SzafyNaLeki.Models;
using SzafyNaLeki.Services;

namespace SzafyNaLeki.Controllers
{
    [Route("api/alarm")]
    public class AlarmController : ControllerBase
    {
        private readonly IAlarmService _alarmService;

        public AlarmController(IAlarmService alarmService)
        {
            _alarmService = alarmService;
        }

        [HttpGet]
        public ActionResult<Alarm> Get()
        {
            var alarm = _alarmService.Get();
            if (alarm == null)
            {
                return NotFound();
            }
            return Ok(alarm);
        }


    }
}
