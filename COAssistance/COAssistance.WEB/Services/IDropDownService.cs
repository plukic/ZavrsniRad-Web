using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace COAssistance.WEB.Services
{
    public interface IDropDownService
    {
        SelectList GetLanguages();
    }
}
