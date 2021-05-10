namespace BG_library.Entities
{
    public class GameCategories
    {
        public string gameId { get; set; }

        public string categoryId { get; set; }

        public override string ToString()
        {
            return $"The game with an id: {gameId} is included in this category: {categoryId}";
        }
    }
}