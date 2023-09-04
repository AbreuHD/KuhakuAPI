using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs.Account
{
    public class JWTResponse
    {
        public bool Success { get; set; } = true;
        public int Statuscode { get; set; }
        public string Message { get; set; }
    }
}
