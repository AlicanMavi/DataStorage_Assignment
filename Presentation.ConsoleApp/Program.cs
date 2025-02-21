

using Business.Services;
using Presentation.ConsoleApp;
using Data.Contexts;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


var services = new ServiceCollection()
    .AddDbContext<DataContext>(x => x.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\DataStorage_Assignment\\Data\\Databases\\local_database.mdf;Integrated Security=True"))
    .AddScoped<CustomerRepository>()
    .AddScoped<ProjectRepository>()
    .AddScoped<CustomerService>()
    .AddScoped<ProjectService>()
    .AddScoped<MenuDialogs>()
    .BuildServiceProvider();

var menuDialogs = services.GetRequiredService<MenuDialogs>();
await menuDialogs.MenuOptions();
