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
    public static class UserService
    {
        public static bool AddUser(User user)
        {
            string connString = File.ReadAllText("connectionString.txt");
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd;
            try
            {
                conn.Open();
                cmd = new MySqlCommand(@$"INSERT INTO users(`name`, `surname`) 
                                        VALUES ('{user.Name}', '{user.Surname}')", conn);
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

        public static void SearchUserByName(string name)
        {
            string connString = File.ReadAllText("connectionString.txt");
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd;
            try
            {

                conn.Open();
                cmd = new MySqlCommand($"SELECT * FROM users WHERE name LIKE '%{name}%'", conn);


                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string userName = (string)reader[1];
                            string userSurname = (string)reader[2];
                            DBNull userTelephoneNumber = (DBNull)reader[3];
                            DBNull userEmail = (DBNull)reader[4];

                            Console.WriteLine($"Found user: {userName}  {userSurname}  {userTelephoneNumber}  {userEmail}.");
                        }
                    }
                    else
                    { Console.WriteLine("Didn't find such user according to the entered parameters. Keep searcing!"); }
                }
                conn.Close();


            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);

            }
        }
    }
}
