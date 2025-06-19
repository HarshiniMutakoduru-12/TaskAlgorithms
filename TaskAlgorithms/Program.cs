// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using NestedTaskFlattener;
using NestedTaskFlattener.Data;
using NestedTaskFlattener.Entity.Models;

var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

var taskObject = JsonSerializer.Deserialize<Tasks>(SampleDatas.jsonSample, options);
var flattenResult = TaskFlattener.FlattenTasks(taskObject);

foreach (var task in flattenResult)
{
    Console.WriteLine($"Id: {task.Id}, Name: {task.Name}, ParentId: {task.ParentId}");
}
