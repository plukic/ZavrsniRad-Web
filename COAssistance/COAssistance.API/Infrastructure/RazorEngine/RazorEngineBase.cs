using RazorEngine.Templating;
using System.Web.Mvc;

namespace COAssistance.API.Infrastructure.RazorEngine
{
    /// <summary>
    /// Extends existing RazorEngine template
    /// </summary>
    /// <typeparam name="T">Model type</typeparam>
    /// <remarks>
    /// TODO: extend later
    /// </remarks>
    public abstract class RazorEngineBase<T> : TemplateBase<T>
    {
        public UrlHelper Url { get; set; }

        protected RazorEngineBase(UrlHelper url)
        {
            Url = url;
        }
    }
}