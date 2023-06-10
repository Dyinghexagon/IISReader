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

namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors();
            builder.Services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
                var notificationJobKey = new JobKey("NotificationJob");
                var acrchiveStockJob = new JobKey("ArchiveStockJob");

                q.AddJob<NotificationJob>(opts => opts.WithIdentity(notificationJobKey));
                q.AddJob<ArchiveStockJob>(opts => opts.WithIdentity(acrchiveStockJob));

                q.AddTrigger(opts => opts
                    .ForJob(notificationJobKey)
                    .WithIdentity("NotificationJob-trigger")
                    .WithCronSchedule("0 0/15 * 1/1 * ? *")
                );

                q.AddTrigger(opts => opts
                    .ForJob(acrchiveStockJob)
                    .WithIdentity("ArchiveStockJob-trigger")
                    .WithCronSchedule("0 0 19 ? * MON-FRI *")
                );
            });

            builder.Services.AddQuartzServer(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            builder.Services.AddSignalR();

            var app = builder.Build();

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