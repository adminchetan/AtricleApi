﻿using Microsoft.AspNetCore.Mvc;
using NewsService.DTO;
using NewsService.JWtModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using NewsService.Interface;
using NewsService.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Linq;
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
        public IActionResult Login([FromBody] UserAuthDTO collection)
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
                var token = new JwtSecurityTokenHandler().WriteToken(sToken);       // after 1 hours Token will expire


                if (token != null)
                {
                    _authHandler.UpdatedLastLoggedIn(collection.username);
                }

                return Ok(new { token = token, Name = username, userId = userid, userName = UserCompleteName });

            }

            else
            {
                return Json(new { success=success,error = message });
            }
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody]  AuthDTO authDTO)
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
                return Json(new{success=false,message="MobileNumber & Email Already Exist"});
            }
            if (ifExistvalidateMobile)
            {
                return Json(new { success = false, message = "MobileNumber Already Exist" });
            }
            if (ifExistValidateEmail)
            {
                return Json(new { success = false, message = "Email Already Exist" });
            }
            else 
            {
                var(success, message) = _authHandler.CreateNewUser(authDTO);
                return Json(new { Success = success, Message = message });
            }
        }


        [HttpPost]
        public IActionResult ChangePassword(string username, string newPassword)
        {
            _loggger.UserCreationLogInfor("User Requested To change the password", username);
            var i= _authHandler.UpdatedPassword(username, newPassword);

            if (i == true)
            {
                return Json(new { success = true, message = "Password rest successfully" });
            }
            else
            {
                return Json(new { success = true, message = "Password rest failed" });
            }

        }
 
    }

  
 }

