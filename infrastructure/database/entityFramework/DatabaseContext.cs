using Microsoft.EntityFrameworkCore;
using dotenv.net;

class DatabaseContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        DotEnv.Load();

        string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "";
        optionsBuilder.UseSqlServer(connectionString);
    }

    #region Required
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
        new ExerciseEntityTypeConfiguration().Configure(modelBuilder.Entity<Exercise>());
    }
    #endregion
}