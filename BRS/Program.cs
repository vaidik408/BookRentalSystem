using BRS.Data;
using BRS.Repository;
using BRS.Repository.Interface;
using BRS.Services;
using BRS.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using Serilog;
using System.Text;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var configuration = builder.Configuration; 
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["JwtSettings:Issuer"],
        ValidAudience = configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]))
    };
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddScoped<IRoleRepository, RoleRepository > ();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<IUserRepository, UserRepository > ();    
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IBookRepository, BookRepository> ();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddScoped<IBookStatusRepository, BookStatusRepository> ();

builder.Services.AddScoped<IBookRentalRepository, BookRentalRepository> ();
builder.Services.AddScoped<IBookRentalService, BookRentalService>();

builder.Services.AddScoped<IInventoryRepository, InventoryRepository> ();
builder.Services.AddScoped<IInventoryService, InventoryService>();

builder.Services.AddScoped<EmailService>();

builder.Services.AddScoped<AuthorizationService>();


builder.Services.AddScoped<RentalReminderJob>();

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();  

    var jobKey = new JobKey ("RentalReminderJob");
    q.AddJob<RentalReminderJob>(opts => opts.WithIdentity(jobKey));
    q.AddTrigger(opts => opts
               .ForJob(jobKey)
               .WithIdentity("OverdueRentalReminderTrigger")
                .WithCronSchedule("0 48 11 * * ? *")
                );               
});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

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
