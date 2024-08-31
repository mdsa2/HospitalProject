using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.FileService
{
    public interface IfileService
    {
        Task<(string? file, string? error)> Upload(IFormFile file);
        Task<(List<string>? files, string? error)> Upload(IFormFile[] files);
    }
    public class fileservice : IfileService
    {
        public async Task<(string? file, string? error)> Upload(IFormFile file)
        {
            try
            {
                // Generate a unique file name using a GUID
                var id = Guid.NewGuid();
                var extension = Path.GetExtension(file.FileName);
                var fileName = $"{id}{extension}";

                // Define the directory to save the files
                var attachmentsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Attachments");

                // Create the directory if it doesn't exist
                if (!Directory.Exists(attachmentsDir))
                {
                    Directory.CreateDirectory(attachmentsDir);
                }

                // Combine the directory and file name to get the full path
                var path = Path.Combine(attachmentsDir, fileName);

                // Save the file to the specified path
                await using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);

                // Return the relative path to the file
                var filePath = Path.Combine("Attachments", fileName);
                return (filePath, null);
            }
            catch (Exception ex)
            {
                // Return null and the error message if an exception occurs
                return (null, ex.Message);
            }
        }
        public async Task<(List<string>? files, string? error)> Upload(IFormFile[] files)
        {
            var fileList = new List<string>();

            try
            {
                foreach (var file in files)
                {
                    var (filePath, error) = await Upload(file);


                    if (error != null)
                    {
                        return (null, error);
                    }


                    fileList.Add(filePath!);
                }

                return (fileList, null);
            }
            catch (Exception ex)
            {

                return (null, ex.Message);
            }
        }
    }
}