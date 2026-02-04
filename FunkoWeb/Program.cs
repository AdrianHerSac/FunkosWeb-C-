using FunkoWeb.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

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
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.UseStaticFiles();

app.Run();