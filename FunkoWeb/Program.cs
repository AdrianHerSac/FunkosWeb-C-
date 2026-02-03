using FunkoWeb.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

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