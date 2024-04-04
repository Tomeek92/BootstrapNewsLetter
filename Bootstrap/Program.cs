using Bootstrap.Service.Interface;
using Bootstrap.Service;
using Bootstrap;
using Microsoft.EntityFrameworkCore;
using Bootstrap.Models.Admin;
using Microsoft.AspNetCore.Identity;
using static System.Formats.Asn1.AsnWriter;
using Bootstrap.Seeder;
using Bootstrap.Models.PriceNameEdit;

var builder = WebApplication.CreateBuilder(args);

// Dodaj us³ugi do kontenera.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ISaveEmailToDbService, SaveEmailToDbService>();
builder.Services.AddScoped<IEditCennik, EditCennikService>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<Bootstrap.Seeder.Seeder>();





builder.Services.AddSession();

// Dodaj DbContext
var connectionString = "Server=DESKTOP-JD2U15O\\MSSQL1;Database=NewsLetterApi;Integrated Security=True; TrustServerCertificate=true;";
builder.Services.AddDbContext<BootstrapDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<AccountAdmin,IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.MaxFailedAccessAttempts = 5;

}).AddEntityFrameworkStores<BootstrapDbContext>();

builder.Services.Configure<SignInOptions>(options =>
{
    options.RequireConfirmedEmail = false;
    options.RequireConfirmedPhoneNumber = false;
});
var app = builder.Build();
var scope = app.Services.CreateScope();

var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
await seeder.Seed();

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

app.UseSession();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=NewsLetter}/{action=Index}/{id?}");

app.Run();
