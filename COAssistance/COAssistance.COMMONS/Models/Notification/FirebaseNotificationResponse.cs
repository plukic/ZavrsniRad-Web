using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COAssistance.COMMONS.Models.Notification
{
    public class FirebaseNotificationResponse
    {
        public bool success { get; set; }
        public FirebaseResponse results { get; set; }
    }
    public class FirebaseResponse
    {
        public string error { get; set; }
    }
}
