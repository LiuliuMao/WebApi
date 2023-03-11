using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Auth
{
    public interface IAuthServer
    {

        AuthResult CreateAuthentication(UserInfo user);


        TokenModelJwt SerializeJwt(string oldToken);

    }
}
