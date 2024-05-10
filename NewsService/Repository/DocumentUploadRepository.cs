using Microsoft.EntityFrameworkCore;
using NewsService.Context;
using NewsService.Interface;
using NewsService.Models;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace NewsService.Repository
{

    public class DocumentUploadRepository : IDocumentHandler
    {
        private readonly newsDbContext _newsDbContext;
        private readonly IErrorLogger _uploadErrorLog;
        Boolean isSuccess;
        StringBuilder logger;
        public DocumentUploadRepository(newsDbContext newsDbContext,IErrorLogger errorLogger)
        {
            _newsDbContext = newsDbContext;
            _uploadErrorLog = errorLogger;
           
        }

        public async Task<List<object>> GetImageDetils()
        {
          
                   
               var res = (from filetable in _newsDbContext.tbl_UploadedFiles
                           orderby filetable.Id descending
                           select new
                           {

                               filetable.Name,
                               filetable.fileUrl,
                               filetable.Description,
                               filetable.fileType
                           }).Take(10).ToList();
       
                return res.Cast<object>().ToList(); // Cast to object in case you have to return a generic list
        }

        public string SaveImageDetilsToDatabase(tbl_UploadedFilesForPost tbl_UploadedFilesForPost)
        {

            try
            {
                logger= new StringBuilder();
                logger = logger.Append("Attempting to create record for uploaded file in Database");
                _uploadErrorLog.uploadFileErrorLog(logger);
                _newsDbContext.tbl_UploadedFiles.Add(tbl_UploadedFilesForPost);
                _newsDbContext.SaveChanges();
                isSuccess = true;
                logger.Append("File =>> " + tbl_UploadedFilesForPost.Description + " Records Has been created Successfully in Database");

                _uploadErrorLog.uploadFileErrorLog(logger);
            }

            catch (Exception ex)
            {
                logger = logger.Append(ex);
                _uploadErrorLog.uploadFileErrorLog(logger);
            }
           
         return logger.ToString();
        }

        public async Task<string> SaveImageToDirectory(FileuploadingModelBuilder file, int folderType)
        {
            string FolderName = "";
            string FileType = "";
            StringBuilder errors;
            string pathcore;
            string path;
            string staticfilepath;

            string databasepath; //done some modification here and retrun this 
            

            if (folderType==1)
            {
                FolderName = "Miscellaneous";
                
            }

            else if(folderType==2)
            {
                FolderName = "FeaturedImage";
               
            }
            
            else
            {
                FolderName = "Advertisements";
                
            }

            _uploadErrorLog.uploadFileCurrentLog("Collecting Information to upload in "+FolderName+ " Folder");


            string extension = Path.GetExtension(file.FormFile.FileName); //To Get Extention of File


            if( extension==".jpg" || extension==".jpeg" || extension == ".png" || extension == ".gif")
            {
                FileType = "Images";
  
            }

            else if (extension == ".Mp4")
            {
                FileType = "Videos";
            }

            else
            {
                FileType = "Documents";
            }

            _uploadErrorLog.uploadFileCurrentLog("Collecting Information to upload  " + FileType + " in " + FolderName);
           
            var filename = "_" + DateTime.Now.ToString("yy''MM''dd'_'HH''mm''ss_") + extension;  //Sanitize File Name
            
            try
            {
               pathcore = "wwwroot/DocumentUploads/"+ FolderName+"/"+FileType; //Document Root for each logged in User
               staticfilepath= "../DocumentUploads/"+ FolderName+"/"+FileType;
               databasepath = "/DocumentUploads/" + FolderName + "/" + FileType+"/";




                _uploadErrorLog.uploadFileCurrentLog("Setting Up Document Root To:"+ pathcore);
              
                //if Path not available then create path
                if (!Directory.Exists(pathcore))
                {
                    try
                    {
                        _uploadErrorLog.uploadFileCurrentLog("Attempting To Create Location:" + pathcore);
                        Directory.CreateDirectory(pathcore);
                        _uploadErrorLog.uploadFileCurrentLog("SuccesFully created directory :" + pathcore);
                    }

                    catch(Exception ex)
                    {
                        _uploadErrorLog.uploadFileCurrentLog("Failed Creating Directory at:" + pathcore +" Excepction" +ex.ToString());
                    }
                   
                }

                else
                {

                    _uploadErrorLog.uploadFileCurrentLog("Path :" + pathcore + " Already Exist");
                    _uploadErrorLog.uploadFileCurrentLog("Attempting to upload file at :" + pathcore);
                }



                try
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(), pathcore, filename);   // create path for local folder store inside wwwroot folder//
                    staticfilepath =Path.Combine(staticfilepath, filename);
                    databasepath = databasepath+filename;
                    using (Stream stream = new FileStream(path, FileMode.Create))    //save in local folder//
                    {
                        await file.FormFile.CopyToAsync(stream);
                    }
                    _uploadErrorLog.uploadFileCurrentLog("File Uploaded SuccessFully At :" + databasepath);
                }

                catch (Exception ex)
                {
                    _uploadErrorLog.uploadFileCurrentLog("Failed to upload File at :" + pathcore);
                }

                return databasepath;
           

           }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
