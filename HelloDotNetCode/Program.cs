using System;
using System.Data.SqlClient;

namespace HelloDotNetCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using (var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SampleDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                var command = new SqlCommand("SELECT TOP 10 Id, Name, Category FROM Product", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[0]}:{reader[1]} ${reader[2]}");
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
