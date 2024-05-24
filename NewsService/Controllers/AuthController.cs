using Microsoft.AspNetCore.Mvc;
using NewsService.DTO;
using NewsService.JWtModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using NewsService.Interface;
using NewsService.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
namespace NewsService.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : Controller
    {

        private readonly JwtOptions _jwtOptions;
        private readonly IAuthHandler _authHandler;
        private readonly IErrorLogger _loggger;
        public AuthController(IOptions<JwtOptions> options,IAuthHandler authHandle,IErrorLogger errorLogger)
        {
            _jwtOptions = options.Value;
            _authHandler = authHandle;
            _loggger = errorLogger;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserAuthDTO collection)
        {
           
            //Check the login credentials

            var (success, message,username,userid,UserCompleteName) = _authHandler.ValidateUserCredentials(collection.username,collection.password);



            //fetch your username and password and check
            if(success) 
           {
                var jwtkey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(_jwtOptions.key));
                var credentials = new SigningCredentials(jwtkey, SecurityAlgorithms.HmacSha256);

                List<Claim> claims = new List<Claim>()
                {
                    new Claim ("username",collection.username)
                };

                var sToken = new JwtSecurityToken(_jwtOptions.key, _jwtOptions.Issuer, claims, expires: DateTime.Now.AddHours(1),signingCredentials:credentials);
                var token = new JwtSecurityTokenHandler().WriteToken(sToken);
                return Ok(new { token = token, LoginName = username, userId= userid, userName= UserCompleteName });
                // after 1 hours Token will expire

            }

            else
            {
                return Json(new { success=success,error = message });
            }
           
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(AuthDTO authDTO)
        {
            var ifExistvalidateMobile=false;
            var ifExistValidateEmail = false; 
            if(authDTO != null)
            {
                ifExistvalidateMobile = _authHandler.CheckMobileNumberExist(authDTO.MobileNumber);
                ifExistValidateEmail = _authHandler.checkEmailAlredyExist(authDTO.Email);
            }
            
            if(ifExistvalidateMobile==true)
            {
                _loggger.UserCreationLogInfor("User MobileNumber " + authDTO.MobileNumber + " Already Existe in System", authDTO.CurrentUser);
            }

            if (ifExistValidateEmail == true)
            {
                _loggger.UserCreationLogInfor("User Email " + authDTO.Email + " Already Existe in System", authDTO.CurrentUser);
            }
            if( ifExistvalidateMobile==true && ifExistValidateEmail==true) {
                return Json(new{success=true,message="MobileNumber & Email Already Exist"});
            }
            if (ifExistvalidateMobile)
            {
                return Json(new { success = true, message = "MobileNumber Already Exist" });
            }
            if (ifExistValidateEmail)
            {
                return Json(new { success = true, message = "Email Already Exist" });
            }
            else 
            {
                var(success, message) = _authHandler.CreateNewUser(authDTO);
                return Json(new { Success = success, Message = message });
            }
        }         
 
    }

  
 }

