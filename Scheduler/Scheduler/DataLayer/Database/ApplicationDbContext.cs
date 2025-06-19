using Microsoft.EntityFrameworkCore;
using Scheduler.DataLayer.Model;
using Scheduler.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DataLayer.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions option) : base(option)
        {

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ToDoTask> ToDoTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoTask>(entity =>
            {
                entity.Property(e => e.Priority)
                      .HasConversion(
                          v => v.ToString(),
                          v => (TaskPriority)Enum.Parse(typeof(TaskPriority), v));

            });
        }
    }

}
