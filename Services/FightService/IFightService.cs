using System.Collections.Generic;
using System.Threading.Tasks;
using Webapi.Models;
using Webapi.Models.DTO.Fight;

namespace Webapi.Services.FightService
{
    public interface IFightService
    {
         Task<ServiceResponce<AttackResultDTO>> WeaponAttack (WeaponAttackDTO request);
         Task<ServiceResponce<AttackResultDTO>> SkillAttack (SKillAttackDTO request);
         Task<ServiceResponce<FightResultDTO>> Fight (FightRequestDTO request);

        Task<ServiceResponce<List<HighscoreDTO>>> GetHighscore();
    }
}