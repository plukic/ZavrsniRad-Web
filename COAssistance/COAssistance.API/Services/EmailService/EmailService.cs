using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace COAssistance.API.Services.EmailService
{
    public class EmailService : IEmailService
    {
        public string EmailFrom { get; set; }

        private string PrepareTemplateFile(string viewName, string path = "")
        {
            // Append file extension if it doesn't exist
            if (!viewName.EndsWith(".cshtml"))
                viewName += ".cshtml";

            // Append file location
            string templateLocation = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory
                + (string.IsNullOrEmpty(path) ? "/Views/Shared/EmailTemplates/" : path)
                + viewName);

            // Throw exception if template file is not found
            if (!File.Exists(templateLocation))
                throw new ArgumentNullException(nameof(templateLocation));

            return templateLocation;
        }

        private SmtpClient GetSmtpClient()
        {
            var SmtpClient = new SmtpClient
            {
                Host = ConfigurationManager.AppSettings["EmailHost"],
                EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EmailEnableSsl"]),
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential
                {
                    UserName = ConfigurationManager.AppSettings["EmailUsername"],
                    Password = ConfigurationManager.AppSettings["EmailPassword"]
                },
                Port = int.Parse(ConfigurationManager.AppSettings["EmailPort"])
            };

            return SmtpClient;
        }

        public async Task SendTemplateEmail<T>(string emailTo, string subject, string viewName, T model, string path = "") where T : class
        {
            string templateLocation = PrepareTemplateFile(viewName, path);
            string targetHtml = File.ReadAllText(templateLocation);
            string htmlResult = string.Empty;

            try
            {
                htmlResult = Engine.Razor.RunCompile(targetHtml, viewName, typeof(T), model);
            }
            catch (Exception e)
            {
                throw new Exception("Razor Run/Compile error");
            }

            using (var message = new MailMessage())
            {
                message.Subject = subject;
                message.Body = htmlResult;
                message.IsBodyHtml = true;
                message.From = new MailAddress(EmailFrom);
                message.To.Add(emailTo);

                using (var smtp = GetSmtpClient())
                {
                    await smtp.SendMailAsync(message);
                }
            }
        }

        public async Task Send(string to, string subject, string body)
        {
            using (MailMessage objMailMessage = new MailMessage())
            {
                objMailMessage.From = new MailAddress(EmailFrom);
                objMailMessage.To.Add(new MailAddress(to));
                objMailMessage.Subject = subject;
                objMailMessage.Body = body;

                using (var smtp = GetSmtpClient())
                {
                    await smtp.SendMailAsync(objMailMessage);
                }
            }
        }
    }
}