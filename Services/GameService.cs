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

        public static bool TakeGame (uint userId, uint gameId)
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
