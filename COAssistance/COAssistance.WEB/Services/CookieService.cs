using System;
using System.Web;

namespace COAssistance.WEB.Services
{
    public class CookieService : ICookieService
    {
        #region Methods

        public void Add(string name, string value, int expirationTime = 50)
        {
            var cookie = this.Get(name);

            if (cookie == null)
            {
                cookie = new HttpCookie(name, value)
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(expirationTime)
                };
            }

            cookie.Value = value;

            this.Add(cookie);
        }

        public void Add(HttpCookie cookie)
        {
            HttpContext.Current.Response.SetCookie(cookie);
        }

        public bool Exists(string name)
        {
            return HttpContext.Current.Request.Cookies[name] != null;
        }

        public HttpCookie Get(string name)
        {
            return HttpContext.Current.Request.Cookies[name];
        }

        public void Remove(string name)
        {
            var cookie = this.Get(name);

            if (cookie == null) return;

            cookie.Expires = DateTime.UtcNow.AddDays(-2);

            this.Add(cookie);
        }

        #endregion Methods
    }
}