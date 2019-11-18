using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AracTakipSistemi.Domain.Responses;
using AracTakipSistemi.Domain.Services;
using AracTakipSistemi.ResourceViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AracTakipSistemi.Controllers
{
    [Produces("application/json")]
    [Route("api/views/Expeditions/Index")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService service)
        {
            this.authenticationService = service;
        }

        //localhost: 12345/api/Authentication/IsAuthentication gelmeli.
        [HttpGet]
        public ActionResult IsAuthentication()
        {
            return Ok(User.Identity.IsAuthenticated);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserViewModelResource userViewModel)
        {
            BaseResponse<UserViewModelResource> response = await this.authenticationService.SignUp(userViewModel);

            if (response.Success)
            {
                return Ok(response.Extra);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult>SignIn(SignInViewModelResource signInViewModel)
        {
            var response = await authenticationService.SignIn(signInViewModel);
            if (response.Success)
            {
                return Ok(response.Extra);
            }
            else
            {
                return BadRequest(response.Message);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccessTokenByRefreshToken(RefreshTokenViewModelResource refreshTokenView)
        {
            var response = await authenticationService.CreateAccessTokenByRefreshToken(refreshTokenView);

            if (response.Success)
            {
                return Ok(response.Extra);
            }

            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult>RevokeRefreshToken(RefreshTokenViewModelResource refreshTokenView)
        {
            var response = await authenticationService.RevokeRefreshToken(refreshTokenView);

            if (response.Success)
            {
                return Ok();
            }

            else
            {
                return BadRequest(response.Message);
            }
        }
    }
}