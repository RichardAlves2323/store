using Microsoft.EntityFrameworkCore;
using Infrastructure.Database;
using Domain.Interfaces.Repositories;
using Infrastructure.Database.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source= Store.db"));

builder.Services.AddScoped<IProductRepository, ProductRepositoryByDbContext>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserRepository, UserRepositoryByDbContext>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStockRepository, StockRepositoryByDbContext>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IStockMovementRepository, StockMovementRepositoryByDbContext>();
builder.Services.AddScoped<IStockMovementService, StockMovementService>();
builder.Services.AddScoped<IOrderRepository, OrderRepositoryByDbContext>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

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
