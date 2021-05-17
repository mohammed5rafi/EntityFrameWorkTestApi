using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Webapi.Models.DTO.Fight;
using Webapi.Services.FightService;

namespace Webapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FightController : ControllerBase
    {
        private readonly IFightService _fightService;
        public FightController(IFightService fightService)
        {
            _fightService = fightService;

        }
        [HttpPost("Weapon")]
      public async Task<IActionResult> WeaponAttack (WeaponAttackDTO request){

          return Ok(await _fightService.WeaponAttack(request));
      }

         [HttpPost("Skill")]
      public async Task<IActionResult> SkillAttack (SKillAttackDTO request){

          return Ok(await _fightService.SkillAttack(request));
      }
     [HttpPost("Fight")]
      public async Task<IActionResult> Fight (FightRequestDTO request){

          return Ok(await _fightService.Fight(request));
      }

       
      public async Task<IActionResult> GetHighscore (){

          return Ok(await _fightService.GetHighscore());
      }
    }
}