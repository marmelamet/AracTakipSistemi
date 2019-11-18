using AracTakipSistemi.Domain.Responses;
using AracTakipSistemi.Domain.Services;
using AracTakipSistemi.Models;
using AracTakipSistemi.ResourceViewModel;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AracTakipSistemi.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(UserManager<Users> userManager, SignInManager<Users> signInManager, RoleManager<Users> roleManager) : base(userManager, signInManager, roleManager)
        {
        }

        public async Task<Tuple<Users, IList<Claim>>> GetUserByRefreshToken(string refreshToken)
        {
            Claim claimRefreshToken = new Claim("refreshToken", refreshToken);

            var users = await userManager.GetUsersForClaimAsync(claimRefreshToken);

            if (users.Any())
            {
                var user = users.First();
                IList<Claim> userClaims = await userManager.GetClaimsAsync(user);
                string refreshTokenEndDate = userClaims.First(c => c.Type == "refreshTokenEndDate").Value;

                if (DateTime.Parse(refreshTokenEndDate) > DateTime.Now)
                {
                    return new Tuple<Users, IList<Claim>>(user, userClaims);
                }

                else
                {
                    return new Tuple<Users, IList<Claim>>(null, null);
                }
            }

            return new Tuple<Users, IList<Claim>>(null, null);

        }

        public async Task<Users> GetUserByUserName(string userName)
        {
            return await userManager.FindByNameAsync(userName);
        }

        public async Task<bool> RevokeRefreshToken(string refreshToken)
        {
            var result = await GetUserByRefreshToken(refreshToken);

            if (result.Item1 == null) return false;

            IdentityResult response = await userManager.RemoveClaimsAsync(result.Item1, result.Item2);

            if (response.Succeeded) return true;

            return false;
        }

        public async Task<BaseResponse<UserViewModelResource>> UpdateUser(UserViewModelResource userViewModel, string userName)
        {
            Users user = await userManager.FindByNameAsync(userName);
            if (userManager.Users.Count(c => c.PhoneNumber == userViewModel.PhoneNumber)>1)
            {
                return new BaseResponse<UserViewModelResource>("Bu telefon numarası başka bri üyeye ait.");
            }

            user.Name = userViewModel.Name;
            user.Surname = userViewModel.Surname;
            user.Email = userViewModel.Email;
            user.PhoneNumber = userViewModel.PhoneNumber;
            user.Photo = userViewModel.Photo;
            user.Address = userViewModel.Address;
            user.Gender = userViewModel.Gender;

            IdentityResult result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return new BaseResponse<UserViewModelResource>(user.Adapt<UserViewModelResource>());
            }

            else
            {
                return new BaseResponse<UserViewModelResource>(result.Errors.First().Description);
            }
        }

        public async Task<BaseResponse<Users>> UploadUserPicture(string picturePath, string userName)
        {
            Users user = await userManager.FindByNameAsync(userName);
            user.Photo = picturePath;
            IdentityResult result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return new BaseResponse<Users>(user);
            }

            else
            {
                return new BaseResponse<Users>(result.Errors.First().Description); 
            }


        }
    }
}
