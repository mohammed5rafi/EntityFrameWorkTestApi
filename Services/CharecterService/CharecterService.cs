using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Webapi.Data;
using Webapi.Models;
using Webapi.Models.DTO.Charecter;

namespace Webapi.Services.CharecterService
{
    public class CharecterService : ICharecterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CharecterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;

        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        private string GetUserRole() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
       
        public async Task<ServiceResponce<List<GetCharecterDTO>>> AddCharecter(AddCharecterDTO newCharecter)
        {
            ServiceResponce<List<GetCharecterDTO>> serviceResponce = new ServiceResponce<List<GetCharecterDTO>>();
            Charecter charecter = (_mapper.Map<Charecter>(newCharecter));
            charecter.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            await _context.Charecters.AddAsync(charecter);
            await _context.SaveChangesAsync();
            serviceResponce.Data = (_context.Charecters.Where(c => c.User.Id == GetUserId()).Select(c => _mapper.Map<GetCharecterDTO>(c))).ToList();
            return serviceResponce;
        }

        public async Task<ServiceResponce<List<GetCharecterDTO>>> GetAllCharecters()
        {
            ServiceResponce<List<GetCharecterDTO>> serviceResponce = new ServiceResponce<List<GetCharecterDTO>>();
            List<Charecter> dbCharecter =
            GetUserRole().Equals("Admin") ?
             await _context.Charecters.ToListAsync() :
             await _context.Charecters.Where(c => c.User.Id == GetUserId()).ToListAsync();
            serviceResponce.Data = (dbCharecter.Select(c => _mapper.Map<GetCharecterDTO>(c))).ToList();
            return serviceResponce;
        }

        public async Task<ServiceResponce<GetCharecterDTO>> GetCharecterById(int id)
        {
            ServiceResponce<GetCharecterDTO> serviceResponce = new ServiceResponce<GetCharecterDTO>();
            Charecter dbCharecter = await _context.Charecters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
            serviceResponce.Data = _mapper.Map<GetCharecterDTO>(dbCharecter);
            return serviceResponce;
        }

        public async Task<ServiceResponce<GetCharecterDTO>> UpdateCharecter(UpdateCharecterDTO updateCharecter)
        {
            ServiceResponce<GetCharecterDTO> serviceResponce = new ServiceResponce<GetCharecterDTO>();

            try
            {

                Charecter charecter = await _context.Charecters.Include(c =>c.User.Id).FirstOrDefaultAsync(c => c.Id == updateCharecter.Id);
                if(charecter.User.Id == GetUserId()){
                charecter.Name = updateCharecter.Name;
                charecter.Class = updateCharecter.Class;
                charecter.Defence = updateCharecter.Defence;
                charecter.Intelligence = updateCharecter.Intelligence;
                charecter.HitPoints = updateCharecter.HitPoints;
                charecter.Strength = updateCharecter.Strength;
                _context.Charecters.Update(charecter);
                await _context.SaveChangesAsync();
                serviceResponce.Data = _mapper.Map<GetCharecterDTO>(charecter);
                }
                else{
                    serviceResponce.Success=false;
                    serviceResponce.Message="Charecter not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponce.Success = false;
                serviceResponce.Message = ex.Message;
            }

            return serviceResponce;
        }

        public async Task<ServiceResponce<List<GetCharecterDTO>>> DeleteCharecter(int id)
        {
            ServiceResponce<List<GetCharecterDTO>> serviceResponce = new ServiceResponce<List<GetCharecterDTO>>();

            try
            {

                Charecter charecter = await _context.Charecters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
                if(charecter != null){
                _context.Charecters.Remove(charecter);
                await _context.SaveChangesAsync();
                serviceResponce.Data = (_context.Charecters.Where(c =>c.User.Id == GetUserId()).Select(c => _mapper.Map<GetCharecterDTO>(c))).ToList();
                }
                else{
                    serviceResponce.Success = false;
                    serviceResponce.Message="Charecter not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponce.Success = false;
                serviceResponce.Message = ex.Message;
            }

            return serviceResponce;
        }
    }
}