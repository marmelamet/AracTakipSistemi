using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AracTakipSistemi.Domain.Services;
using AracTakipSistemi.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AracTakipSistemi.ResourceViewModel;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AracTakipSistemi.Controllers
{
    [Produces("application/json")]
    [Route("api/User/action")]
    [Authorize]
    public class UserController : ControllerBase , IActionFilter
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetUser()
        {
            Users user = await userService.GetUserByUserName(User.Identity.Name);
            return Ok(user.Adapt<UserViewModelResource>());
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutedContext context)
        {
            context.ModelState.Remove("Password");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UserViewModelResource userViewModelResource)
        {
            var response = await userService.UpdateUser(userViewModelResource, User.Identity.Name);

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
        public async Task<ActionResult> UploadUserPicture(IFormFile picture)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(picture.FileName);

            var path = Path.Combine(Directory.GetCurrentDirectory() + "wwwroot/UserPictures", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await picture.CopyToAsync(stream);
            }
            var result = new
            {
                path = "https://" + Request.Host + "/UserPictures" + fileName
            };
            var response = await userService.UploadUserPicture(result.path, User.Identity.Name);

            if (response.Success)
            {
                return Ok(path);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
    }
}