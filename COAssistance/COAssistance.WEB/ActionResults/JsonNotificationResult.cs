using COAssistance.WEB.Infrastructure;
using System.Web.Mvc;

namespace COAssistance.WEB.ActionResults
{
    /// <summary>
    /// Action result used to return notifications in JSON format
    /// </summary>
    public class JsonNotificationResult : ActionResult
    {
        public Notification Notification { get; private set; }

        public JsonNotificationResult(NotificationType notificationType, string text, string redirectUrl)
        {
            Notification = new Notification(notificationType, text, redirectUrl);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            new JsonResult
            {
                Data = this.Notification,
                MaxJsonLength = int.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            }
            .ExecuteResult(context);
        }
    }
}