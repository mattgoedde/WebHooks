namespace WebHook.Class.Entity;

public class User
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateOnly Birthday { get; set; }
}