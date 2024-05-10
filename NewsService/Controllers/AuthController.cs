using Microsoft.AspNetCore.Mvc;
using NewsService.DTO;
using NewsService.JWtModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace NewsService.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : Controller
    {

        private readonly JwtOptions _jwtOptions;
        public AuthController(IOptions<JwtOptions> options)
        {
            _jwtOptions = options.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserAuthDTO collection)
        {
            //fetch your username and password and check


            if(collection.username=="abc" && collection.password=="123") 
            {
                var jwtkey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(_jwtOptions.key));
                var credentials = new SigningCredentials(jwtkey, SecurityAlgorithms.HmacSha256);

                List<Claim> claims = new List<Claim>()
                {
                    new Claim ("username",collection.username)
                };

                var sToken = new JwtSecurityToken(_jwtOptions.key, _jwtOptions.Issuer, claims, expires: DateTime.Now.AddHours(1),signingCredentials:credentials);
                var token = new JwtSecurityTokenHandler().WriteToken(sToken);
                return Ok(new { token = token, name = "testAdmin",userId= "Localhost" });
                // after 1 hours Token will expire

            }

            else
            {
                return BadRequest(new { error = "Invalid UserName and password" });
            }
           
        }







    }
}
