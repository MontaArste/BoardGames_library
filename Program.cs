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

            
            GameService.SearchGameByName("oo");
            CategoryService.SearchCategory("par");
            UserService.SearchUserByName("mar");
            //User user = new User("Māris", "Čaklais");
            //UserService.AddUser(user);
            CategoryService.LoadCategories();            


           
        }
    }
}
