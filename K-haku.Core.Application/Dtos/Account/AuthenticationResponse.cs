using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Application.DTOS.Account
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set;}
        public string Email { get; set; }
        public string ImageProfile { get; set; }

        public List<string> Roles { get; set; }
        public bool IsVerified { get; set; }
        public bool HasError { get; set; } = false;
        public string Error { get; set; } 

    }
}
