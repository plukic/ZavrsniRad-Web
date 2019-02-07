using System.Web;

namespace COAssistance.WEB.Services
{
    public interface ICookieService
    {
        #region Methods

        HttpCookie Get(string name);

        bool Exists(string name);

        void Add(string name, string value, int expirationTime);

        void Add(HttpCookie cookie);

        void Remove(string name);

        #endregion Methods
    }
}