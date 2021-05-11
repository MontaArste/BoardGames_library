namespace BG_library.Entities
{
    public class GameCategories
    {
        public int gameId { get; set; }

        public int categoryId { get; set; }

        public override string ToString()
        {
            return $"The game with an id: {gameId} is included in this category: {categoryId}";
        }
    }
}