using NewsService.Context;
using NewsService.Interface;
using NewsService.Models;
using System.Text;

namespace NewsService.RepositoryErrorLogs
{
    public class AllMessageAndLogs : IErrorLogger
    {
        private readonly newsDbContext _newsDbContext;
        public AllMessageAndLogs(newsDbContext newsDbContext)
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



        public void UserCreationLogInfor(string message, string User)
        {
            tbl_Logger tbl_Logger = new tbl_Logger
            {
                Message = message,
                TrackingCode = User,
                Level = 4,
                EntityName = "tbl_AuthMaster",
                Excepction="NA",
                Eventdate=DateTime.Now,

             };
            _newsDbContext.tbl_Loggers.Add(tbl_Logger); 
            _newsDbContext.SaveChanges();

        }

        public void UserCreationError(string message, string type, string Excepction, string TrackingCode)
        {
            tbl_Logger tbl_Logger = new tbl_Logger
            {
                Message = message,
                TrackingCode =TrackingCode,
                Level = 4,
                EntityName = "tbl_AuthMaster",
                Excepction = Excepction,
                Eventdate = DateTime.Now,
            };
            _newsDbContext.tbl_Loggers.Add(tbl_Logger);
            _newsDbContext.SaveChanges();
        }
    }
}
