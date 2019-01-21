using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RemindersManager.Infrastructure.Repositories;
using RemindersManager.Models;

namespace RemindersManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemindersController : ControllerBase
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly IReminderJobRepository _reminderJobRepository;
        //TODO: addunit of work private readonly IUnitOfWork _unitOfWork;

        public RemindersController(
            IReminderRepository reminderRepository,
            IReminderJobRepository reminderJobRepository
            )
        {
            _reminderRepository = reminderRepository;
            _reminderJobRepository = reminderJobRepository;
        }

        [HttpGet("[action]")]
        public async Task<List<Reminder>> GetReminders()
        {
            return await _reminderRepository.GetAllAsync();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetReminder([BindRequired, FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reminder = await _reminderRepository.GetAsync(id);

            if (reminder == null)
            {
                return NotFound();
            }

            return Ok(reminder);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> PutReminder([FromBody] Reminder reminder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedRemider = await _reminderRepository.UpdateAsync(reminder);

            if(updatedRemider != null)
            {
                return Ok(updatedRemider);
            }

            return NoContent();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PostReminder([FromBody] Reminder reminder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newReminder = await _reminderRepository.AddAsync(reminder);
   
            var jobId = BackgroundJob.Schedule(() => SendEmail(), newReminder.RemindDate);

            await _reminderJobRepository.AddAsync(new ReminderJob { ReminderId = newReminder.Id, JobId = jobId });

            return Ok(newReminder);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteReminder([BindRequired, FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reminder = await _reminderRepository.RemoveAsync(id);
            if (reminder == null)
            {
                return NotFound();
            }
            else
            {
                await _reminderJobRepository.RemoveAsync(reminder.Id);
            }

            return Ok(reminder);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> ActivateReminder([BindRequired, FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reminder = await _reminderRepository.GetAsync(id);
            var reminderJob = await _reminderJobRepository.GetByReminderIdAsync(id);

            var jobId = BackgroundJob.Schedule(() => SendEmail(), reminder.RemindDate);
            await _reminderJobRepository.AddAsync(new ReminderJob
            {
                ReminderId = reminder.Id,
                JobId = jobId
            });

            reminder.IsActive = true;
            await _reminderRepository.UpdateAsync(reminder);

            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DeactivateReminder([BindRequired, FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reminder = await _reminderRepository.GetAsync(id);
            var reminderJob = await _reminderJobRepository.GetByReminderIdAsync(id);

            if (!String.IsNullOrEmpty(reminderJob.JobId))
            {
                BackgroundJob.Delete(reminderJob.JobId);
            }

            reminder.IsActive = false;
            await _reminderRepository.UpdateAsync(reminder);

            return Ok();
        }

        public void SendEmail()
        {
            Console.WriteLine("Send Email");
        }
    }
}