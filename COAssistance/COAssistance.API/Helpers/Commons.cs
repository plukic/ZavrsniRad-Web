using System.Configuration;
using System.Web;

namespace COAssistance.API.Helpers
{
    public static class Commons
    {
        /// <summary>
        /// TEMPORARY SOLUTION FOR ABSOLUTE PATHS
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <remarks>Nije ovo Mirza radio</remarks>
        public static string GenerateAbsolutePath(string path)
        {
            var absolutePath = VirtualPathUtility.ToAbsolute(path);

            var domain = ConfigurationManager.AppSettings["App.BaseUrl"];

            return $"{domain}{absolutePath}";
        }
    }
}