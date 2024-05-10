namespace NewsService.Models
{
    public class FileuploadingModelBuilder
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public IFormFile FormFile { get; set; }

        public string Description { get; set; }
    }
}
