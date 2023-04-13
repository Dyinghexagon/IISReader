using Backend.Jobs;
using Backend.Mappers;
using Backend.Models.Options;
using Backend.Repository.AccountRepository;
using Backend.Services.AccountService;
using Backend.Services.StockService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using System.Text;

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
            builder.Services.Configure<RegistrationOptions>(
                builder.Configuration.GetSection("RegistrationOptions"));
            var secret = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("RegistrationOptions")["Secret"]);
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata= false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            builder.Services.AddSingleton<IAccountsRepository,  AccountsRepository>();
            builder.Services.AddSingleton<IAccountsService,  AccountsService>();

            builder.Services.AddSingleton<AccountMapper>();

            builder.Services.AddSingleton<StocksService>();
            builder.Services.AddSingleton<SecurityMapper>();
            builder.Services.AddControllers()
                            .AddJsonOptions(
                            options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors();
            builder.Services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
                // Just use the name of your job that you created in the Jobs folder.
                var jobKey = new JobKey("NotificationJob");
                q.AddJob<NotificationJob>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("NotificationJob-trigger")
                    //This Cron interval can be described as "run every minute" (when second is zero)
                    .WithCronSchedule("0 * * ? * *")
                );
            });

            // ASP.NET Core hosting
            builder.Services.AddQuartzServer(options =>
            {
                // when shutting down we want jobs to complete gracefully
                options.WaitForJobsToComplete = true;
            });
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

            app.Run();
        }
    }
}