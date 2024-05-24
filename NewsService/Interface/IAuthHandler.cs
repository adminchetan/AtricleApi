using NewsService.DTO;

namespace NewsService.Interface
{
    public interface IAuthHandler
    {
        public (bool, string,string,int,string) ValidateUserCredentials(string username,string password);
        public (bool, string) CreateNewUser(AuthDTO authDTO);

        public bool UpdatedLastLoggedIn(string username);


        public bool UpdatedPassword(string username, string password);

        public bool CheckMobileNumberExist(string mobilenumber);

        public bool checkEmailAlredyExist(string email);
    }
}
