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
            //TestDataInserter.InsertTestData();  //inserts data in database from excel document

            
            GameService.SearchGameByName("om");
            CategoryService.SearchCategory("book");
            UserService.SearchUserByName("māris");
            //User user = new User("Māris", "Čaklais");
            //UserService.AddUser(user);
            //CategoryService.LoadCategories(); 
            //Game a = new Game("Riču raču");
            //GameService.AddGame(a); 
            //CategoryService.LoadCategories();
            //GameService.SearchGameByName("alias");
            //GameService.TakeGame(5, 27);
            //GameService.SearchGameByName("ali");
            //GameService.ReturnGame(5,27);
            //GameService.SearchGameByName("ali");


                      


           
        }
    }
}
