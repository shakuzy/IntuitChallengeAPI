using IntuitChallengeAPI.Clases.Injections;
using IntuitChallengeAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IntuitChallengeAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000",
                                            "http://192.168.100.186:3000",
                                            "http://127.0.0.1:5500")
                                                              .AllowAnyHeader()
                                                              .AllowAnyMethod();
                    });
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            IConfigurationRoot configuration = new ConfigurationBuilder()
                                                  .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                                  .AddJsonFile("appsettings.json")
                                                  .Build();
            builder.Services.AddDbContext<IntuitChallengeContext>(
                           options =>
                           {
                               options.UseSqlServer($"Server={IntuitChallengeAPI.Clases.StaticFunctions.Desencriptar(configuration.GetConnectionString("Server"))};Database={IntuitChallengeAPI.Clases.StaticFunctions.Desencriptar(configuration.GetConnectionString("Database"))};User Id={IntuitChallengeAPI.Clases.StaticFunctions.Desencriptar(configuration.GetConnectionString("User_Id"))};password={IntuitChallengeAPI.Clases.StaticFunctions.Desencriptar(configuration.GetConnectionString("Password"))};Trusted_Connection=False;MultipleActiveResultSets=true;");
                           });
            builder.Services.AddScoped<Clases.Injections.ILogger, Logger>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}