using Microsoft.AspNet.Identity.EntityFramework;

namespace COAssistance.DATA.Model.Authorization
{
    public class OAuthClientRoles
    {
        public int Id { get; set; }
        public string OAuthClientId { get; set; }
        public OAuthClient OAuthClient { get; set; }
        public string IdentityRoleId { get; set; }
        public IdentityRole IdentityRole { get; set; }
    }
}