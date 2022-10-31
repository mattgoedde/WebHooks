using Microsoft.EntityFrameworkCore;
using WebHook.Class.Entity;

namespace WebHook.Data;

public class WebHookContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseInMemoryDatabase(databaseName: "WebHook");
    }
}