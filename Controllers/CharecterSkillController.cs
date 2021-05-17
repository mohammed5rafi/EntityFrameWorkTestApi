using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Webapi.Models.DTO.CharecterSkill;
using Webapi.Services.CharecterSkillService;

namespace Webapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CharecterSkillController : ControllerBase
    {
        private readonly ICharecterSkillService _charecterSkillService;
        public CharecterSkillController(ICharecterSkillService charecterSkillService)
        {
            _charecterSkillService = charecterSkillService;

        }
        [HttpPost]
        public async Task<IActionResult> AddCharecterSkill (AddCharecterSkillDTO newCharecterSkill){

            return Ok(await _charecterSkillService.AddCharecterSkill(newCharecterSkill));
        }
    }
}