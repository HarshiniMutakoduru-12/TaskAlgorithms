
using NestedTaskFlattener.Entity.Models;

namespace NestedTaskFlattener
{
    public static class TaskFlattener
    {
        public static List<FlattenTasks> FlattenTasks(Tasks root)
        {
            var result = new List<FlattenTasks>();
            void Traverse(Tasks task, int? parentId)
            {
                result.Add(new FlattenTasks
                {
                    Id = task.Id,
                    Name = task.Name,
                    ParentId = parentId
                });

                foreach (var sub in task.Subtasks)
                    Traverse(sub, task.Id);
            }

            Traverse(root, null);
            return result;
        }
    }
}
