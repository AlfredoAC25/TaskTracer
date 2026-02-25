using System;
using System.CommandLine;
using System.Text.Json;
using scl.POCO;

public class Program
{
    static int taskIdCounter = 1;
    public static int Main(string[] args)
    {
        // Add a task
        var taskArgument = new Argument<string>("description")
        {
            Description = "Task description"
        };

        var addCommand = new Command("add", "Add a new task")
        {
            taskArgument
        };

        addCommand.SetAction(parseResult =>
        {
            var description = parseResult.GetValue(taskArgument);
            addTask(description);
            return 0;
        });

        var rootCommand = new RootCommand("TaskTracer Command-Line Tool");
        rootCommand.Subcommands.Add(addCommand);

        return rootCommand.Parse(args).Invoke();
    }

    public static void addTask(string description)
    {
        TaskTODO newTask = new TaskTODO
        {
            id = taskIdCounter,
            description = description,
            status = "Pending",
            createdAt = DateTime.Now,
            updatedAt = DateTime.Now
        };

        taskIdCounter++;

        string fileName = "TaskTODO.json";
        string jsonString = JsonSerializer.Serialize(newTask);
        File.WriteAllText(fileName, jsonString);

        Console.WriteLine($"Output: Task added successfully (ID: {Guid.NewGuid()})");
        Console.WriteLine($"Task: {newTask}");
    }
}