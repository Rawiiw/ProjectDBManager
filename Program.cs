using Microsoft.EntityFrameworkCore;
using ProjectDBManager.Models.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json");

// Add control data services
builder.Services.AddDbContext<EDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();