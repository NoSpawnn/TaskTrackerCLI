namespace TaskTrackerCLI;

public class Todo(int id, string description, DateTime createdAt, DateTime updatedAt, TodoStatus status)
{
    private string _description = description;
    private TodoStatus _status = status;
    public int Id { get; } = id;

    public string Description
    {
        get => _description;
        set
        {
            _description = value;
            UpdatedAt = DateTime.Now;
        }
    }

    public TodoStatus Status
    {
        get => _status;
        set
        {
            _status = value;
            UpdatedAt = DateTime.Now;
        }
    }

    public DateTime CreatedAt { get; } = createdAt;
    public DateTime UpdatedAt { get; private set; } = updatedAt;
}