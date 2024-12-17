using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Dodanie usług do kontenera DI (Dependency Injection)
builder.Services.AddControllersWithViews()
        .AddJsonOptions(options =>
        {
            // Configure JSON serializer to use camelCase (lowercase for the first letter)
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

var app = builder.Build();

// Konfiguracja potoku HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Bezpieczeństwo HSTS dla produkcji
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Obsługa plików statycznych (CSS, JS, obrazy)

app.UseRouting();

app.UseAuthorization();

// Konfiguracja domyślnej trasy
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
