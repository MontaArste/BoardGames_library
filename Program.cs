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
            //TestDataInserter.InsertTestData();

            string connString = File.ReadAllText("connectionString.txt");
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd;
            try
            {
                conn.Open();
                cmd = new MySqlCommand("SELECT * FROM category", conn);
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
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return;
            }


            //cmd = new MySqlCommand("INSERT INTO category (`categoryName`) VALUES ('classical'), ('party')", conn);
            //cmd.ExecuteNonQuery(); // non-query for INSERT, UPDATE, DELETE and others that do not return dataset.
            /* cmd = new MySqlCommand("SELECT * FROM category", conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string name = (string)reader[1];
                    Console.WriteLine("category: " + name);
                }
            } */

            //cmd = new MySqlCommand("SELECT LOCATE('gameToSearch', 'foreach game in') AS MatchPosition;",conn);
            //cmd = new MySqlCommand("DELETE FROM category WHERE id = 1", conn);
            //cmd.ExecuteNonQuery();
            /* cmd = new MySqlCommand("SELECT * FROM category WHERE categoryName LIKE '%par%'", conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string name = (string)reader[1];
                    Console.WriteLine("category: " + name);
                }
            } */
            
            conn.Close();
            GameService.SearchGameByName("zirgs");
            //CategoryService.SearchCategory("par");

            


           
        }
    }
}
