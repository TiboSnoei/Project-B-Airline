public class Program
{
    public static int Main(string[] args)
    {
        var seeder = new DatabaseSeeder();
        seeder.databaseSeeder();
        return 0;
    }
}