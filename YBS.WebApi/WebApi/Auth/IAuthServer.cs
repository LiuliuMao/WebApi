using Common;
using Model;
using Model.ViewEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Auth
{
    public interface IAuthServer
    {

        AuthResult CreateAuthentication(UserViewModel user);


        TokenModelJwt SerializeJwt(string oldToken);

    }
}
