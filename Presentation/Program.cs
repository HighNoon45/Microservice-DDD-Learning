using Domain.Interfaces.Repositories;
using Domain.Services;
using Infrastructure;
using Infrastructure.Repositories;


namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddScoped<IArticleRepo, ArticleRepo>();
            builder.Services.AddScoped<IPricingRepo, PricingRepo>();

            builder.Services.AddScoped<IArticleService, ArticleService>();
            builder.Services.AddScoped<IPricingService, PricingService>();

            builder.Services.AddDbContext<AppDbContext>();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
