using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Class08b.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Class08bContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Class08bContext") ?? throw new InvalidOperationException("Connection string 'Class08bContext' not found.")));

builder.Services.AddSession(option => {
    option.IdleTimeout = TimeSpan.FromMinutes(10);
    option.Cookie.Name = ".Class08b.Session";
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
