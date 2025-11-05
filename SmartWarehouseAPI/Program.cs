using Microsoft.EntityFrameworkCore;
using SmartWarehouseAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔗 Conexión a MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("SmartWarehouseDB"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("SmartWarehouseDB"))));

var app = builder.Build();

// Habilita Swagger (documentación automática)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
