using Bootstrap.Service.Interface;
using Bootstrap.Service;
using Bootstrap;
using Microsoft.EntityFrameworkCore;
using Bootstrap.Models.Admin;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Dodaj us³ugi do kontenera.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ISaveEmailToDbService, SaveEmailToDbService>();

// Dodaj DbContext
var connectionString = "Server=DESKTOP-JD2U15O\\MSSQL1;Database=NewsLetterApi;Integrated Security=True; TrustServerCertificate=true;";
builder.Services.AddDbContext<BootstrapDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentityCore<AccountAdmin>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    
}).AddEntityFrameworkStores<BootstrapDbContext>();

builder.Services.Configure<SignInOptions>(options =>
{
    options.RequireConfirmedEmail = false;
    options.RequireConfirmedPhoneNumber = false;
});



var app = builder.Build();

// Konfiguruj potok ¿¹dania HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Domyœlna wartoœæ HSTS wynosi 30 dni. Mo¿esz j¹ zmieniæ dla scenariuszy produkcyjnych, zobacz: https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=NewsLetter}/{action=Index}/{id?}");

app.Run();
