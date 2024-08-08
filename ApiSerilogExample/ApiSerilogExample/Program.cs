
using ApiSerilogExample.Data;
using ApiSerilogExample.Services;
using ApiSerilogExample.Services.Interfaces;
using Serilog;

namespace ApiSerilogExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Error()
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();


            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddDbContext<AppDbContext>();

            // Add services to the container.

            builder.Services.AddControllers();

            // Add Serilog to the logging pipeline
            builder.Services.AddLogging(loggingBuilder =>
                             loggingBuilder.AddSerilog(dispose: true));


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
