

using System;
using System.IO;
using fileReader.Entities;
using IronXL;
using MySql.Data.MySqlClient;

namespace fileReader.Services
{
    public static class TestDataInserter
    {
        public static void InsertTestData()
        {
            IronXL.License.LicenseKey = File.ReadAllText("IronXL.Licence.txt");
            var path = File.ReadAllText("connectionString.txt");
            MySqlConnection conn = new MySqlConnection(path);
            var workBook = new WorkBook("DatiGame_library.xlsx");

            try
            {
                conn.Open();
                InsertUserTestData(conn, workBook);
                InsertGameTestData(conn, workBook);
                InsertCategoryTestData(conn, workBook);
                InsertGameCategoryTestData(conn, workBook);
                InsertGameInUseTestData(conn, workBook);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

        }

        private static bool DoesDataExist(MySqlConnection conn, string tableName)
        {
            var cmd = new MySqlCommand($"SELECT * FROM {tableName}", conn);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return true;
                }
            }
            return false;
        }

        private static void InsertGameInUseTestData(MySqlConnection conn, WorkBook workBook)
        {
            if (DoesDataExist(conn, "GamesInUse")) return;
            var workSheet = workBook.GetWorkSheet("GameInUse");
            var range = workSheet.GetRange("A2:B11");

           

            foreach (var row in range.Rows)
            {
                GameInUse gameInUse = new GameInUse();
                gameInUse.gameId = row.Columns[0].ToString();
                gameInUse.userId = row.Columns[1].ToString();

                Console.WriteLine(gameInUse.ToString());

                MySqlCommand cmd;

                cmd = new MySqlCommand($"INSERT INTO gamesInUse (`gameId`, `userId`) VALUES ('{gameInUse.gameId}', '{gameInUse.userId}')", conn);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand($"UPDATE games SET `availability` ='0' WHERE `id` = {gameInUse.gameId}", conn);
                cmd.ExecuteNonQuery();
            }
        }
        private static void InsertGameCategoryTestData(MySqlConnection conn, WorkBook workBook)
        {
            if (DoesDataExist(conn, "GameCategory")) return;
            var workSheet = workBook.GetWorkSheet("GameCategories");
            var range = workSheet.GetRange("A2:B77");

            foreach (var row in range.Rows)
            {
                GameCategories gameCategories = new GameCategories();
                gameCategories.gameId = row.Columns[0].ToString();
                gameCategories.categoryId = row.Columns[1].ToString();

                Console.WriteLine(gameCategories.ToString());

                MySqlCommand cmd;

                cmd = new MySqlCommand($"INSERT INTO gamecategory (`gameId`, `categoryId`) VALUES ('{gameCategories.gameId}', '{gameCategories.categoryId}')", conn);
                cmd.ExecuteNonQuery();
            }
        }

        private static void InsertCategoryTestData(MySqlConnection conn, WorkBook workBook)
        {
            if (DoesDataExist(conn, "category")) return;
            var workSheet = workBook.GetWorkSheet("categories");
            var range = workSheet.GetRange("A1:A8");

            foreach (var row in range.Rows)
            {
                Category category = new Category();
                category.categoryName = row.Columns[0].ToString();


                Console.WriteLine(category.ToString());

                MySqlCommand cmd;

                cmd = new MySqlCommand($"INSERT INTO category (`categoryName`) VALUES ('{category.categoryName}')", conn);
                cmd.ExecuteNonQuery();

            }
        }

        private static void InsertGameTestData(MySqlConnection conn, WorkBook workBook)
        {
            if (DoesDataExist(conn, "games")) return;
            var workSheet = workBook.GetWorkSheet("games");
            var range = workSheet.GetRange("A1:B56");

            foreach (var row in range.Rows)
            {
                Game game = new Game();
                game.gameName = row.Columns[0].ToString();
                game.availability = row.Columns[1].ToString();

                Console.WriteLine(game.ToString());

                MySqlCommand cmd;

                cmd = new MySqlCommand($"INSERT INTO games (`gameName`, `availability`) VALUES ('{game.gameName}', '{game.availability}')", conn);
                cmd.ExecuteNonQuery();

            }
        }

        private static void InsertUserTestData(MySqlConnection conn, WorkBook workBook)
        {
            if (DoesDataExist(conn, "users")) return;
            var workSheet = workBook.GetWorkSheet("users");
            var range = workSheet.GetRange("A1:B23");

            foreach (var row in range.Rows)
            {
                User user = new User();
                user.Name = row.Columns[0].ToString();
                user.Surname = row.Columns[1].ToString();

                Console.WriteLine(user.ToString());

                MySqlCommand cmd;

                cmd = new MySqlCommand($"INSERT INTO Users (`Name`, `Surname`) VALUES ('{user.Name}', '{user.Surname}')", conn);
                cmd.ExecuteNonQuery();

            }
        }

    }
}
