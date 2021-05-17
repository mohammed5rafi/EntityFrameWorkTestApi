using System.Collections.Generic;
using System.Threading.Tasks;
using Webapi.Models;
using Webapi.Models.DTO.Charecter;

namespace Webapi.Services.CharecterService

{
    public interface ICharecterService
    {
        Task<ServiceResponce<List<GetCharecterDTO>>> GetAllCharecters();
        Task<ServiceResponce<GetCharecterDTO>> GetCharecterById(int id);
        Task<ServiceResponce<List<GetCharecterDTO>>> AddCharecter(AddCharecterDTO newCharecter);
        Task<ServiceResponce<GetCharecterDTO>> UpdateCharecter(UpdateCharecterDTO updateCharecter );
        Task<ServiceResponce<List<GetCharecterDTO>>> DeleteCharecter(int id);

    }
}