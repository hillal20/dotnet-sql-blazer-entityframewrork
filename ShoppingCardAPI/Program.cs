using Microsoft.EntityFrameworkCore;
using ShoppingCardAPI.DataAccess;
using ShoppingCardAPI.Repositories.Contracts;
using ShoppingCardAPI.Repositories;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


/////////////////////////////////////// regestring the data repository  ///////////////
///
builder.Services.AddDbContextPool<Repository>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ShoppingCardDBURL")));

/////////////////////////////////////////////////////////////////////////////////////////////////



/////////////////////////////////////// regestring product respository to the dependecy injection CDN  ///////////////
///
builder.Services.AddScoped<IProductRepository, ProductRepository>();

/////////////////////////////////////////////////////////////////////////////////////////////////













/// <summary>
/// 
/// </summary>

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

