// Program.cs
using System.Text.Json.Serialization;
using dotenv.net;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        var root = Directory.GetCurrentDirectory();
        var dotenvPath = Path.Combine(root, "..", ".env");

        Console.WriteLine("Loading .env from: " + dotenvPath);

        DotEnv.Load(new DotEnvOptions(envFilePaths: [dotenvPath]));

        string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "";
        string port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
        builder.Services.AddDbContext<DatabaseContext>(option =>
        {

            option.UseSqlServer(connectionString);
            Console.WriteLine("Database connected: " + option.IsConfigured);
        });

        // DI Configuration
        InfrastructureProvider.ConfigureServices(builder);
        ServiceProvider.ConfigureServices(builder);
        UseCaseProvider.ConfigureServices(builder);

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        builder.Services.AddControllers()
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });


        var app = builder.Build();
        app.Environment.ApplicationName = "FirstNETWebApp";
        app.Environment.EnvironmentName = "Development";


        Console.WriteLine("app.Environment.IsDevelopment() " + app.Environment.IsDevelopment());
        Console.WriteLine("membershipTier: " + MembershipTierEnum.Basic.ToString());
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUi(options =>
            {
                options.DocumentPath = "/openapi/v1.json";
            });
        }


        app.MapGet("/", () => "Hello World!");
        // app.UseHttpsRedirection();
        app.MapControllers();
        app.Run($"http://localhost:{port}");
    }
}
