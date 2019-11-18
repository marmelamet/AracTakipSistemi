using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AracTakipSistemi.ResourceViewModel
{
    public class SignInViewModelResource
    {
        [Required(ErrorMessage =("E-mail alanı gereklidir."))]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6,ErrorMessage ="Şifreniz en az 6 karakter olmalı!")]
        public string Password { get; set; }
    }
}
