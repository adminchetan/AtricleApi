using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsService.Models
{
    [Table("tbl_Logger", Schema = "dbo")]
    public class tbl_Logger
    {
        [Key]
        public int LogId { get; set; }
        public string Message { get; set; }

        public string Excepction { get; set; }
        public int Level { get; set; }
        public DateTime Eventdate { get; set; }
        public string TrackingCode { get; set; }

        public string EntityName { get; set; }




    }
}
