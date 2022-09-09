using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Dtos.Account
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public String Token { get; set; }
        public DateTime Expires { get; set; }
        public bool isExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public String ReplacedByToken { get; set; }
        public bool isActive => Revoked == null && !isExpired;
    }
}
