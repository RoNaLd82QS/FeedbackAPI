var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 👇 Registro del servicio personalizado y cliente feedback
builder.Services.AddHttpClient<NewsPortal.Services.JsonPlaceholderService>();

builder.Services.AddHttpClient("feedback", client =>
{
    client.BaseAddress = new Uri("http://localhost:5000"); // Ajusta si es otro puerto
});

var app = builder.Build();

// Configure the HTTP request pipeline.
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
