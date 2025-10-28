using Microsoft.EntityFrameworkCore;
using NetworkingWebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
// Configure CORS: allow all origins, headers and methods

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();
app.UseCors(x=>x.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()); 
app.MapControllers();

app.Run();
