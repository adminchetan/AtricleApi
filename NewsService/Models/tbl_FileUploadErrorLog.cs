using System.ComponentModel.DataAnnotations.Schema;

namespace NewsService.Models
{
    [Table("tbl_FileUploadErrorLog", Schema = "dbo")]

    public class tbl_FileUploadErrorLog
    {
        public int Id { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }

        public DateTime Eventdate { get; set; }
    }
}
