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
            //TestDataInserter.InsertTestData();  //inserts data in database from excel document

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


                       
            conn.Close();
            GameService.SearchGameByName("oo");
            CategoryService.SearchCategory("par");
            UserService.SearchUserByName("mar");
            //User user = new User("Māris", "Čaklais");
            //UserService.AddUser(user);
            


           
        }
    }
}
