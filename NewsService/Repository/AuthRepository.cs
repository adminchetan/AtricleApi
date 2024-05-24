using Microsoft.AspNetCore.Identity;
using NewsService.Context;
using NewsService.DTO;
using NewsService.Interface;
using NewsService.Models;
using System.Security.Cryptography;
using System.Text;

namespace NewsService.Repository
{
    public class AuthRepository : IAuthHandler
    {
        private readonly newsDbContext _newsDbContext;
        private readonly IErrorLogger _errorLogger;
        public AuthRepository(newsDbContext newsDbContext, IErrorLogger tbl_Logger)
        {
            _newsDbContext = newsDbContext;
            _errorLogger = tbl_Logger;
        }
        public (bool,string) CreateNewUser(AuthDTO authDTO)
        {

            _errorLogger.UserCreationLogInfor("Attempting to create User "+authDTO.Email,authDTO.CurrentUser);
            var (hash, salt) = GenerateHashAndSalt(authDTO.password);
            tbl_AuthMaster tbl_AuthMaster = new tbl_AuthMaster()
            {
                Email=authDTO.Email,
                Hash=hash,
                Salt=salt,
                MobileNumber=authDTO.MobileNumber,
                CreatedOn=DateTime.Now,
                CreatedBy=authDTO.CurrentUser
               
            };

           try
            {
                _newsDbContext.tbl_AuthMasters.Add(tbl_AuthMaster);
                var i= _newsDbContext.SaveChanges();
                if (i == 1)
                {
                    _errorLogger.UserCreationLogInfor("User " + authDTO.Email+" Created successfully " , authDTO.CurrentUser);
                    return (true, "Account has been Created successfully");
                }

                else
                {
                    _errorLogger.UserCreationLogInfor("User Not Created" + authDTO.Email, authDTO.CurrentUser);
                    return (false, "Account cannot be created");
                }
                
            }
             catch (Exception ex)
            {
                _errorLogger.UserCreationError("User Cannot Be created", "Error", ex.Message,authDTO.CurrentUser);
                return (false, "Account cannot be created dur to technical error");
            }
                    
        }

        public (bool, string,string,int,string) ValidateUserCredentials(string username, string password)
        {
            var EmailExist = false;   var MobileExist = false; 
            EmailExist = _newsDbContext.tbl_AuthMasters.Any(x => x.Email == username);
            MobileExist = _newsDbContext.tbl_AuthMasters.Any(x => x.MobileNumber == username);

            if (EmailExist == true || MobileExist == true)
            {

                var userCredentials = _newsDbContext.tbl_AuthMasters
                .Where(x => x.Email == username || x.MobileNumber == username)
                .Select(x => new { x.Salt, x.Hash, x.UserId, x.Email,x.FirstName,x.LastName })
                .FirstOrDefault();

                        if (userCredentials != null)
                        {
                            string salt = userCredentials.Salt;
                            string hashedPassword = userCredentials.Hash;
                            int userID = userCredentials.UserId;
                            string Username = userCredentials.Email;
                            string UserCompleteName = userCredentials.FirstName + " " + userCredentials.LastName;
  

                            var i = VerifyPassword(password, hashedPassword, salt);
                            if (i == true)
                            {
                                return (true, "User validated successfully", Username, userID,UserCompleteName);
                            }

                            else
                            {
                                return (false, "Incorrect Password", "NA", 0, UserCompleteName);
                            }
                        }
                        else
                        {
                            _errorLogger.UserCreationLogInfor("Not able to get user information", username);
                            return (false, "Not able to get user information", "", 0,"NA");
                        }
            }

            else
            {
                return (false, "User (" + username + ")" + " does not exist", "", 0,"NA");
            }


        }

        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            // Combine entered password and stored salt
            byte[] combinedBytes = Encoding.UTF8.GetBytes(enteredPassword + storedSalt);

            // Hash the combined bytes
            using (var sha256 = new SHA256Managed())
            {
                byte[] hashedBytes = sha256.ComputeHash(combinedBytes);
                string hashedPassword = Convert.ToBase64String(hashedBytes);

                // Compare the hashed entered password with the stored hash
                return hashedPassword == storedHash;
            }

        }


        public static (string Hash, string Salt) GenerateHashAndSalt(string password)
         {
            // Generate a random salt
            byte[] salt = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(salt);

            // Combine salt and password
            byte[] combinedBytes = Encoding.UTF8.GetBytes(password + Convert.ToBase64String(salt));

            // Hash the combined bytes
            using (var sha256 = new SHA256Managed())
            {
                byte[] hashedBytes = sha256.ComputeHash(combinedBytes);
                string hashedPassword = Convert.ToBase64String(hashedBytes);
                string saltString = Convert.ToBase64String(salt);
                return (hashedPassword, saltString);
            }
        }

        public bool CheckMobileNumberExist(string mobilenumber)
        {
            var i=false;
            if (mobilenumber!=null)
            {
                i = _newsDbContext.tbl_AuthMasters.Select(x => x.MobileNumber == mobilenumber).FirstOrDefault();
            }

            return i;
        }

        public bool checkEmailAlredyExist(string email)
        {
            var i = false;

            if(email != null)
            {
                i= _newsDbContext.tbl_AuthMasters.Select(x=>x.Email== email).FirstOrDefault();  

            }

            return i;
        }

        public bool UpdatedLastLoggedIn(string username)
        {
            var response = false;
            var record = _newsDbContext.tbl_AuthMasters.FirstOrDefault(r => r.Email==username);
            if (record != null){
                record.LastLogin=DateTime.Now;
                response=Convert.ToBoolean(_newsDbContext.SaveChanges());
                return (response);
            }
            return (response);
        }

    }
}
