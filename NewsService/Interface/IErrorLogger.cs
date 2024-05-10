using NewsService.Models;
using System.Text;

namespace NewsService.Interface
{
    public interface IErrorLogger
    {
        void uploadFileErrorLog(StringBuilder message);

   
        void uploadFileCurrentLog(string message);
    }
}
