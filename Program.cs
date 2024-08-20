using System.Text;
using System.Text.Json;

namespace TaskTrackerCLI;

class Program
{
    private static List<Todo> _todos = [];
    private static readonly JsonSerializerOptions _jsonOpts = new() { WriteIndented = true };
    private const string JsonFileName = "tasklist.json";
    private static int _nextId;

    public static void Main(String[] args)
    {
        if (args.Length < 1)
        {
            PrintUsage();
            return;
        }

        LoadTasks();

        switch (args[0])
        {
            case "add":
                if (args.Length != 2)
                {
                    PrintUsage();
                    break;
                }

                AddTask(args[1]);
                break;
            case "update":
                if (args.Length != 3)
                {
                    PrintUsage();
                    break;
                }

                if (!int.TryParse(args[1], out int updateId))
                {
                    Console.WriteLine($"Invalid ID {args[1]}");
                }

                UpdateTask(updateId, args[2]);
                break;
            case "delete":
                if (args.Length != 2)
                {
                    PrintUsage();
                    break;
                }

                if (!int.TryParse(args[1], out int deleteId))
                {
                    Console.WriteLine($"Invalid ID {args[1]}");
                }

                DeleteTask(deleteId);
                break;
            case "list":
                if (args.Length == 1)
                {
                    ListTasks();
                    break;
                }

                switch (args[1])
                {
                    case "done":
                        ListTasks(TodoStatus.DONE);
                        break;
                    case "in-progress":
                        ListTasks(TodoStatus.IN_PROGRESS);
                        break;
                    case "todo":
                        ListTasks(TodoStatus.TODO);
                        break;
                    default:
                        Console.WriteLine($"Invalid status {args[1]}");
                        break;
                }

                break;
            case "mark":
                if (args.Length != 3)
                {
                    PrintUsage();
                    break;
                }

                var newStatus = args[2];

                if (!int.TryParse(args[1], out int markId))
                {
                    Console.WriteLine($"Invalid ID {args[1]}");
                    break;
                }

                switch (newStatus)
                {
                    case "done":
                        MarkTask(markId, TodoStatus.DONE);
                        break;
                    case "in-progress":
                        MarkTask(markId, TodoStatus.IN_PROGRESS);
                        break;
                    default:
                        Console.WriteLine("Tasks may be marked as 'done' or 'in-progress'");
                        break;
                }
                break;
            default:
                PrintUsage();
                break;
        }
    }

    private static void AddTask(string description)
    {
        _todos.Add(new(_nextId, description, DateTime.Now, DateTime.Now, TodoStatus.TODO));
        _nextId++;
        SaveTasks();
    }

    private static void UpdateTask(int id, string description)
    {
        var toUpdate = _todos.FindIndex(t => t.Id == id);

        if (toUpdate == -1)
        {
            Console.WriteLine($"Todo with ID {id} does not exist!");
            return;
        }

        _todos[toUpdate].Description = description;

        SaveTasks();
    }

    private static void MarkTask(int id, TodoStatus status)
    {
        var toMark = _todos.FindIndex(t => t.Id == id);

        if (toMark == -1)
        {
            Console.WriteLine($"Todo with ID {id} does not exist!");
            return;
        }

        _todos[toMark].Status = status;

        SaveTasks();
    }

    private static void DeleteTask(int id)
    {
        var toDelete = _todos.FindIndex(t => t.Id == id);

        if (toDelete == -1) return;

        _todos.RemoveAt(toDelete);
        SaveTasks();
    }

    private static void ListTasks() => PrintTasks(_todos);

    private static void ListTasks(TodoStatus status) =>
        PrintTasks(_todos.Where(t => t.Status == status));

    // Prints tasks in a table
    private static void PrintTasks(IEnumerable<Todo> tasks)
    {
        if (!tasks.Any())
        {
            Console.WriteLine("There are no tasks!");
            return;
        }

        var idColumnWidth = _todos.MaxBy(t => t.Id)!.Id.ToString().Length + 1;
        var statusColumnWidth = _todos.Any(t => t.Status == TodoStatus.IN_PROGRESS) ? 14 : 9;
        var descColumnWidth = _todos.MaxBy(t => t.Description.Length)!.Description.Length + 3;
        var dateFormat = "dd/MM/yy H:mm";
        var dateColumnWidth = dateFormat.Length;

        var sb = new StringBuilder();
        foreach (var t in tasks)
            sb.Append($"{t.Id.ToString().PadRight(Math.Max(idColumnWidth, 5))}" +
                        $"{t.Description.PadRight(Math.Max(descColumnWidth, 14))}" +
                        $"{t.Status.ToString().PadRight(statusColumnWidth)}" +
                        $"{t.CreatedAt.ToString(dateFormat).PadRight(dateColumnWidth + 4)}" +
                        $"{t.UpdatedAt.ToString(dateFormat).PadRight(dateColumnWidth)}\n");
        var tasksString = sb.ToString();

        // Header row
        Console.WriteLine("ID ".PadRight(Math.Max(idColumnWidth, 3)) +
                            "| Description ".PadRight(Math.Max(descColumnWidth, 14)) +
                            "| Status ".PadRight(statusColumnWidth) +
                            "| Created ".PadRight(dateColumnWidth + 4) +
                            "| Updated".PadRight(dateColumnWidth));

        // Separator
        Console.WriteLine(new string('-', tasksString.Length / _todos.Count - 1)); // -1 to account for newline character

        // Tasks
        Console.WriteLine(tasksString);
    }

    private static void SaveTasks()
    {
        // { WriteIndented = true }
        var s = JsonSerializer.Serialize(_todos, _jsonOpts);
        File.WriteAllText(JsonFileName, s);
    }

    private static void LoadTasks()
    {
        if (!File.Exists(JsonFileName)) return;

        var tasksJson = File.ReadAllText(JsonFileName);

        if (!string.IsNullOrEmpty(tasksJson))
            _todos = JsonSerializer.Deserialize<List<Todo>>(tasksJson)!;

        _nextId = _todos.Count == 0 ? 1 : _todos.MaxBy(t => t.Id)!.Id + 1;
    }

    private static void PrintUsage()
    {
        string usageString = "\nUsage:" +
                            $"\n    add <description> - add a todo with the specified description" +
                            $"\n    update <id> <description> - update todo with the specified ID" +
                            $"\n    delete <id> - delete todo with the specified ID\n" +
                            $"\n    list - list all todos" +
                            $"\n    list <done | todo | in-progress> - list todos with the specified status\n" +
                            $"\n    mark in-progress <id> - mark todo with the specified ID as 'in progress'" +
                            $"\n    mark done <id> - mark todo with the specified ID as 'done'";
        Console.WriteLine(usageString);
    }
}