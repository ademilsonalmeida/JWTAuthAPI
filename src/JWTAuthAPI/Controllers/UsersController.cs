using JWTAuthAPI.Authorize;
using JWTAuthAPI.ViewModels;
using System.Web.Http;

namespace JWTAuthAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ApiController
    {
        [HttpGet]
        [Route("check")]
        [CustomAuthorize("Authenticated")]
        public IHttpActionResult Check()
        {
            return Ok(new { Message = "Authenticated user." });
        }

        [HttpPost]
        [Route("auth")]
        public IHttpActionResult Auth([FromBody] UserViewModel userViewModel)
        {
            if (string.IsNullOrWhiteSpace(userViewModel.User) || string.IsNullOrWhiteSpace(userViewModel.Password))
                return BadRequest("Invalid credentials");

            if (userViewModel.User != "admin" || userViewModel.Password != "123")
                return BadRequest("Invalid credentials");

            return Ok(TokenManager.GenerateToken(userViewModel.User));
        }

    }
}