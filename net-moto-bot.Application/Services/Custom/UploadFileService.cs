using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using net_moto_bot.Application.Interfaces.Custom;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Enums.Custom;
using net_moto_bot.Domain.Exceptions.BadRequest;
using System.Text;

namespace net_moto_bot.Application.Services.Custom;

public class UploadFileService(
    IWebHostEnvironment _environment,
    IConfiguration configuration
) : IUploadFileService
{
    private readonly string _host = configuration["Host:URL"]!;

    private static string GenerateCode(short length = 20)
    {
        char[] _chars =
        [
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
                'k', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
                'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
                'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'o',
                'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y',
                'z', '0', '1', '2', '3', '4', '5', '6', '7', '8',
                '9'
        ];
        StringBuilder sb = new();
        Random random = new();
        for (int i = 0; i < length; i++)
        {
            sb.Append(_chars[random.Next(0, _chars.Length)]);
        }
        return sb.ToString();
    }

    private static void CreateDirectory(string path)
    {
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
    }

    public async Task<ProductFile> UploadImageFile(IFormFile file)
    {
        string profileImagePath = _environment.WebRootPath + "/public/" + "motobot" + "/images/";
        string fileExtension = Path.GetExtension(file.FileName);

        if (!fileExtension.Equals(".png") && !fileExtension.Equals(".jpg") && !fileExtension.Equals(".jpeg")) throw new InvalidFieldException(ExceptionEnum.InvalidExtensionType);

        string filename = GenerateCode(20) + Path.GetExtension(file.FileName);
        CreateDirectory(profileImagePath);
        using FileStream fileStream = File.Create(profileImagePath + filename);
        await file.CopyToAsync(fileStream);
        await fileStream.FlushAsync();
        string imageUrl = _host + "/public/images/profile/" + filename;

        return new()
        {
            Url = imageUrl,
            Active = true,
        };
    }

    public async Task<List<ProductFile>> UploadFiles(List<IFormFile> files)
    {
        string profileImagePath = _environment.WebRootPath + "/public/" + "motobot" + "/images/";
        CreateDirectory(profileImagePath);

        List<ProductFile> uploadedFiles = [];

        foreach (IFormFile file in files)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            string filename = GenerateCode(20) + Path.GetExtension(file.FileName);

            if (!fileExtension.Equals(".png") && !fileExtension.Equals(".jpg") && !fileExtension.Equals(".jpeg")) throw new InvalidFieldException(ExceptionEnum.InvalidExtensionType);

            using FileStream fileStream = System.IO.File.Create(profileImagePath + filename);
            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();

            string imageUrl = _host + "/public/" + "motobot" + "/images/" + filename;

            uploadedFiles.Add(new ProductFile
            {
                Url = imageUrl,
                Active = true,
            });
        }

        return uploadedFiles;
    }

    public async Task<string> UploadImageFileAsync(IFormFile file)
    {
        string profileImagePath = _environment.WebRootPath + "/public/" + "motobot" + "/images/";
        string fileExtension = Path.GetExtension(file.FileName);

        if (!fileExtension.Equals(".png") && !fileExtension.Equals(".jpg") && !fileExtension.Equals(".jpeg")) throw new InvalidFieldException(ExceptionEnum.InvalidExtensionType);

        string filename = GenerateCode(20) + Path.GetExtension(file.FileName);
        CreateDirectory(profileImagePath);
        using FileStream fileStream = File.Create(profileImagePath + filename);
        await file.CopyToAsync(fileStream);
        await fileStream.FlushAsync();
        string imageUrl = _host + "/public/images/profile/" + filename;

        return imageUrl;
    }

    public async Task<List<string>> UploadFilesAsync(List<IFormFile> files)
    {
        string profileImagePath = _environment.WebRootPath + "/public/" + "motobot" + "/images/";
        CreateDirectory(profileImagePath);

        List<string> uploadedFiles = [];

        foreach (IFormFile file in files)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            string filename = GenerateCode(20) + Path.GetExtension(file.FileName);

            if (!fileExtension.Equals(".png") && !fileExtension.Equals(".jpg") && !fileExtension.Equals(".jpeg")) throw new InvalidFieldException(ExceptionEnum.InvalidExtensionType);

            using FileStream fileStream = System.IO.File.Create(profileImagePath + filename);
            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();

            string imageUrl = _host + "/public/" + "motobot" + "/images/" + filename;

            uploadedFiles.Add(imageUrl);
        }

        return uploadedFiles;
    }

    public int DeleteAllFilesAsync(List<string> filesPath)
    {
        foreach (string url in filesPath)
        {
            Uri uri = new(url);
            string path = "wwwroot" + uri.AbsolutePath;
            if (File.Exists(path)) File.Delete(path);
        }
        return filesPath.Count;
    }

    public static string GetFolderName(string fileExtension)
    {
        return fileExtension.ToLower() switch
        {
            ".png" or ".jpg" or ".jpeg" => "images",
            ".xlsx" or ".xls" => "excel",
            ".mp4" => "videos",
            ".pdf" or ".doc" or ".docx" => "documentos",
            _ => throw new InvalidFieldException(ExceptionEnum.InvalidExtensionType),
        };
    }
}
