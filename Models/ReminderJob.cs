using System;

namespace RemindersManager.Models
{
    public class ReminderJob
    {
        public Guid Id { get; set; }

        public Guid ReminderId { get; set; }

        public string JobId { get; set; }
    }
}
