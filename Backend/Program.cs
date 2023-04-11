using Backend.Mappers;
using Backend.Models.Options;
using Backend.Repository;
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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

            builder.Services.AddSingleton<IAccountRepository,  AccountRepository>();

            builder.Services.AddSingleton<AccountService>();
            builder.Services.AddSingleton<AccountMapper>();

            builder.Services.AddSingleton<SecurityService>();
            builder.Services.AddSingleton<SecurityMapper>();
            builder.Services.AddControllers()
                            .AddJsonOptions(
                            options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors();

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