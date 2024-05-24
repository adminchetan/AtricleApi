namespace NewsService.Interface
{
    public interface INotificationHandler
    {

        public bool SendOtpEmail(string email);

        public bool SendGeneralEmail(string emailIdTo, string emailsubject, string UserName, string file);
    }
}
