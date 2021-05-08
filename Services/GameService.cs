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
    public static class GameService
    {
        public static bool AddGame(Game game)
        {

            string connString = File.ReadAllText("connectionString.txt");
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd;
            try
            {
                conn.Open();
                cmd = new MySqlCommand($"INSERT INTO games(`gameName`, `availability`) VALUES ('{game.gameName}', 1)", conn);
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

        public static void SearchGameByName(string name)
        {
            string connString = File.ReadAllText("connectionString.txt");
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd;
            try
            {
                conn.Open();
                cmd = new MySqlCommand($"SELECT * FROM games WHERE gameName LIKE '%{name}%'", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string gname = (string)reader[1];
                        sbyte avail = (sbyte)reader[2];
                                              
                        bool isAvailable = true;
                        if (avail == (sbyte)0)
                        {
                            isAvailable = false;
                
                        }
                        Console.Write("Found game: "+ gname + " , availability: ");
                        Console.WriteLine(isAvailable ? "available" : "not available");
                        
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

        public static bool TakeGame(uint userId, uint gameId)
        {
            throw new NotImplementedException();
            // remember about timestamps
        }

        public static bool ReturnGame(uint userId, uint gameId)
        {
            throw new NotImplementedException();
            // remember about timestamps
        }
    }
}
