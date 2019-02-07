using COAssistance.DATA.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COAssistance.API.Services.Logger
{
    public interface ILoggerService
    {
        void Log(string userId, ActionType action, string description);
    }
}