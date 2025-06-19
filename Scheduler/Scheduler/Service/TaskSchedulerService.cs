using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Scheduler.DataLayer.Database;
using Scheduler.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Service
{
    public class TaskSchedulerService
    {
        private readonly ILogger<TaskSchedulerService> _logger;

        private readonly ApplicationDbContext _applicationDbContext;

        public TaskSchedulerService(ApplicationDbContext applicationDbContext, ILogger<TaskSchedulerService> logger)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }
        public async Task DistributeTasksAsync()
        {
            _logger.LogInformation("Task distribution is started at: {time}", DateTimeOffset.Now);

            try
            {
                var users = await _applicationDbContext.Users
                    .Where(u => !u.IsDeleted)
                    .ToListAsync();

                var currentUserLoad = users.ToDictionary(
                u => u.UserId,
                    u => _applicationDbContext.ToDoTasks.Count(t => t.UserId == u.UserId && !t.IsCompleted)
                );

                var unassignedTasks = await _applicationDbContext.ToDoTasks
                    .Where(t => !t.IsCompleted && t.UserId == null)
                    .OrderByDescending(t => t.Priority == TaskPriority.High)
                    .ThenByDescending(t => t.Priority == TaskPriority.Medium)
                    .ThenBy(t => t.DueDate)
                    .ToListAsync();

                foreach (var task in unassignedTasks)
                {
                    var minLoadUser = currentUserLoad.OrderBy(x => x.Value).First().Key;
                    task.UserId = minLoadUser;
                    currentUserLoad[minLoadUser]++;
                }

                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Task distribution complete at: {time}", DateTimeOffset.Now);
            }
            _logger.LogInformation("Task distribution complete at: {time}", DateTimeOffset.Now);
        }
    }
}
