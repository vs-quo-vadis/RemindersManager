using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly IReminderRepository _repository;

        public RemindersController(
            IReminderRepository repository
            )
        {
            _repository = repository;
        }

        [HttpGet("[action]")]
        public async Task<List<Reminder>> GetReminders()
        {
            return await _repository.GetAllAsync();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetReminder([BindRequired, FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reminder = await _repository.GetAsync(id);

            if (reminder == null)
            {
                return NotFound();
            }

            return Ok(reminder);
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> PutReminder([BindRequired, FromRoute] Guid id, [BindRequired, FromRoute] Reminder reminder)
        {
            if (!ModelState.IsValid || id != reminder.Id)
            {
                return BadRequest(ModelState);
            }

            var updatedRemider = await _repository.UpdateAsync(reminder);

            if(updatedRemider != null)
            {
                return Ok();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostReminder([FromBody] Reminder reminder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newReminder = await _repository.AddAsync(reminder);

            return CreatedAtAction("GetReminder", new { id = newReminder.Id }, newReminder);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteReminder([BindRequired, FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reminder = await _repository.RemoveAsync(id);
            if (reminder == null)
            {
                return NotFound();
            }

            return Ok(reminder);
        }

    }
}