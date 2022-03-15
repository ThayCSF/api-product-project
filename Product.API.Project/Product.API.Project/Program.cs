using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.API.Project.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ProductContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("ProductContextDb")));
builder.Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMvc();

app.MapControllers();

app.Run();