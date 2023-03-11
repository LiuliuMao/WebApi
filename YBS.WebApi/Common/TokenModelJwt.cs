using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class TokenModelJwt
    {
        public string UserId { get; set; }
    }
    public class UserCredentials
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class RefreshCredentials
    {
        [Required]
        public string OldToken { get; set; }
    }
}
