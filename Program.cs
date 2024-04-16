using Microsoft.EntityFrameworkCore;
using Polly;
using SAP_Assignment3.Data;
namespace SAP_Assignment3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<SAP_Assignment3Context>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SAP_Assignment3Context") ?? throw new InvalidOperationException("Connection string 'SAP_Assignment3Context' not found."));
            });
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpClient("RetryClient")
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    return new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    };
                })
                .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10)
                }));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                //pattern: "{controller=Home}/{action=Index}/{id?}");
                pattern: "{controller=FinalProject}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
