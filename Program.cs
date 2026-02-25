using System.CommandLine;

int taskIdCounter = 1;

//  Add a task
var taskArgument = new Argument<string>("description")
{
    Description = "Task description"
};
var addCommand = new Command("add", "Add a new task")
{
    taskArgument
};

addCommand.SetAction((parseResult) =>
{
    var description = parseResult.GetValue(taskArgument);
    taskIdCounter += 1;
    Console.WriteLine($"Output: Task added successfully(ID: {taskIdCounter})");
    Console.WriteLine($"Task: {taskArgument}");
    return 0;
});

var rootCommand = new RootCommand("TaskTracer Command-Line Tool");
rootCommand.Subcommands.Add(addCommand);

return rootCommand.Parse(args).Invoke();