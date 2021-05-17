using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Webapi.Data;
using Webapi.Models;
using Webapi.Models.DTO.Charecter;
using Webapi.Models.DTO.CharecterSkill;

namespace Webapi.Services.CharecterSkillService
{
    public class CharecterSkillService : ICharecterSkillService
    {
        public DataContext _context { get; set; }
        public IHttpContextAccessor _httpContextAccessor { get; set; }
        public IMapper _mapper { get; set; }
        public CharecterSkillService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _context = context;

        }
        public async Task<ServiceResponce<GetCharecterDTO>> AddCharecterSkill(AddCharecterSkillDTO newCharecterSkill)
        {
           ServiceResponce<GetCharecterDTO> responce = new ServiceResponce<GetCharecterDTO>();
           try{
               Charecter charecter = await _context.Charecters
               .Include(c => c.Weapon)
               .Include(c => c.CharecterSkills).ThenInclude(cs => cs.Skill)
               .FirstOrDefaultAsync(c =>c.Id == newCharecterSkill.CharecterId && 
               c.User.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
               if (charecter == null){
                   responce.Success = false;
                   responce.Message ="Charecter not found.";
                   return responce;
               }
               Skill skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == newCharecterSkill.SkillId);
               if(skill == null){
                 responce.Success = false;
                 responce.Message ="Skill not found.";
                 return responce;}
                 CharecterSkill characterSkill = new CharecterSkill(){
                  Charecter = charecter,
                  Skill = skill
                 };
                 await _context.CharecterSkills.AddAsync(characterSkill);
                 await _context.SaveChangesAsync();
                 responce.Data = _mapper.Map<GetCharecterDTO>(charecter);
           }
           catch(Exception ex)
           {
               responce.Success =false;
               responce.Message = ex.Message;
               
           }
           return responce;
        }
    }
}