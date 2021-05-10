

namespace BG_library.Entities
{
    public class Category
    {
        public uint Id { get; set; }
        public string categoryName { get; set; }

        public Category (string categoryName)
        {
            this.categoryName = categoryName;
            
        }

        public Category() {} 

    
        public override string ToString()
        {
            return $"Category with id {Id} is called {categoryName}";
        }
    }
}
