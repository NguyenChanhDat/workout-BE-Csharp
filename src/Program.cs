// Program.cs
class Program
{
    static void Main()
    {
        using var db = new DatabaseContext();
        Console.WriteLine("db Connect: " + db.Database.CanConnect());
        // Console.WriteLine("Database ready");
    }
}
