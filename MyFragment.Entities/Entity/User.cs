using MyFragment.Entities.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFragment.Entities.Entity
{
    [Table("User")]
    public class User : EntityBase
    {
        [DisplayName("Kullanıcı Adı"),
            Required(ErrorMessage = "'{0}' alanı boş geçilemez."),
            StringLength(25, ErrorMessage = "'{0}' max. {1} karekter olmalıdır.")]
        public string Username { get; set; }

        [DisplayName("Şifre"),
           Required(ErrorMessage = "'{0}' alanı boş geçilemez."),
           StringLength(25, ErrorMessage = "'{0}' max. {1} karekter olmalıdır.")]
        public string Password { get; set; }

        [DisplayName("E-posta"),
            Required(ErrorMessage = "'{0} adresi' alanı boş geçilemez."),
            EmailAddress(ErrorMessage = "'{0}' alanı için lütfen geçerli bir e-posta adresi giriniz."),
            StringLength(50, ErrorMessage = "'{0}' max. '{1}' karekter olmalıdır.")]
        public string Email { get; set; }

        [DisplayName("Kullanıcı tipi"),
         Required(ErrorMessage = "'{0}' alanı boş geçilemez.")]
        public UserState? UserState { get; set; }
        public User()
        {
            Movies = new List<Movie>();
        }
        public override string ToString()
        {
            return $"{Name} {Surname}";
        }
        public virtual ICollection<Movie> Movies { get; set; }

    }
}
