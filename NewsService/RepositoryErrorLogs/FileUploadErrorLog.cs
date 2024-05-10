using NewsService.Context;
using NewsService.Interface;
using NewsService.Models;
using System.Text;

namespace NewsService.RepositoryErrorLogs
{
    public class FileUploadErrorLog : IErrorLogger
    {
        private readonly newsDbContext _newsDbContext;
        public FileUploadErrorLog(newsDbContext newsDbContext)
        {
            _newsDbContext = newsDbContext;
        }

        public void uploadFileCurrentLog(string message)
        {
            tbl_FileUploadErrorLog errorLog = new tbl_FileUploadErrorLog()
            {
                Error = "Message",
                Message = message.ToString(),
                Eventdate = DateTime.Now

            };

            _newsDbContext.tbl_FileUploadErrorLogs.Add(errorLog);
            _newsDbContext.SaveChanges();
        }

  

        public void uploadFileErrorLog(StringBuilder message)
        {
            tbl_FileUploadErrorLog errorLog = new tbl_FileUploadErrorLog()
            {
                Error = "Message",
                Message=message.ToString(),
                Eventdate= DateTime.Now

            }; 

            _newsDbContext.tbl_FileUploadErrorLogs.Add(errorLog);
            _newsDbContext.SaveChanges();

            
        }
    }
}
