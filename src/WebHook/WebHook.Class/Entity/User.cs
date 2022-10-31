using WebHook.Class.Entity.Base;

namespace WebHook.Class.Entity;

public class User : EntityBase
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateTime Birthday { get; set; }
}