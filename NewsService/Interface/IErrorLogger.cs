using NewsService.DTO;
using NewsService.Models;
using System.Reflection.Metadata;
using System.Text;

namespace NewsService.Interface
{
    public interface IErrorLogger
    {
        void uploadFileErrorLog(StringBuilder message);
        void uploadFileCurrentLog(string message);

        void UserCreationError(string message, string type,string Excepction,string TrackingCode);
        void UserCreationLogInfor(
            string message,
            string User);

    
    
    }
}
