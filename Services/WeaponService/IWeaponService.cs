using System.Threading.Tasks;
using Webapi.Models;
using Webapi.Models.DTO.Charecter;
using Webapi.Models.DTO.Weapon;

namespace Webapi.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponce<GetCharecterDTO>> AddWeapon(AddWeaponDTO newWeapon);
    }
}