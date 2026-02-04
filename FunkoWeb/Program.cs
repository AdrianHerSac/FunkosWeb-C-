using FunkoWeb.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Configurar autenticación por cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.Name = ".FunkoAuth";
    });

builder.Services.AddAuthorization();

builder.Services.AddDistributedMemoryCache(); // Requisito técnico
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = ".FunkoWorld.Session";
});

var app = builder.Build();
app.UseSession();

InMemoryData.Seed();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/ErrorDelServidor");
    app.UseHsts();
}
else 
{
    app.UseExceptionHandler("/Home/ErrorDelServidor");
}
app.UseStatusCodePagesWithReExecute("/Home/PaginaNoEncontrada");

app.UseHttpsRedirection();
app.UseRouting();

// ⚠️ EL ORDEN ES CRÍTICO: Authentication ANTES de Authorization
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.UseStaticFiles();

app.Run();