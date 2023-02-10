using JwtAuthenticationManager;
using Microsoft.AspNetCore.Mvc;
using JwtAuthenticationManager.Model;
namespace GameShop.Authentication
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController: ControllerBase
    {
        private readonly JwtTokenHandler _jwtTokenHandler;
        public AuthController(JwtTokenHandler jwtTokenHandler)
        {
            _jwtTokenHandler = jwtTokenHandler;
        }
        [HttpPost]
        public ActionResult<AuthenticationResponse?> Authenticate (AuthenticationRequest request){
            var response  = _jwtTokenHandler.GenerateJwtToken(request);
            if (response == null){
                return Unauthorized();
            }
            return response;
        }
    }
}