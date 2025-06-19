
namespace TaskDependencyAnalyzer.Entity
{
    public class TaskDependency
    {
        public int TaskId { get; set; }
        public int DependsOnId { get; set; }
    }
}
