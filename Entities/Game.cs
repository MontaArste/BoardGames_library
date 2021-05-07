
namespace BG_library.Entities
{
    public class Game
    {
        public string gameName { get; set; }
        public string availability { get; set; }


        public override string ToString()
        {
            return gameName+" "+availability;
        }

        
    }
}