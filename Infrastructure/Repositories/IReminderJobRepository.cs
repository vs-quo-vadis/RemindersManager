using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RemindersManager.Models;

namespace RemindersManager.Infrastructure.Repositories
{
    public  interface IReminderJobRepository
    {
        Task<List<ReminderJob>> GetAllAsync();

        Task<ReminderJob> GetByReminderIdAsync(Guid reminderId);

        Task<ReminderJob> GetByJobIdAsync(string jobId);

        Task<ReminderJob> AddAsync(ReminderJob item);

        Task<ReminderJob> RemoveAsync(string jobId);

        Task<ReminderJob> RemoveAsync(Guid reminderId);
    }
}
