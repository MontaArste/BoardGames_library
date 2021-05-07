using BG_library.Entities;
using System;
using System.IO;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                cmd = new MySqlCommand($"INSERT INTO games (`gameName`, `availability`) VALUES ('{game.gameName}', 1)", conn);
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

        public static Game SearchGameByName(string name)
        {
            throw new NotImplementedException();
        }
        public static bool CheckGameAvailability(uint gameId)
        {

            string connString = File.ReadAllText("connectionString.txt");
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd;
            try
            {
                conn.Open();
                cmd = new MySqlCommand($"SELECT `availability` FROM games WHERE `id` = {gameId}", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();

                    var available = reader.GetValue(0);
                    if (available.Equals(true))
                    {
                        Console.WriteLine("This game is available.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Sorry, this game is not available!");
                        return false;
                    }
                }
            }

            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public static bool TakeGame(uint userId, uint gameId)
        {
            string connString = File.ReadAllText("connectionString.txt");
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd;
            try
            {
                conn.Open();
                bool availability = CheckGameAvailability(gameId);
                if (availability.Equals(true))
                {
                    cmd = new MySqlCommand($"INSERT INTO gamesinuse (`gameId`, `userId`, `timeTaken`) VALUES ('{gameId}', '{userId}','{DateTime.Now.ToString("yyyy-MM-dd")}')", conn);
                    cmd.ExecuteNonQuery();
                    cmd = new MySqlCommand($"UPDATE games SET `availability` = '0' WHERE `id` = {gameId}", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Console.WriteLine("You can use the game for 30 days. Have fun!");
                    return true;
                }
                return false;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static bool ReturnGame(uint userId, uint gameId)
        {
            throw new NotImplementedException();
            // remember about timestamps
        }
    }
}
