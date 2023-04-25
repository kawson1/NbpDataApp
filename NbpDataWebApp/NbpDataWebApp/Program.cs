using NbpDataWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<INbpDataService, NbpDataService>();
builder.Services.AddHttpClient<INbpDataService, NbpDataService>(client =>
{
    client.BaseAddress = new Uri("http://api.nbp.pl");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=NbpData}/{action=exchanges}");
});

app.Run();
