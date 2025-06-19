
namespace NestedTaskFlattener.Data
{
    public static class SampleDatas
    {
      public static string jsonSample = @"
        {
          ""id"": 10,
          ""name"": ""Root Task"",
          ""subtasks"": [
            {
              ""id"": 11,
              ""name"": ""Subtask 1"",
              ""subtasks"": []
            },
            {
              ""id"": 12,
              ""name"": ""Subtask 2"",
              ""subtasks"": [
                {
                  ""id"": 13,
                  ""name"": ""Subtask 2.1"",
                  ""subtasks"": []
                },
                {
                  ""id"": 14,
                  ""name"": ""Subtask 2.2"",
                  ""subtasks"": []
                }
              ]
            }
          ]
        }";
    }
}
