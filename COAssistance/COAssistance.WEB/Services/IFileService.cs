using System.Web;

namespace COAssistance.WEB.Services
{
    /// <summary>
    /// FileService interface
    /// </summary>
    public interface IFileService
    {
        #region Methods

        void SaveFile(HttpPostedFileBase file);

        #endregion Methods
    }
}