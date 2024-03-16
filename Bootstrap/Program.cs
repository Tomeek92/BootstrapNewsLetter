using Bootstrap.Service.Interface;
using Bootstrap.Service;
using Bootstrap;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Dodaj us�ugi do kontenera.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ISaveEmailToDbService, SaveEmailToDbService>();

// Dodaj DbContext
var connectionString = "Server=DESKTOP-JD2U15O\\MSSQL1;Database=NewsLetterApi;Integrated Security=True; TrustServerCertificate=true;";
builder.Services.AddDbContext<BootstrapDbContext>(options =>
    options.UseSqlServer(connectionString));



var app = builder.Build();

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=NewsLetter}/{action=Index}/{id?}");

app.Run();
