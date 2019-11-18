using AracTakipSistemi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AracTakipSistemi.Services
{
    public class BaseService:ControllerBase
    {
        protected UserManager <Users> userManager { get; } 
        protected SignInManager<Users> signInManager { get; }
        protected RoleManager<Users> roleManager { get; }

        public BaseService(UserManager<Users> userManager, SignInManager<Users> signInManager, RoleManager<Users> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
    }
}
