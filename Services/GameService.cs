using BG_library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_library.Services
{
    class GameService
    {
        public bool AddGame(Game game)
        {
            throw new NotImplementedException();
        }

        public Game SearchGameByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool TakeGame (uint userId, uint gameId)
        {
            throw new NotImplementedException();
            // remember about timestamps
        }

        public bool ReturnGame(uint userId, uint gameId)
        {
            throw new NotImplementedException();
            // remember about timestamps
        }
    }
}
