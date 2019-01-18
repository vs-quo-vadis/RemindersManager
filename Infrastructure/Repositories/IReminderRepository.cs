using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RemindersManager.Models;

namespace RemindersManager.Infrastructure.Repositories
{
    public interface IReminderRepository
    {
        Task<List<Reminder>> GetAllAsync();

        Task<Reminder> GetAsync(Guid id);

        Task<Reminder> AddAsync(Reminder item);

        Task<Reminder> FindAsync(string subject);

        Task<Reminder> RemoveAsync(Guid id);

        Task<Reminder> UpdateAsync(Reminder item);
    }
}