using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFragment.Entities.ViewModel
{
    public class LoginViewModel
    {
        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(25, ErrorMessage = "{0} max. {1} karekter olmalıdır")]
        public string Username { get; set; }

        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez"), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
