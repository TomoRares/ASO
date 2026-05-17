using Microsoft.EntityFrameworkCore;
using TodoApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TodoList"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
    c.RoutePrefix = "swagger"; // Swagger va fi la adresa /swagger
});

app.MapGet("/", () => "Serverul ASP.NET Core rulează cu succes! Mergi la /swagger pentru interfață.");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();