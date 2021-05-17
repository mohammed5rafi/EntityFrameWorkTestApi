using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Webapi.Models.DTO.Weapon;
using Webapi.Services.WeaponService;

namespace Webapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;
        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService;

        }

        [HttpPost]
        public async Task<IActionResult> AddWeapon (AddWeaponDTO newWeapon){
           return Ok(await _weaponService.AddWeapon(newWeapon));

        }
    }
}