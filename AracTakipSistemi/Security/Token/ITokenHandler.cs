using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AracTakipSistemi.Models;

namespace AracTakipSistemi.Security.Token
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(Users user);
        void RevokeRefreshToken(Users user);
    }
}
