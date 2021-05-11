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
        public static void AddGameCategory(uint gameId, uint categoryId)
        {
            string connString = File.ReadAllText("connectionString.txt");
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd;
            try
            {
                conn.Open();
                cmd = new MySqlCommand($"INSERT INTO gamecategory (`gameID`, `categoryID`) VALUE ('{gameId}','{categoryId}')", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                Console.WriteLine("The game category has been successfully added.");
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static bool AddGame(Game game, uint categoryId)
        {
            string connString = File.ReadAllText("connectionString.txt");
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd;
            try
            {
                conn.Open();
                cmd = new MySqlCommand($"INSERT INTO games (`gameName`, `availability`) VALUES ('{game.gameName}', 1)", conn);
                cmd.ExecuteNonQuery();
                uint newGameId;
                cmd = new MySqlCommand($"SELECT `id` FROM games WHERE `gameName`='{game.gameName}'", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    newGameId = (uint)reader.GetValue(0);
                    Console.WriteLine(newGameId);
                }
                Console.WriteLine($"The game {game.gameName} has been successfully added to catalog!");
                AddGameCategory(newGameId, categoryId);
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
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            uint id = (uint)reader[0];
                            string gname = (string)reader[1];
                            bool avail = (bool)reader[2];

                            bool isAvailable = true;
                            if (avail == false)
                            {
                                isAvailable = false;

                            }
                            Console.Write("Found game: " + gname + ", game ID: " + id + ", availability: ");
                            Console.WriteLine(isAvailable ? "available" : "not available");

                        }
                    }
                    else
                    {
                        Console.WriteLine("Didn't find game according to the entered parameters. Keep searcing!");
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
            string connString = File.ReadAllText("connectionString.txt");
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd;
            uint isNotReturned;
            try
            {
                conn.Open();
                cmd = new MySqlCommand($"SELECT `id` FROM gamesinuse WHERE `gameID`={gameId} AND `userID`={userId} AND`timeReturned`IS null", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())

                {
                    reader.Read();
                    isNotReturned = (uint)reader.GetValue(0);

                    Console.WriteLine($"{isNotReturned}");

                }
                cmd = new MySqlCommand($"UPDATE gamesinuse SET `timeReturned`='{DateTime.Now.ToString("yyyy-MM-dd")}' WHERE `id`={isNotReturned}", conn);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand($"UPDATE games SET `availability` = '1' WHERE `id` = {gameId}", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                Console.WriteLine("The game is available again.");
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
