using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.FileService;

namespace SocialMedia.APi.Controller
{
    public class FileController : ControllerBase
    {
        private readonly IfileService _ifileService;
        public FileController(IfileService ifileService)
        {
            _ifileService = ifileService;
        }
        [HttpPost("UploadMultiFile")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile[] files)
        {
            var (filePaths, error) = await _ifileService.Upload(files);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(new { Error = error });
            }

            return Ok(new { Files = filePaths });
        }
    }
}
