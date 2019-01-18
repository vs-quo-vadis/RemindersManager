using System;
using Microsoft.EntityFrameworkCore;
using RemindersManager.DAL.Seed;
using RemindersManager.Models;

namespace RemindersManager.DAL
{
    public class ReminderContext : DbContext
    {
        public ReminderContext(DbContextOptions<ReminderContext> options)
        : base(options)
        {
        }

        public virtual DbSet<Reminder> Reminders { get; set; }
        public virtual DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var authorId = Guid.NewGuid();
            modelBuilder.Entity<Author>(entity =>
            {               
                entity.Property(e => e.Name)
                    .HasMaxLength(80)
                    .IsRequired();

                entity.Property(e => e.Email)
                    .HasMaxLength(80)
                    .IsRequired();
            });

            modelBuilder.Entity<Reminder>(entity =>
            {
                entity.Property(e => e.Subject)
                    .HasMaxLength(80)
                    .IsRequired();

                entity.Property(e => e.RemindDate)
                    .IsRequired();

                entity.Property(e => e.Notes)
                    .HasMaxLength(500);
            });

            modelBuilder.Seed();
        }
    }
}
