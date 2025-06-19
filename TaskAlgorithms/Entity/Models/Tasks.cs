
namespace NestedTaskFlattener.Entity.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Tasks> Subtasks { get; set; } = new();
    }
}
