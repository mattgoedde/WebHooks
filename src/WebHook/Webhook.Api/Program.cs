
using WebHook.Class.Event;
using WebHook.Data;
using WebHook.Data.Test;
using WebHook.Logic;
using WebHook.Logic.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IEventNotifier<EntityChangedEvent>, EntityChangedNotifier>();

builder.Services.AddDbContext<WebHookContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<WebHookContext>();
    DbInitializer.Initialize(context);
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
