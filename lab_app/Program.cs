namespace lab_app;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        app.MapStaticAssets();

        app.MapControllerRoute("books-by-year", "books/year/{year:int:min(1450)}",
            new { controller = "Book", action = "ByYear" });

        app.MapControllerRoute("books-by-isbn", "books/isbn/{isbn:regex(^\\d{{13}}$)}",
            new { controller = "Book", action = "ByIsbn" });

        app.MapControllerRoute("docs", "docs/{*path}",
            new { controller = "Home", action = "Docs" });

        app.MapControllerRoute("admin-area", "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

        app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}