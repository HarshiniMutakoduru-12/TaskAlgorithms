using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAlgorithmsTests
{
    using Xunit;
    using Moq;
    using Microsoft.Extensions.Logging;
    using Microsoft.EntityFrameworkCore;
    using Scheduler.Service;
    using Scheduler.DataLayer.Database;
    using Scheduler.Enums;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System;
    using Scheduler.DataLayer;
    using Scheduler.DataLayer.Model;

    public class TaskSchedulerServiceTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        private ILogger<TaskSchedulerService> GetLogger()
            => new LoggerFactory().CreateLogger<TaskSchedulerService>();

        [Fact]
        public async Task DistributeTasksAsync_ShouldAssignUnassignedTasks_ToLeastLoadedUsers()
        {
            // Arrange
            var context = GetInMemoryDbContext();

            context.Users.AddRange(new List<User>
        {
            new User
            {
                UserId = 1,
                Name = "Alice",
                Email = "alice@example.com",
                Role = "Developer",
                IsDeleted = false,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "Seeder",
                UpdatedOn = DateTime.UtcNow,
                UpdatedBy = "Seeder"
            },
            new User
            {
                UserId = 2,
                Name = "Bob",
                Email = "bob@example.com",
                Role = "Tester",
                IsDeleted = false,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "Seeder",
                UpdatedOn = DateTime.UtcNow,
                UpdatedBy = "Seeder"
            }
        });

            context.ToDoTasks.AddRange(new List<ToDoTask>
        {
            new ToDoTask
            {
                TaskId = 101,
                Title = "Fix login bug",
                Description = "Fix login issue on mobile devices",
                DueDate = DateTime.UtcNow.AddDays(2),
                Priority = TaskPriority.Medium,
                IsCompleted = false,
                UserId = null,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "System",
                UpdatedOn = DateTime.UtcNow,
                UpdatedBy = "System"
            },
            new ToDoTask
            {
                TaskId = 102,
                Title = "Update README",
                Description = "Improve documentation",
                DueDate = DateTime.UtcNow.AddDays(3),
                Priority = TaskPriority.Low,
                IsCompleted = false,
                UserId = 1,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "System",
                UpdatedOn = DateTime.UtcNow,
                UpdatedBy = "System"
            },
                new ToDoTask
            {
                TaskId = 103,
                Title = "Update README 2",
                Description = "Improve documentation 2",
                DueDate = DateTime.UtcNow.AddDays(4),
                Priority = TaskPriority.High,
                IsCompleted = false,
                UserId = null,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "System",
                UpdatedOn = DateTime.UtcNow,
                UpdatedBy = "System"
            }
        });

            await context.SaveChangesAsync();

            var service = new TaskSchedulerService(context, GetLogger());

            // Act
            await service.DistributeTasksAsync();

            // Assert
            var tasks = context.ToDoTasks.ToList();
            Assert.All(tasks, t => Assert.NotNull(t.UserId));

            //Checking the scheduler distribute the tasks to users evenly
            Assert.Equal(tasks.First(x => x.TaskId == 103).UserId, 2);
            Assert.Equal(tasks.First(x => x.TaskId == 101).UserId, 1);
        }

        [Fact]
        public async Task DistributeTasksAsync_ShouldNotFail_WhenNoUsers()
        {
            // Arrange
            var context = GetInMemoryDbContext(); // No users

            context.ToDoTasks.Add(new ToDoTask
            {
                TaskId = 103,
                Title = "Write unit tests",
                Description = "Create test coverage for business logic",
                DueDate = DateTime.UtcNow.AddDays(1),
                Priority = TaskPriority.Medium,
                IsCompleted = false,
                UserId = null,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "System",
                UpdatedOn = DateTime.UtcNow,
                UpdatedBy = "System"
            });

            await context.SaveChangesAsync();

            var service = new TaskSchedulerService(context, GetLogger());

            // Act
            await service.DistributeTasksAsync();

            // Assert
            var task = context.ToDoTasks.First();
            Assert.Null(task.UserId);
        }

    }

}
