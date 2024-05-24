using NewsService.Models;

namespace NewsService.Interface
{
    public interface IDocumentHandler
    {
        string SaveImageDetilsToDatabase(tbl_UploadedFilesForPost tbl_UploadedFilesForPost);
        public Task<List<object>> GetImageDetils();

        public Task<string> SaveImageToDirectory(FileuploadingModelBuilder file, int folderType);


    }
}
