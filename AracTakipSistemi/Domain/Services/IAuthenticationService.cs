﻿using AracTakipSistemi.Domain.Responses;
using AracTakipSistemi.ResourceViewModel;
using AracTakipSistemi.Security.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AracTakipSistemi.Domain.Services
{
    public interface IAuthenticationService
    {
        Task<BaseResponse<UserViewModelResource>>SignUp(UserViewModelResource userViewModel);

        Task<BaseResponse<AccessToken>>SignIn(SignInViewModelResource signInViewModel);

        Task<BaseResponse<AccessToken>> CreateAccessTokenByRefreshToken(RefreshTokenViewModelResource refreshTokenViewModel);

        Task<BaseResponse<AccessToken>> RevokeRefreshToken(RefreshTokenViewModelResource refreshTokenViewModel);
        Task SignIn(object signInViewModel);
    }
}
