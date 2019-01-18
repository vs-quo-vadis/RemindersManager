using System;
using Microsoft.EntityFrameworkCore;
using RemindersManager.Models;

namespace RemindersManager.DAL.Seed
{
    public static class ModelBuilderSeedExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var authorId = Guid.NewGuid();

            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = authorId,
                    Name = "Franz Kafka",
                    Email = "franz.kafka@gmail.com"
                });

            modelBuilder.Entity<Reminder>().HasData(
                new Reminder
                {
                    Id = Guid.NewGuid(),
                    Subject = "Finish interview task",
                    Notes = "Write clean code!",
                    RemindDate = new DateTime(2019, 1, 18, 6, 0, 0),
                    IsActive = true,
                    AuthorId = authorId,
                });
        }
    }
}
