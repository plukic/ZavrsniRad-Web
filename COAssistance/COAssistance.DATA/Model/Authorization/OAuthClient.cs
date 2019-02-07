using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COAssistance.DATA.Model.Authorization
{
    public class OAuthClient
    {
        public string Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }

        public ICollection<OAuthClientRoles> OAuthClientRoles { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }

    }
}
