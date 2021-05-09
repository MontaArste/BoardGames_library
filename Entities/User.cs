

namespace BG_library.Entities
{
    public class User
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public User (string Name, string Surname)
        {
            this.Name = Name;
            this.Surname = Surname;
        }

        public User() {}           
        


        public override string ToString()
        {
            return Name + " " + Surname;
        }
    }
}
