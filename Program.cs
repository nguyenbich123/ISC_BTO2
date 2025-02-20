using System.Text;
using AuthorizationAPI.Data;
using AuthorizationAPI.Repositories.IRepo;
using AuthorizationAPI.Repositories;
using AuthorizationAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Cấu hình DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// Cấu hình Authentication với JWT
builder.Services.AddSingleton<JwtService>();

builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAllowAccessRepository, AllowAccessRepository>();
builder.Services.AddScoped<IInternRepository, InternRepository>();

builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AllowAccessService>();
builder.Services.AddScoped<InternService>();



builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
