

using TaskDependencyAnalyzer;
using TaskDependencyAnalyzer.Entity;

namespace TaskAlgorithmsTests;

public class CycleDetectionTests
{
    [Fact]
    public void Detects_Cycle_In_TaskDependencies()
    {
        var dependencies = new List<TaskDependency>
        {
            new() { TaskId = 1, DependsOnId = 2 },
            new() { TaskId = 2, DependsOnId = 3 },
            new() { TaskId = 3, DependsOnId = 1 } // Cycle: 1 -> 2 -> 3 -> 1
        };

        var hasCycle = CycleDetectionService.HasCycle(dependencies);

        Assert.True(hasCycle);
    }

    [Fact]
    public void ReturnsFalse_When_NoCycleExists()
    {
        var dependencies = new List<TaskDependency>
        {
            new() { TaskId = 2, DependsOnId = 1 },
            new() { TaskId = 3, DependsOnId = 2 },
            new() { TaskId = 4, DependsOnId = 2 }
        };

        var hasCycle = CycleDetectionService.HasCycle(dependencies);

        Assert.False(hasCycle);
    }
}
