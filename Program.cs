using System.CommandLine;

namespace scl;

class Program
{
    public static int taskIdCounter = 1;

    static int Main(string[] args)
    {
        var rootCommand = new RootCommand("TaskTracer Command-Line Tool");

        //  Add Task
        var addCommand = new Command("add", "Add a new task");
        var updateCommand = new Command("update", "Update a task");
        var deleteCommand = new Command("delete", "Delete a task");

        rootCommand.Subcommands.Add(addCommand);
        rootCommand.Subcommands.Add(updateCommand);
        rootCommand.Subcommands.Add(deleteCommand);

        addCommand.SetAction(parseResult =>
        {
            string task = Console.ReadLine() ?? string.Empty;
            AddCommand(task);
            return 0;
        });

        ParseResult parseResult = rootCommand.Parse(args);
        return parseResult.Invoke();
    }

    static void AddCommand(string task)
    {
        taskIdCounter += 1;
        Console.WriteLine($"Output: Task added successfully(ID: {taskIdCounter})");
    }
}