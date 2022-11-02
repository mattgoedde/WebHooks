using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebHooks.Console;
using WebHooks.DependencyInjection;
using WebHooks.Services.Base;

using IHost host = Host.CreateDefaultBuilder(args)
                    .ConfigureServices((context, services) =>
                    {
                        services.AddWebhooks<SubscriberService>();
                    })
                    .Build();

await Main(host.Services);

static async Task Main(IServiceProvider services)
{
    Console.WriteLine("Hello, World!");
    var actionNotifier = services.GetRequiredService<IActionNotifier>();
    await actionNotifier.Notify(new MessageAction() { Message = "Hello WebHook!" });
    Console.WriteLine("Goodbye, World!");
};