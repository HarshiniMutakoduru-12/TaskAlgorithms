
namespace TaskDependencyAnalyzer.TempData
{
    public static class SamplesDatas
    {
        public static string nonCircularJson = @"
        [
          { ""TaskId"": 2, ""DependsOnId"": 1 },
          { ""TaskId"": 3, ""DependsOnId"": 2 },
          { ""TaskId"": 4, ""DependsOnId"": 3 }
        ]";

        public static string circularJson = @"
        [
          { ""TaskId"": 1, ""DependsOnId"": 2 },
          { ""TaskId"": 2, ""DependsOnId"": 3 },
          { ""TaskId"": 3, ""DependsOnId"": 1 }
        ]";


    }
}
