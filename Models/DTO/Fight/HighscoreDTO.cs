namespace Webapi.Models.DTO.Fight
{
    public class HighscoreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Fights { get; set; }
        public int Victories  { get; set; }

        public int Defeats { get; set; }
    }
}