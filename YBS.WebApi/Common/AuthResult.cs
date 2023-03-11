using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class AuthResult
    {
        public string token { get; set; }
        public double ExpirySecond { get; set; }

        public bool success { get; set; }

        public string message { get; set; }
    }
}
