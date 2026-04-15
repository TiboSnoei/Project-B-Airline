// using System.Runtime.InteropServices;
// using System.Security.Cryptography.X509Certificates;
// using Microsoft.Data.Sqlite;

// class SearchoverviewAcces
// {
//     public string Query;
//     public const string DatabaseLoc = "data/airline.db";
//     public string cs = $"Data Source={DatabaseLoc}";
//     public SearchoverviewAcces(string query)
//     {
//         Query = query;
//         using var connection = DatabaseConnector.Open(cs);
//         cmd = new SqliteCommand(query);
//     }
// }
