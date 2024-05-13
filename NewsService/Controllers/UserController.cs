using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsService.Context;
using NewsService.Models;
using System.Reflection;
using NewsService.Utility;
namespace NewsService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly newsDbContext _context;

        public UserController(newsDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public ActionResult CreateUser(tbl_UserData tbl_UserData)
        {


            try
            {


                string passencrypt = PasswordHelper.GenerateSaltedHash(tbl_UserData.Password);

                // Create new user data entity
                var newUser = new tbl_UserData
                {
                    UserName = tbl_UserData.UserName,
                    Password = passencrypt,
                    Email = tbl_UserData.Email,
                    RoleId = tbl_UserData.RoleId,
                    CreatedAt = DateTime.Now,
                    Status = true, // Assuming active by default
                    Mobile = tbl_UserData.Mobile
                };

                // Add user to DbContext and save changes
                _context.tbl_UserData.Add(newUser);
                _context.SaveChanges();
                return Ok();
            }
            catch(Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }




        [HttpPost]
        public ActionResult Login(tbl_UserData model)
        {
            try
            {
                // Retrieve user by username
                var user = _context.tbl_UserData.FirstOrDefault(u => u.UserName == model.UserName);

                if (user == null)
                {
                    return BadRequest("Invalid username or password.");
                }

                // Verify password
                bool isPasswordValid = PasswordHelper.VerifyPassword(model.Password, user.Password);

                if (!isPasswordValid)
                {
                    return BadRequest("Invalid username or password.");
                }

                // Authentication successful
                // You can implement further actions here (e.g., set authentication cookie, redirect to dashboard)
                return Ok("Login successful!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}




    

   

