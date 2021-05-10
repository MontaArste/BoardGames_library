

namespace BG_library.Entities
{
    public class GameInUse
    {
        public string gameId { get; set; }

        public string userId { get; set; }

        public override string ToString()
        {
            return $"The game with id: {gameId} has been taken by user with id: {userId}";
        }
    }
}
