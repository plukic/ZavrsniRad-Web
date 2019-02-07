using COAssistance.COMMONS.Extensions;
using COAssistance.COMMONS.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace COAssistance.WEB.Extensions
{
    /// <summary>
    /// Extends existing HtmlHelper methods
    /// </summary>
    public static class HtmlHelperExtensions
    {
        #region Fields

        private static readonly string _requiredFieldCharacter = "*";
        private static readonly double _defaultLatitude = 43.21718417050045;
        private static readonly double _defaultLongitude = 17.992162739299715;
        private static readonly string _successClass = "text-success";
        private static readonly string _errorClass = "text-error";

        #endregion Fields

        #region Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="html"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static IHtmlString FaviconStatusIcon(this HtmlHelper html, bool status)
        {
            var tagBuilder = new TagBuilder("i");
            var cssClass = status ? $"fa fa-check {_successClass}" : $"fa fa-times {_errorClass}";

            tagBuilder.AddCssClass(cssClass);

            return new HtmlString(tagBuilder.ToString());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="html"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static IHtmlString StatusText(this HtmlHelper html, bool status, string textTrue, string textFalse)
        {
            var tagBuilder = new TagBuilder("strong");
            var cssClass = status ? _successClass : _errorClass;

            tagBuilder.AddCssClass(cssClass);
            tagBuilder.SetInnerText(status ? textTrue : textFalse);

            return new HtmlString(tagBuilder.ToString());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="html"></param>
        /// <param name="status"></param>
        /// <param name="trueIcon"></param>
        /// <param name="falseIcon"></param>
        /// <returns></returns>
        public static IHtmlString StatusIcon(this HtmlHelper html, bool status, string trueIcon, string falseIcon)
        {
            string icon = status ? trueIcon : falseIcon;

            return new HtmlString($"<i class=\"{icon}\"></i>");
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static IHtmlString LabelRequiredFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var labelName = ExpressionHelper.GetExpressionText(expression);
            var labelText = metadata.DisplayName ?? metadata.PropertyName;

            if (labelText.IsNullOrEmpty())
            {
                return new HtmlString(string.Empty);
            }

            var labelTag = new TagBuilder("label");
            labelTag.Attributes.Add("for", html.ViewData.TemplateInfo.GetFullHtmlFieldId(labelName));
            labelTag.SetInnerText(labelText);

            var spanTag = new TagBuilder("span");
            spanTag.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            spanTag.Attributes.Add("title", Resource.RequiredField);
            spanTag.SetInnerText(_requiredFieldCharacter);

            labelTag.InnerHtml = labelText + spanTag.ToString();

            return new HtmlString(labelTag.ToString());
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="html"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IHtmlString FaviconByEnum<TEnum>(this HtmlHelper html, Enum value)
        {
            var icon = value.GetDescription<TEnum>();

            return new HtmlString(!string.IsNullOrEmpty(icon) ? $"<i class=\"fa fa-{icon} margin-r-5\"></i>" : "");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="html"></param>
        /// <param name="excludePropertyErrors"></param>
        /// <returns></returns>
        public static IHtmlString ValidationSummaryWithCheck(this HtmlHelper html, bool excludePropertyErrors = false)
        {
            if (!html.ViewData.ModelState.IsValid)
            {
                var validationSummary = html.ValidationSummary(excludePropertyErrors);

                if (validationSummary != null)
                {
                    return validationSummary.ToString().Contains("display:none") ?
                        null :
                        validationSummary;
                }
            }

            return new HtmlString(string.Empty);
        }

        /// <summary>
        /// Returns html field name for selected property
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string FieldNameFor<TModel, TResult>(this HtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression)
        {
            return html.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));
        }

        /// <summary>
        /// Returns html field id for selected property
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string FieldIdFor<TModel, TResult>(this HtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression)
        {
            return html.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="html"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IHtmlString LoadMapScript(this HtmlHelper html, params KeyValuePair<string, object>[] parameters)
        {
            var apiKey = ConfigurationManager.AppSettings["ApiKey"];

            if (!string.IsNullOrEmpty(apiKey))
            {
                var template = $"<script src=\"https://maps.googleapis.com/maps/api/js?key={apiKey}";

                foreach (var item in parameters)
                {
                    template += $"&{item.Key}={item.Value}";
                }

                template += "\"></script>";

                return new HtmlString(template);
            }

            return new HtmlString(string.Empty);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="html"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public static IHtmlString GenerateMapUrl(this HtmlHelper html, string linkText, double latitude, double longitude)
        {
            latitude = latitude.HasDefaultValue() ? _defaultLatitude : latitude;
            longitude = longitude.HasDefaultValue() ? _defaultLongitude : longitude;

            var tagBuilder = new TagBuilder("a");

            tagBuilder.MergeAttribute("href", $"http://www.google.com/maps/place/{latitude},{longitude}");
            tagBuilder.MergeAttribute("target", "_blank");
            tagBuilder.SetInnerText(linkText.IsNullOrEmpty() ? "Link" : linkText);

            return new HtmlString(tagBuilder.ToString());
        }

        /// <summary>
        /// Generates anchor element with passed arguments
        /// </summary>
        /// <param name="html">Current HtmlHelper instance</param>
        /// <param name="linkText">Link text</param>
        /// <param name="url">Target url</param>
        /// <param name="openInNewWindow">Flag whether new tab should be used</param>
        /// <param name="htmlAttributes">HtmlAttributes object</param>
        /// <returns>HTML encoded string</returns>
        public static IHtmlString Anchor(this HtmlHelper html, string linkText, string url, bool openInNewWindow = false, object htmlAttributes = null)
        {
            var tagBuilder = new TagBuilder("a");
            var targetUrl = url.IsNullOrEmpty() ? "#" : url;

            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            tagBuilder.MergeAttribute("href", targetUrl);
            tagBuilder.SetInnerText(linkText);

            if (openInNewWindow && targetUrl != "#")
            {
                tagBuilder.MergeAttribute("target", "_blank");
            }

            return new HtmlString(tagBuilder.ToString());
        }

        /// <summary>
        /// Renders img element with specified attributes
        /// </summary>
        /// <param name="htmlHelper">Current HtmlHelper instance</param>
        /// <param name="src">Image source</param>
        /// <param name="alt">Alternative text</param>
        /// <param name="htmlAttributes">Html attributes object</param>
        /// <returns>HTML encoded string</returns>
        public static IHtmlString Image(this HtmlHelper htmlHelper, string src, string alt, object htmlAttributes = null)
        {
            if (src.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(src));

            // Make sure we convert virtual path(starts with ~) to absolute path.
            // First we should check if path is already absolute(ie. http://myapp.com/content) or not.
            if (!src.Contains("://"))
            {
                src = VirtualPathUtility.ToAbsolute(src);
            }

            var tagBuilder = new TagBuilder("img");

            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            tagBuilder.MergeAttribute("src", src);
            tagBuilder.MergeAttribute("alt", alt);

            return new HtmlString(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IHtmlString FileFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            // Finish later for gasstations
            return new MvcHtmlString("");
        }

        /// <summary>
        /// Retrieves current controller name
        /// </summary>
        /// <param name="htmlHelper">Current HtmlHelper instance</param>
        /// <returns>Controller name</returns>
        public static string ControllerName(this HtmlHelper htmlHelper)
        {
            var controller = htmlHelper.ViewContext.RequestContext.HttpContext.Request.RequestContext.RouteData.Values["controller"] as string;

            if (controller.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(controller));
            }

            return controller;
        }

        #endregion Methods
    }
}