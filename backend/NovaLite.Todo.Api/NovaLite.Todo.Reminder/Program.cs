using Microsoft.EntityFrameworkCore;
using NovaLite.Todo.Core.Data;
using NovaLite.Todo.Reminder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NovaLite.Todo.Core.Interfaces;
using NovaLite.Todo.Core.Repositories;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostContext, config) =>
    {
        // Load appsettings.json and other configuration files
        config.AddJsonFile("appsettings.json", optional: true);
        // You can add more configuration sources if needed
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();

        // Retrieve the connection string from the configuration
        IConfiguration configuration = hostContext.Configuration;
        string connectionString = configuration.GetConnectionString("MyDatabase");

        services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IToDoRepository, ToDoRepository>();

    })
    .Build();

await host.RunAsync();
