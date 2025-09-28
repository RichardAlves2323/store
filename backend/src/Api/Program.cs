using Microsoft.EntityFrameworkCore;
using Infrastructure.Database;
using Domain.Interfaces.Repositories;
using Infrastructure.Database.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using Domain.Interfaces.HashPassword;
using Infrastructure.HashPassword;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()                     
            .AllowAnyMethod();                    
    });
});

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
builder.Services.AddScoped<IHashPassword, HashPasswordByBcrypt>();

var key = "minha-chave-secreta-super-segura-123!";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        RoleClaimType = ClaimTypes.Role
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Store API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT desta forma: Bearer {seu token}"
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
            Array.Empty<string>()
        }
    });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

await SeedData.EnsureAdminUser(app.Services);

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();





if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
