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
        public static bool AddCategory(Category category)
        //add to category table new category
        {
            string connString = File.ReadAllText("connectionString.txt");
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd;
            try
            {
                conn.Open();
                cmd = new MySqlCommand(@$"INSERT INTO category(`categoryName`) 
                                        VALUES ('{category.categoryName}')", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static void SearchCategory(string name)
        //search category by name and print out on console founded categories
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
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string cname = (string)reader[1];
                            Console.WriteLine($"Found category: {cname}");
                        }
                    }
                    else
                    { Console.WriteLine("Didn't find category according to the entered parameters. Keep searcing!"); }
                }
                conn.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

        }

        public static bool LoadCategories()
        //loads all categories
        {
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
                        uint categoryId = (uint)reader[0];
                        string categoryName = (string)reader[1];
                        Console.WriteLine($" ID: {categoryId}, category: {categoryName}");
                    }
                }
                conn.Close();
                return true;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
