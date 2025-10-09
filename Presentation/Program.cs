using Infraestructure.Interface;
using Infraestructure.Service;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var tessdataPath = Path.Combine(Directory.GetCurrentDirectory(), "tessdata");
if (!Directory.Exists(tessdataPath))
{
    Directory.CreateDirectory(tessdataPath);
    Console.WriteLine("ADVERTENCIA: Carpeta tessdata creada. Coloca spa.traineddata ahí.");
}

// Registrar servicios
builder.Services.AddScoped<IOCRService, OCRService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
