using System.CommandLine;
using System.Text.Json;
using scl.POCO;
using System.Linq;
using scl.Enums;

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
        List<TaskTODO> tasks = new List<TaskTODO>();

        string existingJson = File.ReadAllText("TaskTODO.json");

        if(!string.IsNullOrWhiteSpace(existingJson))
        {
            tasks = JsonSerializer.Deserialize<List<TaskTODO>>(existingJson) ?? new List<TaskTODO>();
        }
        

        int taskIdCounter = tasks.Count > 0 ? tasks.Max(tasks => tasks.id) + 1 : 1;

        TaskTODO newTask = new TaskTODO
        {
            id = taskIdCounter,
            description = description,
            status = TaskStatusTodo.Todo,
            createdAt = DateTime.Now,
            updatedAt = DateTime.Now
        };

        tasks.Add(newTask);

        string fileName = "TaskTODO.json";
        string jsonString  = JsonSerializer.Serialize<List<TaskTODO>>(tasks, new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(fileName, jsonString);

        Console.WriteLine($"Output: Task added successfully (ID: {Guid.NewGuid()})");
        Console.WriteLine($"Task: {newTask}");
    }
}