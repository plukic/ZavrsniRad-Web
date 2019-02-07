using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COAssistance.API.Services.EmailService
{
   public  interface IEmailService
    {
        Task SendTemplateEmail<T>(string emailTo, string subject, string viewName, T model, string path = "") where T : class;
        Task Send(string to, string subject, string body);

    }
}
