using System;
using System.IO;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BG_library
{
    class Games
    {
        private string gameName;

        public Games(string gameName)
        {
            this.gameName = gameName;
        }

        public string GetGameName()
        {
            return gameName;
        }

        public void SetGameName(string value)
        {
            gameName = value;
        }

        public override string ToString()
        {
            return $"Game: {gameName}";
        }

        public static bool addGame(string newGame)
        {

            string connString = File.ReadAllText("connectionString.txt");
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd;
            try
            {
                conn.Open();
                cmd = new MySqlCommand("INSERT IGNORE INTO games(gameName, availability) VALUES (newGame, 1)", conn);
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
    }
}