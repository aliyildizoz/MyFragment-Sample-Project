using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFragment.Entities.ViewModel
{
    public class RegisterViewModel
    {
        [DisplayName("Kullanıcı Adı"),
            Required(ErrorMessage = "'{0}' alanı boş geçilemez."),
            StringLength(25, ErrorMessage = "'{0}' max. {1} karekter olmalıdır.")]
        public string Username { get; set; }

        [DisplayName("Isim"),
            Required(ErrorMessage = "'{0}' alanı boş geçilemez."),
            StringLength(25, ErrorMessage = "'{0}' max. {1} karekter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Soyisim"),
            Required(ErrorMessage = "'{0}' alanı boş geçilemez."),
            StringLength(25, ErrorMessage = "'{0}' max. {1} karekter olmalıdır.")]
        public string Surname { get; set; }

        [DisplayName("E-posta"),
            Required(ErrorMessage = "'{0} adresi' alanı boş geçilemez."),
            EmailAddress(ErrorMessage = "'{0}' alanı için lütfen geçerli bir e-posta adresi giriniz."),
            StringLength(25, ErrorMessage = "'{0}' max. '{1}' karekter olmalıdır.")]
        public string Email { get; set; }

        [DisplayName("Şifre"),
            Required(ErrorMessage = "'{0}' alanı boş geçilemez."),
            DataType(DataType.Password),
            StringLength(25, ErrorMessage = "'{0}' max. {1} karekter olmalıdır.")]
        public string Password { get; set; }

        [DisplayName("Şifre Tekrar"),
            Required(ErrorMessage = "'{0}' alanı boş geçilemez."),
            DataType(DataType.Password),
            StringLength(25, ErrorMessage = "{0} max. {1} karekter olmalıdır."),
            Compare("Password", ErrorMessage = "'{1}' ve '{0}' alanı uyuşmuyor.")]
        public string RePassword { get; set; }

    }
}
