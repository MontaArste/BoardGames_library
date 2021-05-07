using BG_library.Entities;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// dotnet add package MySql.Data
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BG_library.Services
{
    class CategoryService
    {
        public static bool AddCategory( Category category)
        {
            throw new NotImplementedException();
        }

        public static void SearchCategory(string name)
        {
            string connString = File.ReadAllText("connectionString.txt");
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd;
            try
            {
                conn.Open();
                cmd = new MySqlCommand($"SELECT * FROM category WHERE categoryName LIKE '%{name}%'", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string cname = (string)reader[1];
                        Console.WriteLine($"Found category: {cname}");
                    }
                }
                conn.Close();
                
                
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            
        }
    }
}
