using Library.App.Configurations;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
            
builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRouting();
app.MapControllers();
app.Run();
