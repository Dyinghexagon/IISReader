using Backend.Models;
using Backend.Services;

namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<IISReaderDatabaseSettings>(
                builder.Configuration.GetSection("IISReaderDatabaseSettings"));

            builder.Services.AddSingleton<UserService>();

            builder.Services.AddControllers()
                .AddJsonOptions(
                options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}