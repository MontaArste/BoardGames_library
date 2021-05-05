

namespace fileReader.Entities
{
    class Category
    {
        public uint Id { get; set; }
        public string categoryName { get; set; }

    
        public override string ToString()
        {
            return $"Category with id {Id} is called {categoryName}";
        }
    }
}
