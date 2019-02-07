using System;

namespace COAssistance.WEB.Infrastructure
{
    [Serializable]
    public class Notification
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public string RedirectUrl { get; set; }

        public Notification(NotificationType notificationType, string text, string redirectUrl)
        {
            Type = notificationType.ToString().ToLower();
            Text = text;
            RedirectUrl = redirectUrl;
        }
    }
}