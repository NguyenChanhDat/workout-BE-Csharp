// Program.cs
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

        var app = builder.Build();

        // DI Configuration
        InfrastructureProvider.ConfigureServices(builder);
        ServiceProvider.ConfigureServices(builder);

        app.MapGet("/", () => "Hello World!");
        app.Run($"http://localhost:{port}");
    }
}
