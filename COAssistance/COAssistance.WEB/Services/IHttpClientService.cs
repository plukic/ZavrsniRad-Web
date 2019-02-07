using System.Net.Http;
using System.Threading.Tasks;

namespace COAssistance.WEB.Services
{
    public interface IHttpClientService
    {
        #region Methods

        Task<HttpResponseMessage> SendRequest<T>(string requestUri, HttpMethod httpMethod, T content);

        Task<HttpResponseMessage> PostUrlEncodedContent(string requestUri, HttpContent content);

        Task<HttpResponseMessage> GetJsonContent(string requestUri);

        Task<HttpResponseMessage> PostMultiPart(string requestUri, byte[] bytes, string fileName);

        #endregion Methods
    }
}