using System.Threading.Tasks;
using Webapi.Models;
using Webapi.Models.DTO.Charecter;
using Webapi.Models.DTO.CharecterSkill;

namespace Webapi.Services.CharecterSkillService
{
    public interface ICharecterSkillService
    {
         Task<ServiceResponce<GetCharecterDTO>> AddCharecterSkill (AddCharecterSkillDTO newCharecterSkill);
    }
}