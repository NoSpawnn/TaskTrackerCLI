namespace TaskTrackerCLI;

public class Todo(int id, string description, DateTime createdAt, DateTime updatedAt, TodoStatus status)
{
    public int Id { get; } = id; // IDs start at 1
    private string _description = description;
    public string Description
    {
        get { return _description; }
        set
        {
            _description = value;
            UpdatedAt = DateTime.Now;
        }
    }
    private TodoStatus _status = status;
    public TodoStatus Status
    {
        get { return _status; }
        set
        {
            _status = value;
            UpdatedAt = DateTime.Now;
        }
    }
    public DateTime CreatedAt { get; } = createdAt;
    public DateTime UpdatedAt { get; set; } = updatedAt;
}