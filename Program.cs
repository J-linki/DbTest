using DbTest.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace DbTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<TestContext>();


            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            using var _dbContext = scope.ServiceProvider.GetRequiredService<TestContext>();

            if (_dbContext.Database.CanConnect()) Console.WriteLine("Can connect to the db");
            /*

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

            
            app.Run();*/
        }
    }
}
