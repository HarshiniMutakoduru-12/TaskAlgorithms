
using TaskDependencyAnalyzer.Entity;

namespace TaskDependencyAnalyzer;

public static class CycleDetectionService
{
    public static bool HasCycle(List<TaskDependency> dependencies)
    {
        var graph = new Dictionary<int, List<int>>();
        foreach (var dep in dependencies)
        {
            if (!graph.ContainsKey(dep.DependsOnId))
                graph[dep.DependsOnId] = new List<int>();
            graph[dep.DependsOnId].Add(dep.TaskId);
        }

        var visited = new HashSet<int>();
        var recStack = new HashSet<int>();

        bool DFS(int node)
        {
            if (recStack.Contains(node)) return true;
            if (visited.Contains(node)) return false;

            visited.Add(node);
            recStack.Add(node);

            if (graph.TryGetValue(node, out var neighbors))
            {
                foreach (var n in neighbors)
                {
                    if (DFS(n)) return true;
                }
            }

            recStack.Remove(node);
            return false;
        }

        foreach (var node in graph.Keys)
        {
            if (DFS(node)) return true;
        }

        return false;
    }
}
