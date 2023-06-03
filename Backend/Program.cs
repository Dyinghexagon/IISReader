using Backend.Hubs.NotificationHub;
using Backend.Jobs;
using Backend.Mappers;
using Backend.Models.Options;
using Backend.Repository.AccountRepository;
using Backend.Repository.StockRepository;
using Backend.Services.AccountService;
using Backend.Services.ArchiveStockService;
using Backend.Services.StockService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Quartz;
using System.Text.Json.Serialization;
using System.Text.Json;
using Backend.Services;

namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.Configure<DatabaseOptions>(
                builder.Configuration.GetSection("DatabaseOptions"));

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = RegistrationOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = RegistrationOptions.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = RegistrationOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });

            builder.Services.AddSingleton<IAccountsRepository,  AccountsRepository>();
            builder.Services.AddSingleton<IArchiveStocksRepository,  AchiveStocksRepository>();

            builder.Services.AddSingleton<IAccountsService,  AccountsService>();
            builder.Services.AddSingleton<IActualStocksService, ActualStocksService>();
            builder.Services.AddSingleton<IArchiveStockService, ArchiveStockService>();

            builder.Services.AddSingleton<AccountMapper>();
            builder.Services.AddSingleton<StockListMapper>();
            builder.Services.AddSingleton<ActualStockMapper>();
            builder.Services.AddSingleton<ArchiveDataMapper>();


            builder.Services.AddControllers()
                            .AddJsonOptions(options =>
                            {
                                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors();
            builder.Services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
                // Just use the name of your job that you created in the Jobs folder.
                var notificationJobKey = new JobKey("NotificationJob");
                var acrchiveStockJob = new JobKey("ArchiveStockJob");

                q.AddJob<NotificationJob>(opts => opts.WithIdentity(notificationJobKey));
                q.AddJob<ArchiveStockJob>(opts => opts.WithIdentity(acrchiveStockJob));

                q.AddTrigger(opts => opts
                    .ForJob(notificationJobKey)
                    .WithIdentity("NotificationJob-trigger")
                    //This Cron interval can be described as "run every minute" (when second is zero)
                    .WithCronSchedule("0 * * ? * *")
                    //.WithCronSchedule("0 0/15 * 1/1 * ? *")
                );

                q.AddTrigger(opts => opts
                    .ForJob(acrchiveStockJob)
                    .WithIdentity("ArchiveStockJob-trigger")
                    //This Cron interval can be described as "run every minute" (when second is zero)
                    .WithCronSchedule("0 * * ? * *")
                //.WithCronSchedule("0 0 19 ? * MON-FRI *")
                );
            });

            // ASP.NET Core hosting
            builder.Services.AddQuartzServer(options =>
            {
                // when shutting down we want jobs to complete gracefully
                options.WaitForJobsToComplete = true;
            });

            builder.Services.AddSignalR();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(options =>
            {
                options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.MapHub<NotificationHub>("/notification");

            app.Run();
        }
    }
}