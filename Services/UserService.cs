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
            throw new NotImplementedException();
            // return true ja sanaak, false ja nesanaak
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
                    while (reader.Read())
                    {
                        string userName = (string)reader[1];
                        string userSurname = (string)reader[2];
                        DBNull userTelephoneNumbername = (DBNull)reader[3];
                        DBNull userEmail = (DBNull)reader[4];

                        Console.WriteLine($"Found user: {userName}; {userSurname}, {userTelephoneNumbername}, {userEmail}.");
                    }
                }
                conn.Close();


            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                //return e;
            }
        }
    }
}
