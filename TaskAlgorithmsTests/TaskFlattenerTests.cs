using NestedTaskFlattener;
using NestedTaskFlattener.Entity.Models;

namespace TaskAlgorithmsTests
{
    public class TaskFlattenerTests
    {
        [Fact]
        public void TestFlattenTasks()
        {
            // Arrange
            var input = new Tasks
            {
                Id = 1,
                Name = "Task A",
                Subtasks = new List<Tasks>
                {
                    new Tasks
                    {
                        Id = 2,
                        Name = "Task B",
                        Subtasks = new List<Tasks>
                        {
                            new Tasks { Id = 3, Name = "Task C" }
                        }
                    }
                }
            };

            // Act
            var result = TaskFlattener.FlattenTasks(input);

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Null(result[0].ParentId);      // Task A
            Assert.Equal(1, result[1].ParentId);  // Task B
            Assert.Equal(2, result[2].ParentId);  // Task C
        }
    }
}
