using System.Collections.Generic;
using Webapi.Models.DTO.Skill;
using Webapi.Models.DTO.Weapon;

namespace Webapi.Models.DTO.Charecter
{
    public class GetCharecterDTO
    {
        public int Id {get;set;}
        public string Name {get;set;} ="rafi";
        public int HitPoints {get;set;}=100;
        public int Strength {get;set;}=10;
        public int Defence {get;set;}=10;
        public int Intelligence {get;set;}=10;

        public RpgClass Class {get;set;} = RpgClass.Knight;

        public GetWeaponDTO weapon { get; set; }

        public List<GetSkillDTO> Skills {get;set;}
           public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }

    }
}