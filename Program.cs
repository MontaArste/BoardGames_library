using System;
using System.IO;
using BG_library.Entities;
using BG_library.Services;


// dotnet add package MySql.Data
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BG_library
{
    class Program
    {
        static void Main(string[] args)
        {
            TestDataInserter.InsertTestData();

            string connString = File.ReadAllText("connectionString.txt");
            MySqlConnection conn = new MySqlConnection(connString);

            try
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM category", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var category = new Category()
                        {
                            Id = (uint)reader[0],
                            categoryName = reader[1].ToString()
                        };
                        Console.WriteLine(category.ToString());
                    }
                }
                Game gameToAdd = new Game() { gameName = "The Moon", availability = 1 };

                //GameService.AddGame(gameToAdd);

                GameService.TakeGame(2, 25);
                GameService.TakeGame(5, 23);

            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
    }
}
