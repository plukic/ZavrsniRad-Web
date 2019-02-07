using COAssistance.WEB.Constants;
using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace COAssistance.WEB.Services
{
    /// <summary>
    /// HttpClient service implementation
    /// </summary>
    public class HttpClientService : IHttpClientService
    {
        #region Fields

        private readonly HttpClient _client;
        private readonly ICommonService _commonService;

        #endregion Fields

        #region Constructor

        public HttpClientService(HttpClient client, ICommonService commonService)
        {
            _client = client;
            _commonService = commonService;
        }

        #endregion Constructor

        #region Methods

        public async Task<HttpResponseMessage> GetJsonContent(string requestUri)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

            httpRequestMessage.Headers.Add("Authorization", $"Bearer {_commonService.GetTokenData(AssistanceConstants.Cookies.AuthenticationTokenKey).AccessToken}");

            var result = await _client.SendAsync(httpRequestMessage);

            return result;
        }

        public async Task<HttpResponseMessage> PostMultiPart(string requestUri, byte[] bytes, string fileName)
        {
            var form = new MultipartFormDataContent();
            var header = new ContentDispositionHeaderValue("form-data")
            {
                FileName = fileName,
                FileNameStar = fileName,
                Name = fileName
            };

            var content = new ByteArrayContent(bytes);

            content.Headers.ContentDisposition = header;
            form.Add(content);

            var result = await _client.PostAsync(requestUri, form);

            return result;
        }

        public async Task<HttpResponseMessage> PostMultiPart(string requestUri, string base64, string fileName)
        {
            var form = new MultipartFormDataContent();
            var header = new ContentDispositionHeaderValue("form-data")
            {
                FileName = fileName,
                FileNameStar = fileName,
                Name = fileName
            };

            string bytesTruncated = base64.Substring(22);
            byte[] bytes = Convert.FromBase64String(bytesTruncated);

            var content = new ByteArrayContent(bytes);

            content.Headers.ContentDisposition = header;
            form.Add(content);

            var result = await _client.PostAsync(requestUri, form);

            return result;
        }

        public async Task<HttpResponseMessage> PostUrlEncodedContent(string requestUri, HttpContent content)
        {
            var result = await _client.PostAsync(requestUri, content);

            return result;
        }

        public async Task<HttpResponseMessage> SendRequest<T>(string requestUri, HttpMethod httpMethod, T content)
        {
            var httpRequestMessage = new HttpRequestMessage(httpMethod, requestUri)
            {
                Content = new ObjectContent<T>(content, new JsonMediaTypeFormatter(), (MediaTypeHeaderValue)null)
            };

            httpRequestMessage.Headers.Add("Authorization", $"Bearer {_commonService.GetTokenData(AssistanceConstants.Cookies.AuthenticationTokenKey).AccessToken}");

            var result = await _client.SendAsync(httpRequestMessage);

            return result;
        }

        #endregion Methods
    }
}