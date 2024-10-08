using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;


namespace City.info.api.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        FileExtensionContentTypeProvider FileExtensionContentTypeProvider;
        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {

            FileExtensionContentTypeProvider = fileExtensionContentTypeProvider;    

        }

        [HttpGet("{fileId}")]
        public ActionResult GetFile(string fileId)
        {
            //string pathToFile1 = "downloadFiles/download.jpg";
            string pathToFile2 = "downloadFiles/image.rar";
            //string pathToFile3 = "downloadFiles/pdf.rar";
            string pathToFile4 = "downloadFiles/p.pdf";

            if (!System.IO.File.Exists(pathToFile2))
            { 
                return NotFound();
            }

            var bytes = System.IO.File.ReadAllBytes(pathToFile2);

            if(!FileExtensionContentTypeProvider.TryGetContentType(pathToFile2,out var contentType))
            {
                contentType = "application/octet-steam";
            }

            return File(bytes, contentType, Path.GetFileName(pathToFile2) );
            //return File(bytes,"application/pdf",Path.GetFileName(pathToFile4) );
        }
    }
}
