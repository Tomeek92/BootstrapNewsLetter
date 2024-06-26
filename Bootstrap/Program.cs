using Bootstrap;
using Bootstrap.Models.Admin;
using Bootstrap.Seeder;
using Bootstrap.Service;
using Bootstrap.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Dodaj us�ugi do kontenera.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ISaveEmailToDbService, SaveEmailToDbService>();
builder.Services.AddScoped<IEditCennik, EditCennikService>();
builder.Services.AddScoped<IEditSzkolenie, EditSzkolenieService>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<Bootstrap.Seeder.Seeder>();





builder.Services.AddSession();

// Dodaj DbContext
var connectionString = "Server=mssql5.webio.pl,2401;Database=tomczi123_testMarcelina;User Id=tomczi123_;Password=Tomeklol123!;TrustServerCertificate=True;Encrypt=False;";
builder.Services.AddDbContext<BootstrapDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<AccountAdmin, IdentityRole>(options =>
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

// Konfiguruj potok ��dania HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Domy�lna warto�� HSTS wynosi 30 dni. Mo�esz j� zmieni� dla scenariuszy produkcyjnych, zobacz: https://aka.ms/aspnetcore-hsts.
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
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
