using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace COAssistance.WEB.Services
{
    /// <summary>
    /// FileService interface implementation
    /// </summary>
    public class FileService : IFileService
    {
        #region Methods        

        public void SaveFile(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength <= 0)
            {
                throw new ArgumentNullException(nameof(file));
            }

            var relativePath = ConfigurationManager.AppSettings["App.ImageSaveLocation"] ??
                throw new ArgumentNullException("relativePath");

            file.SaveAs(Path.Combine(relativePath + file.FileName));
        }

        #endregion Methods
    }
}