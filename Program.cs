using System;
using System.IO;
using BG_library.Entities;
using BG_library.Services;
// dotnet add package MySql.Data
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BG_library
{
    class Program
    {
        static void Main(string[] args)
        {
            TestDataInserter.InsertTestData();  //inserts data in database from excel document


            //GameService.TakeGame(4,2);
            //GameService.ReturnGame(4, 2);
            //GameService.SearchGameByName("black");
            //GameService.CheckGameAvailability(2);
            //CategoryService.SearchCategory("action");
            //UserService.SearchUserByName("mar");
            //User user = new User("Māris", "Čaklais");
            //UserService.AddUser(user);
            //CategoryService.LoadCategories(); 
            // Game a = new Game("Riču raču");
            // GameService.AddGame(a,2);           



        }
    }
}
