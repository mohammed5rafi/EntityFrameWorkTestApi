using System.Collections.Generic;

namespace Webapi.Models
{
    public class Charecter
    {
        public int Id {get;set;}
        public string Name {get;set;} ="rafi";
        public int HitPoints {get;set;}=100;
        public int Strength {get;set;}=10;
        public int Defence {get;set;}=10;
        public int Intelligence {get;set;}=10;

        public RpgClass Class {get;set;} = RpgClass.Knight;

        public User User {get;set;}
        public Weapon Weapon { get; set; }
        public List<CharecterSkill> CharecterSkills { get; set; }
        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }

    }
}