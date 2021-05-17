using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webapi.Data;
using Webapi.Models;
using Webapi.Models.DTO.User;

namespace Webapi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
           _authRepository = authRepository;

        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDTO request){
            ServiceResponce<int> responce = await _authRepository.Register(
                new User {Username = request.Username},request.Password
            );
            if(!responce.Success){
                return BadRequest(responce);
            }return Ok(responce);
        }

         [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDTO request){
            ServiceResponce<string> responce = await _authRepository.Login(request.Username,request.Password );
            if(!responce.Success){
                return BadRequest(responce);
            }
            return Ok(responce);
        }
    }
}