using ServiceMicroService.Extensions;

namespace ServiceMicroService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var config = builder.Configuration;

        builder.Services.ConfigureJwtAuthentication(config);
        builder.Services.ConfigureDbConnection(config);
        builder.Services.ConfigureMassTransit(config);
        builder.Services.ConfigureServices();
        builder.Services.ConfigureSwagger();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}