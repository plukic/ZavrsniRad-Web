using System;

namespace COAssistance.COMMONS.Models.Staff
{
    public class StaffModel : BaseModel<string>
    {
        public bool IsActive { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public DateTime? LastLoginTime { get; set; }
    }
}