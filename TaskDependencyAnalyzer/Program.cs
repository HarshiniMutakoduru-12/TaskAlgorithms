// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using TaskDependencyAnalyzer;
using TaskDependencyAnalyzer.Entity;
using TaskDependencyAnalyzer.TempData;

Console.WriteLine("Hello, World!");

var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

List<TaskDependency> nonCircularDependencies = JsonSerializer.Deserialize<List<TaskDependency>>(SamplesDatas.nonCircularJson);

List<TaskDependency> circularDependencies = JsonSerializer.Deserialize<List<TaskDependency>>(SamplesDatas.circularJson);

bool resultForNonCircularInput = CycleDetectionService.HasCycle(nonCircularDependencies);
bool resultForCircularInput = CycleDetectionService.HasCycle(circularDependencies);


Console.WriteLine("Circular Dependencies Result:");
Console.WriteLine(resultForCircularInput);

Console.WriteLine("Non-Circular Dependencies Result:");
Console.WriteLine(resultForNonCircularInput);