using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RemindersManager.DAL;
using RemindersManager.Models;

namespace RemindersManager.Infrastructure.Repositories
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly ReminderContext _context;

        public ReminderRepository(ReminderContext context)
        {
            _context = context;
        }

        public async Task<List<Reminder>> GetAllAsync()
        {
            return await _context.Reminders.ToListAsync();
        }

        public async Task<Reminder> GetAsync(Guid id)
        {
            return await _context.Reminders.FirstAsync(x => x.Id == id);
        }

        public async Task<Reminder> AddAsync(Reminder item)
        {
            var newReminder = await _context.Reminders.AddAsync(item);
            await _context.SaveChangesAsync();

            return newReminder.Entity;
        }

        public async Task<Reminder> UpdateAsync(Reminder item)
        {
            var updatedReminder = _context.Reminders.Update(item);
            await _context.SaveChangesAsync();

            return updatedReminder.Entity;
        }

        public async Task<Reminder> FindAsync(string subject)
        {
            return await _context.Reminders.FirstOrDefaultAsync(t => t.Subject == subject);
        }

        public async Task<Reminder> RemoveAsync(Guid id)
        {
            var reminderToDelete = await GetAsync(id);

            if(reminderToDelete != null)
            {
                _context.Reminders.Remove(reminderToDelete);
                await _context.SaveChangesAsync();
                return reminderToDelete;
            }

            return null;
        }
    }
}
