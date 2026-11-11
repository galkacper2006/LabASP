using lab_app.Middleware;
using lab_app.Filters;

namespace lab_app;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews(options =>
        {
            options.Filters.Add<GlobalExceptionFilter>();
        });

        builder.Services.AddScoped<GlobalExceptionFilter>();
        builder.Services.AddScoped<LogActionFilter>();
        builder.Services.AddSingleton<RequestStats>();

        var app = builder.Build();

        app.UseMiddleware<RequestTimingMiddleware>();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        
        app.UseHttpsRedirection();

        app.UseStatusCodePagesWithReExecute(
            "/Home/HttpStatus",
            "?code={0}");

        app.UseRouting();
        app.UseAuthorization();


        app.MapStaticAssets();

        app.MapControllerRoute("books-by-year", "books/year/{year:int:min(1450)}",
            new { controller = "Book", action = "ByYear" });

        app.MapControllerRoute("books-by-isbn", "books/isbn/{isbn:regex(^\\d{{13}}$)}",
            new { controller = "Book", action = "ByIsbn" });
        
        app.MapControllerRoute(
            "archive",
            "archive/{year:int}/{month:int:range(1,12)}",
            new { controller = "Book", action = "Archive" });


        app.MapControllerRoute("docs", "docs/{*path}",
            new { controller = "Home", action = "Docs" });

        app.MapControllerRoute("admin-area",
            "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

        app.MapControllerRoute("default",
                "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}