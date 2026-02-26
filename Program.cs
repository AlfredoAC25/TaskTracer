using System.CommandLine;
using System.Text.Json;
using scl.POCO;
using System.Linq;
using scl.Enums;

public class Program
{
    static int taskIdCounter = 1;
    private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions { WriteIndented = true };
    public static int Main(string[] args)
    {
        // Add a task
        var taskDescriptionArgument = new Argument<string>("description")
        {
            Description = "Task description"
        };

        var addCommand = new Command("add", "Add a new task")
        {
            taskDescriptionArgument
        };

        addCommand.SetAction(parseResult =>
        {
            string? description = parseResult.GetValue(taskDescriptionArgument);
            if (!string.IsNullOrWhiteSpace(description)) addTask(description);

            return 0;
        });

        // Update a task
        var updateTaskArgument = new Argument<string>("description")
        {
            Description = "Task description to update"
        };

        var getTaskIdArgument = new Argument<int>("id")
        {
            Description = "Task ID"
        };

        var updateCommand = new Command("update", "Update a new task")
        {
            getTaskIdArgument,
            updateTaskArgument
        };

        updateCommand.SetAction(parseResult =>
        {
            string? description = parseResult.GetValue(updateTaskArgument);
            int idInput = parseResult.GetValue(getTaskIdArgument);

            if (!string.IsNullOrWhiteSpace(description)) updateTask(idInput, description);
            else Console.WriteLine("Invalid input for ID or description.");
                
            return 0;
        });

        //  Delete a task
        var deleteCommand = new Command("delete", "Delete a task")
        {
            getTaskIdArgument
        };

        deleteCommand.SetAction(parseResult =>
        {
            int idInput = parseResult.GetValue(getTaskIdArgument);

            deleteTask(idInput);

            return 0;
        });

        var rootCommand = new RootCommand("TaskTracer Command-Line Tool");
        rootCommand.Subcommands.Add(addCommand);
        rootCommand.Subcommands.Add(updateCommand);
        rootCommand.Subcommands.Add(deleteCommand);

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
        string jsonString  = JsonSerializer.Serialize<List<TaskTODO>>(tasks, JsonOptions);

        File.WriteAllText(fileName, jsonString);

        Console.WriteLine($"Output: Task added successfully (ID: {Guid.NewGuid()})");
        Console.WriteLine($"Task: {newTask}");
    }

    public static void updateTask(int id, string description)
    {
        List<TaskTODO> tasks = new List<TaskTODO>();
        string fileName = "TaskTODO.json";

        string existingJson = File.ReadAllText("TaskTODO.json");

        if (!string.IsNullOrWhiteSpace(existingJson))
        {
            tasks = JsonSerializer.Deserialize<List<TaskTODO>>(existingJson) ?? new List<TaskTODO>();
        }

        TaskTODO? task = tasks.FirstOrDefault(t => t.id == id);

        task?.description = description;

        string jsonString = JsonSerializer.Serialize<List<TaskTODO>>(tasks, JsonOptions);

        File.WriteAllText(fileName, jsonString);            
    }

    public static void deleteTask(int id)
    {
        List<TaskTODO> tasks = new List<TaskTODO>();
        string fileName = "TaskTODO.json";

        string existingJson = File.ReadAllText("TaskTODO.json");

        if (!string.IsNullOrWhiteSpace(existingJson))
        {
            tasks = JsonSerializer.Deserialize<List<TaskTODO>>(existingJson) ?? new List<TaskTODO>();
        }

        TaskTODO? task = tasks.FirstOrDefault(t => t.id == id);

        if(task != null)
        {
            tasks.Remove(task);
            Console.WriteLine("Task deleted successfully.");
        }

        string jsonString = JsonSerializer.Serialize<List<TaskTODO>>(tasks, JsonOptions);

        File.WriteAllText(fileName, jsonString);
    }
}