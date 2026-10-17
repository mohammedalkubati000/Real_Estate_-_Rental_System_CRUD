

using Microsoft.EntityFrameworkCore;
using Real_Estate___Rental_System_CRUD.Application.Services;
using Real_Estate___Rental_System_CRUD.Application.Services.Base;
using Real_Estate___Rental_System_CRUD.Infrastructure.Data;
using Real_Estate___Rental_System_CRUD.Infrastructure.Repositories;
using Real_Estate___Rental_System_CRUD.Infrastructure.Repositories.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(conectionString));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
