using System;
using System.Linq;
using System.Threading.Tasks;
using AracTakipSistemi.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using AracTakipSistemi.Security.Token;
using AracTakipSistemi.Domain.Services;
using AracTakipSistemi.Domain.Responses;
using AracTakipSistemi.ResourceViewModel;
using Mapster;
using System.Security.Claims;

namespace AracTakipSistemi.Services
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private readonly ITokenHandler tokenHandler;
        private readonly CustomTokenOptions tokenOptions;
        private readonly IUserService userService;
        
        public AuthenticationService(IUserService userService, ITokenHandler tokenHandler, IOptions<CustomTokenOptions> tokenOptions, UserManager<Users> userManager, SignInManager<Users> signInManager, RoleManager<Users> roleManager) : base(userManager, signInManager, roleManager)
        {
            this.tokenHandler = tokenHandler;
            this.userService = userService;
            this.tokenOptions = tokenOptions.Value;
        }

        public async Task<BaseResponse<AccessToken>> CreateAccessTokenByRefreshToken(RefreshTokenViewModelResource refreshTokenViewModel)
        {
            var userClaim = await userService.GetUserByRefreshToken(refreshTokenViewModel.RefreshToken);
            if (userClaim.Item1 != null)
            {
                AccessToken accessToken = tokenHandler.CreateAccessToken(userClaim.Item1);
                Claim refreshTokenClaim = new Claim("refreshToken", accessToken.RefreshToken);
                Claim refreshTokenEndDateClaim = new Claim("refreshTokenEndDate", DateTime.Now.AddMinutes(tokenOptions.RefreshTokenExpiration).ToString());

                await userManager.ReplaceClaimAsync(userClaim.Item1, userClaim.Item2[0], refreshTokenClaim);
                await userManager.ReplaceClaimAsync(userClaim.Item1, userClaim.Item2[1], refreshTokenEndDateClaim);

                return new BaseResponse<AccessToken>(accessToken);
            }

            else
            {
                return new BaseResponse<AccessToken>("Böyle bir refreshToken'a sahip bir kullanıcı bulunamadı.");
            }
        }

        public async Task<BaseResponse<AccessToken>> RevokeRefreshToken(RefreshTokenViewModelResource refreshTokenViewModel)
        {
            bool result = await userService.RevokeRefreshToken(refreshTokenViewModel.RefreshToken);

            if (result)
            {
                return new BaseResponse<AccessToken>(new AccessToken());
            }

            else
            {
                return new BaseResponse<AccessToken>("RefreshToken veritabanında bulunamadı");
            }
        }

        public async Task<BaseResponse<AccessToken>> SignIn(SignInViewModelResource signInViewModel)
        {
            Users user = await userManager.FindByEmailAsync(signInViewModel.Email);
            if (user != null)
            {
                bool isUser = await userManager.CheckPasswordAsync(user, signInViewModel.Password);
                if (isUser)
                {
                    AccessToken accessToken = tokenHandler.CreateAccessToken(user);
                    Claim refreshTokenClaim = new Claim("refreshToken", accessToken.RefreshToken);
                    Claim refreshTokenEndDateClaim = new Claim("refreshTokenEndDate", DateTime.Now.AddMinutes(tokenOptions.RefreshTokenExpiration).ToString());

                    List<Claim> refreshClaimList = userManager.GetClaimsAsync(user).Result.Where(c => c.Type.Contains("refreshToken")).ToList();

                    if (refreshClaimList.Any())
                    {
                        await userManager.ReplaceClaimAsync(user, refreshClaimList[0], refreshTokenClaim);
                        await userManager.ReplaceClaimAsync(user, refreshClaimList[1], refreshTokenEndDateClaim);
                    }
                    else
                    {
                        await userManager.AddClaimsAsync(user, new[] { refreshTokenClaim, refreshTokenEndDateClaim });
                    }
                    return new BaseResponse<AccessToken>(accessToken);
                }
                return new BaseResponse<AccessToken>("Şifre yanlış!");
            }
            return new BaseResponse<AccessToken>("Email yanlış!");
        }

        public Task SignIn(object signInViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<UserViewModelResource>> SignUp(UserViewModelResource userViewModel)
        {
            Users user = new Users { UserName = userViewModel.UserName, Email = userViewModel.Email };

            IdentityResult result = await this.userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                return new BaseResponse<UserViewModelResource>(user.Adapt<UserViewModelResource>());
            }

            else
            {
                return new BaseResponse<UserViewModelResource>(result.Errors.First().Description);
            }
        }
    }
}
