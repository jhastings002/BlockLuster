using BlockLuster.Common;
using DbUp;
using System.Reflection;


var connectionString =
    args.FirstOrDefault()
    ?? Config.SqlServerConnectionString;

EnsureDatabase.For.SqlDatabase(connectionString);

var upgrader =
    DeployChanges.To
        .SqlDatabase(connectionString)
        .JournalToSqlTable("dbo", "SchemaVersion")
        .WithTransaction()
        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
        .LogToConsole()
        .Build();

var result = upgrader.PerformUpgrade();

if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(result.Error);
    Console.ResetColor();
#if DEBUG
    Console.ReadLine();
#endif                
    return -1;
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Success!");
Console.ResetColor();
return 0;
