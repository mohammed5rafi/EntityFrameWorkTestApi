namespace Webapi.Models
{
    public class Weapon
    {
        public int  Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }

        public Charecter Charecter { get; set; }

        public int CharecterId { get; set; }
    }
}