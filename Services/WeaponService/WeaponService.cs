using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Webapi.Data;
using Webapi.Models;
using Webapi.Models.DTO.Charecter;
using Webapi.Models.DTO.Weapon;

namespace Webapi.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public WeaponService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
           _mapper = mapper;
           _context = context;
           _httpContextAccessor = httpContextAccessor;

        }
        public async Task<ServiceResponce<GetCharecterDTO>> AddWeapon(AddWeaponDTO newWeapon)
        {
          ServiceResponce<GetCharecterDTO> responce = new ServiceResponce<GetCharecterDTO>();

          try{

           Charecter charecter = await _context.Charecters.FirstOrDefaultAsync(c => c.Id == newWeapon.CharecterId 
           && c.User.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
           if(charecter == null)
           {
               responce.Success = false;
               responce.Message = "Charecter is not found.";
               return responce;
           }
           Weapon weapon = new Weapon(){
               Name = newWeapon.Name,
               Damage = newWeapon.Damage,
               Charecter =charecter
           };

           await _context.Weapons.AddAsync(weapon);
           await _context.SaveChangesAsync();
           responce.Data = _mapper.Map<GetCharecterDTO>(charecter);
          }
          catch(Exception ex){
              responce.Success =false;
              responce.Message = ex.Message;
          }
          return responce;
        }
        
    }
}