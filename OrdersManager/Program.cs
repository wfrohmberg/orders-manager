using Microsoft.AspNetCore.Identity;
using OrdersManager;
using OrdersManager.ControllersActions;
using OrdersManager.DbContexts;
using OrdersManager.Models;
using OrdersManager.ModelsActions;
using OrdersManager.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<OrdersContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.OperationFilter<AddRequiredHeaderParameter>());
builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddSingleton<RegisterUser>();
builder.Services.AddSingleton<RegisterSuperUser>();
builder.Services.AddSingleton<UserCredentialsCheck>();
builder.Services.AddSingleton<GenerateEncryptedToken>();
builder.Services.AddSingleton<CheckTokenExpiration>();
builder.Services.AddSingleton<GetTokenLogin>();
builder.Services.AddSingleton<AddUserAddress>();
builder.Services.AddSingleton<GetUserAddresses>();
builder.Services.AddSingleton<DeleteUserAddress>();
builder.Services.AddSingleton<RestoreUserAddress>();
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
