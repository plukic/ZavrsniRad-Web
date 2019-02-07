using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COAssistance.COMMONS.Models
{
    class ConfirmationModelGeneric<T>:ConfirmationModel
    {
        public T AdditionalData { get; set; }
    }
}
