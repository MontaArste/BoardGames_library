﻿using System;
using System.IO;
// dotnet add package MySql.Data
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BG_library
{
    class Program
    {
        static void Main()
        {
            string connString = File.ReadAllText("connectionString.txt");
            MySqlConnection conn = new MySqlConnection(connString);

         
            try
            {
                conn.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            MySqlCommand cmd;

            //cmd = new MySqlCommand("INSERT INTO category (`categoryName`) VALUES ('classical'), ('party')", conn);
            //cmd.ExecuteNonQuery(); // non-query for INSERT, UPDATE, DELETE and others that do not return dataset.
            cmd = new MySqlCommand("SELECT * FROM category",conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {while (reader.Read())
            {string name = (string) reader[1];
            Console.WriteLine("category: " + name);}}
            
            //cmd = new MySqlCommand("SELECT LOCATE('gameToSearch', 'foreach game in') AS MatchPosition;",conn);
            //cmd = new MySqlCommand("DELETE FROM category WHERE id = 1", conn);
            //cmd.ExecuteNonQuery();
            cmd = new MySqlCommand("SELECT * FROM category WHERE categoryName LIKE '%par%'",conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {while (reader.Read())
            {string name = (string) reader[1];
            Console.WriteLine("category: " + name);}}
            conn.Close();
        }
    }
}
