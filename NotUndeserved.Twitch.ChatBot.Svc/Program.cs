using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotUndeserved.Twitch.ChatBot.Application;
using NotUndeserved.Twitch.ChatBot.Infrastructure;
using NotUndeserved.Twitch.ChatBot.Infrastructure.Services;
using NotUndeserved.Twitch.ChatBot.Persistence;

IHostBuilder hostBuilder = new HostBuilder();
hostBuilder.ConfigureAppConfiguration((hostContext, builder) => {
    builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
});

hostBuilder.ConfigureServices((hostContext, services) => {
    services.AddApplication(hostContext.Configuration);
    services.AddPersistence(hostContext.Configuration);
    services.AddTwitchApiService(hostContext.Configuration);
    services.AddTwitchClient(hostContext.Configuration);
});

IHost host =  hostBuilder.Build();
TwitchChatService chatService = host.Services.GetRequiredService<TwitchChatService>();
Console.ReadLine();