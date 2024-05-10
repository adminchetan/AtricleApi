using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsService.Interface;
using NewsService.Models;

namespace NewsService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]


    public class ImageController : Controller
    {
        private readonly IDocumentHandler _IdocumentHandler;
        public string UserName;
        public ImageController(IDocumentHandler idocumentHandler)
        {
            _IdocumentHandler = idocumentHandler;
            UserName = "UserName";
        }

        [HttpPost]
        public async Task<IActionResult> UploadDocumets([FromForm] FileuploadingModelBuilder file)
        {
            string extension = Path.GetExtension(file.FormFile.FileName); //To Get Extention of File

            var filename = "_" + DateTime.Now.ToString("yy''MM''dd'_'HH''mm''ss_") + extension;  //Sanitize File Name
            try
            {
                string pathcore = "wwwroot/DocumentUploads/ForPost"; //Document Root for each logged in User
                                                                     //if Path not available then create path
                if (!Directory.Exists(pathcore))
                {
                    Directory.CreateDirectory(pathcore);
                }
                //if Path not available then create path

                string path = Path.Combine(Directory.GetCurrentDirectory(), pathcore, filename);   // create path for local folder store inside wwwroot folder//
                                                                                                   // 

                using (Stream stream = new FileStream(path, FileMode.Create))    //save in local folder//
                {
                    await file.FormFile.CopyToAsync(stream);
                }

                tbl_UploadedFilesForPost tbl_UploadedFilesForPost = new tbl_UploadedFilesForPost()   //save information to database
                {
                    Name = filename,
                    Description = file.Description,
                    fileUrl = "/DocumentUploads/ForPost" + "/" + filename,
                    fileType = 1
                };

                var res = _IdocumentHandler.SaveImageDetilsToDatabase(tbl_UploadedFilesForPost);
                //save information to database

                return StatusCode(StatusCodes.Status201Created);

            }
            catch (Exception ex)
            {
                return Json(new { status = StatusCode(StatusCodes.Status500InternalServerError), error = ex.Message });
            }
        }




        [HttpGet]
        public async Task<IActionResult> GetUploadedDocumentsAsync()
        {
            var res = await Task.Run(() =>
            {
                return _IdocumentHandler.GetImageDetils();

            });

            return Json(res);
        }

        [HttpPost]
        public async Task<IActionResult> SaveImageToDirectory([FromForm] FileuploadingModelBuilder file, int folderType)
        {
            var res1 = await Task.Run(() =>
            {
                return _IdocumentHandler.SaveImageToDirectory(file, folderType);
            });

            try
            {
                tbl_UploadedFilesForPost tbl_UploadedFilesForPost = new tbl_UploadedFilesForPost()   //save information to database
                {
                    Name = file.FileName,
                    Description = file.Description,
                    fileUrl = res1,
                    fileType = 1
                };

                var res = _IdocumentHandler.SaveImageDetilsToDatabase(tbl_UploadedFilesForPost);
                //save information to database

                return StatusCode(StatusCodes.Status201Created);
            }
            catch(Exception ex)
            {
                return Json(ex.ToString());
            }

        }
    }
}
