using WebHook.Class.Entity;

namespace WebHook.Data.Test;

public static class DbInitializer
{
    public static void Initialize(WebHookContext context)
    {
        if (context.Users.Any()) return;

        var users = new User[]
        {
            new User { FirstName = "Matthew", LastName = "Goedde", Birthday = DateTime.Parse("1997-11-19") },
            new User { FirstName = "Lexi", LastName = "Goedde", Birthday = DateTime.Parse("1999-02-02") }
        };

        context.Users.AddRange(users);
        context.SaveChanges();
    }
}