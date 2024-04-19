using Microsoft.AspNetCore.Identity;
using OrdersManager;
using OrdersManager.DbContexts;
using OrdersManager.Models;
using OrdersManager.ModelsActions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<OrdersContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddSingleton<RegisterUser>();
builder.Services.AddSingleton<RegisterSuperUser>();
builder.Services.AddSingleton<UserCredentialsCheck>();
builder.Services.AddSingleton<Boot>();
builder.Services.AddHostedService(services => services.GetService<Boot>()!);

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
