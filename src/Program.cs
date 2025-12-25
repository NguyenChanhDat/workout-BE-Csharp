// Program.cs
using System.Text.Json.Serialization;
using dotenv.net;
using FirstNETWebApp.Infrastructure.Database.EntityFramework;
using FirstNETWebApp.Infrastructure.Provider;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        var builder = CreateBuilder(args);
        ConfigureServices(builder);
        var app = BuildApp(builder);
        ConfigureApp(app);
        app.Run(GetAppUrl());
    }

    private static WebApplicationBuilder CreateBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var root = Directory.GetCurrentDirectory();
        var dotenvPath = Path.Combine(root, "..", ".env");
        DotEnv.Load(new DotEnvOptions(envFilePaths: [dotenvPath]));
        return builder;
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
            ?? throw new InvalidOperationException("CONNECTION_STRING environment variable is required");

        builder.Services.AddDbContext<DatabaseContext>(option =>
        {
            option.UseSqlServer(connectionString);
        });

        // DI Configuration
        InfrastructureProvider.ConfigureServices(builder);
        FirstNETWebApp.Infrastructure.Provider.ServiceProvider.ConfigureServices(builder);
        UseCaseProvider.ConfigureServices(builder);
        builder.Services.AddOpenApi();
        builder.Services.AddControllers()
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
    }

    private static WebApplication BuildApp(WebApplicationBuilder builder)
    {
        var app = builder.Build();
        app.Environment.ApplicationName = "FirstNETWebApp";
        return app;
    }

    private static void ConfigureApp(WebApplication app)
    {
        // Exception handling: convert ArgumentException to 400 Bad Request with message
        app.Use(async (context, next) =>
        {
            try
            {
                await next();
            }
            catch (ArgumentException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            }
        });

        app.MapOpenApi();
        app.UseSwaggerUi(options =>
        {
            options.DocumentPath = "/openapi/v1.json";
        });
        app.MapGet("/", () => "Hello World!");
        // app.UseHttpsRedirection();
        app.MapControllers();
    }

    private static string GetAppUrl()
    {
        string port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
        return $"http://localhost:{port}";
    }
}
