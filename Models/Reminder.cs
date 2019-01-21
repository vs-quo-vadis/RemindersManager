using System;

namespace RemindersManager.Models
{
    public class Reminder
    {
        public Guid Id { get; set; }

        public string Subject { get; set; }

        public string Notes { get; set; }

        public DateTime RemindDate { get; set; }

        public bool IsActive { get; set; }

        public bool IsCancelled { get; set; }

        public Guid? AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
