using Microsoft.EntityFrameworkCore;
using Product.API.Project.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ProductContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("ProductContextDb")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
    // Expose the Program class for use with WebApplicationFactory<T>
}