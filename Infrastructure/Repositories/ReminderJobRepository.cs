using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RemindersManager.DAL;
using RemindersManager.Models;

namespace RemindersManager.Infrastructure.Repositories
{
    public class ReminderJobRepository : IReminderJobRepository
    {
        private readonly ReminderContext _context;

        public ReminderJobRepository(ReminderContext context)
        {
            _context = context;
        }

        public async Task<List<ReminderJob>> GetAllAsync()
        {
            return await _context.ReminderJobs.ToListAsync();
        }

        public async Task<ReminderJob> GetByJobIdAsync(string jobId)
        {
            return await _context.ReminderJobs.FirstAsync(x => x.JobId == jobId);
        }

        public async Task<ReminderJob> GetByReminderIdAsync(Guid reminderId)
        {
            return await _context.ReminderJobs.FirstAsync(x => x.ReminderId == reminderId);
        }

        public async Task<ReminderJob> AddAsync(ReminderJob item)
        {
            var newJob = await _context.ReminderJobs.AddAsync(item);
            await _context.SaveChangesAsync();

            return newJob.Entity;
        }

        public async Task<ReminderJob> RemoveAsync(string jobId)
        {
            var reminderJobToDelete = await GetByJobIdAsync(jobId);

            if (reminderJobToDelete != null)
            {
                _context.ReminderJobs.Remove(reminderJobToDelete);
                await _context.SaveChangesAsync();
                return reminderJobToDelete;
            }

            return null;
        }

        public async Task<ReminderJob> RemoveAsync(Guid reminderId)
        {
            var reminderJobToDelete = await GetByReminderIdAsync(reminderId);

            if (reminderJobToDelete != null)
            {
                _context.ReminderJobs.Remove(reminderJobToDelete);
                await _context.SaveChangesAsync();
                return reminderJobToDelete;
            }

            return null;
        }
    }
}
