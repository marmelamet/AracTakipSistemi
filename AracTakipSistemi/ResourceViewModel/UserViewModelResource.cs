using AracTakipSistemi.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AracTakipSistemi.ResourceViewModel
{
    public class UserViewModelResource
    {
        [Required]
        public string UserName { get; set; }

        [Required (ErrorMessage = "İsim Alanı Zorunludur!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad Alanı Zorunludur!")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "E-posta Adresi Zorunludur")]
        [EmailAddress(ErrorMessage = "E-posta adresi doğru formatta değil!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı Zorunludur")]
        public string Password { get; set; }

        [RegularExpression(@"^(0(\d{3})(\d{3})(\d{2})(\d{2}))$",ErrorMessage ="Telefon numarası formata uygun değil!")]
        public string PhoneNumber { get; set; }

        public string Photo { get; set; }

        [Required(ErrorMessage = "Adres bilgisi zorunludur!")]
        public string Address { get; set; }

        public Gender Gender { get; set; }

        public int AuthoID { get; set; }

    }
}
