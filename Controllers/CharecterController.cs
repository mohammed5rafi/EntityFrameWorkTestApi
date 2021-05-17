using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Webapi.Models;
using System.Linq;
using Webapi.Services.CharecterService;
using System.Threading.Tasks;
using Webapi.Models.DTO.Charecter;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Webapi.Controllers
{

    [Authorize(Roles="Player")]
    [ApiController]
    [Route("[controller]")]
    public class CharecterController : ControllerBase
    {
       
        private readonly ICharecterService _charecterService;

        public CharecterController(ICharecterService charecterService)
        {
            _charecterService = charecterService;

        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _charecterService.GetAllCharecters());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id){
        return Ok (await _charecterService.GetCharecterById(id));
        }
        [HttpPost]
        public async Task<IActionResult> AddCharecter(AddCharecterDTO newCharecter)
        {
            return Ok(await _charecterService.AddCharecter(newCharecter));
        }
          [HttpPut]
        public async Task<IActionResult> UpdateCharecter(UpdateCharecterDTO updateCharecter)
        {
            ServiceResponce<GetCharecterDTO> responce = await _charecterService.UpdateCharecter(updateCharecter);
            if(responce.Data == null)
            {
             return NotFound(responce);
            }
            return Ok(responce);
        }
         [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            ServiceResponce<List<GetCharecterDTO>> responce = await _charecterService.DeleteCharecter(id);
            if(responce.Data == null)
            {
             return NotFound(responce);
            }
            return Ok(responce);
        }
    }
}