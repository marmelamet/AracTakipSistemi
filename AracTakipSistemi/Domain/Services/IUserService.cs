using AracTakipSistemi.Domain.Responses;
using AracTakipSistemi.Models;
using AracTakipSistemi.ResourceViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AracTakipSistemi.Domain.Services
{
    public interface IUserService
    {
        Task<BaseResponse<UserViewModelResource>> UpdateUser(UserViewModelResource userViewModel, string userName);
        Task<Users> GetUserByUserName(string userName);
        Task<BaseResponse<Users>> UploadUserPicture(string picturePath, string userName);
        Task<Tuple<Users, IList<Claim>>> GetUserByRefreshToken(string refreshToken);
        Task<bool> RevokeRefreshToken(string refreshToken);
    }
}
