using BRS.Data;
using BRS.Repository;
using BRS.Repository.Interface;
using BRS.Services;
using BRS.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRoleRepository, RoleRepository > ();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<IUserRepository, UserRepository > ();    
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IBookRepository, BookRepository> ();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddScoped<IBookStatusRepository, BookStatusRepository> ();

builder.Services.AddScoped<IBookRentalRepository, BookRentalRepository> ();
builder.Services.AddScoped<IBookRentalService, BookRentalService>();

builder.Services.AddScoped<EmailService>();


IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

builder.Services.AddDbContext<BRSDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("BRS")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
