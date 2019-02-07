using System.Web;

namespace COAssistance.API.Services.FileService
{
    /// <summary>
    /// FileService interface
    /// </summary>
    public interface IFileService
    {
        #region Methods

        void SaveFile(HttpPostedFile file, string fileName = "");

        void RemoveFile(string fileName, string filePath = "");

        #endregion Methods
    }
}