using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace COAssistance.API.Services.FileService
{
    /// <summary>
    /// FileService interface implementation
    /// </summary>
    public class FileService : IFileService
    {
        #region Methods

        public void SaveFile(HttpPostedFile file, string fileName = "")
        {
            if (file == null || file.ContentLength <= 0)
            {
                throw new ArgumentNullException(nameof(file));
            }

            var relativePath = ConfigurationManager.AppSettings["App.ImageSaveLocation"] ?? throw new ArgumentNullException("save path");
            var fullName = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + (relativePath + (string.IsNullOrEmpty(fileName) ? file.FileName : fileName)));

            file.SaveAs(fullName);
        }

        public void RemoveFile(string fileName, string filePath = "")
        {
            if (string.IsNullOrEmpty(fileName)) return;

            var directoryPath = Path.GetFullPath(
                this.AbsolutePath +
                (string.IsNullOrEmpty(filePath) ? this.TargetRelativePath : filePath));

            DirectoryInfo directory = new DirectoryInfo(directoryPath);

            var file = directory.GetFiles(fileName)
                .FirstOrDefault()?.FullName;

            if (!string.IsNullOrEmpty(file))
            {
                File.Delete(file);
            }
        }

        public string TargetRelativePath
        {
            get
            {
                return ConfigurationManager.AppSettings["App.ImageSaveLocation"] ?? throw new ArgumentNullException("path");
            }
        }

        public string AbsolutePath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        #endregion Methods
    }
}