namespace Webapi.Models
{
    public class CharecterSkill
    {
        public int CharecterId { get; set; }
        public Charecter Charecter { get; set; }
        public int SkillId { get; set; }

        public Skill Skill { get; set; }
    }
}