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
    public class EntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Isim"),
            Required(ErrorMessage = "'{0}' alanı boş geçilemez."),
            StringLength(40, ErrorMessage = "'{0}' max. {1} karekter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Soyisim"),
            Required(ErrorMessage = "'{0}' alanı boş geçilemez."),
            StringLength(50, ErrorMessage = "'{0}' max. {1} karekter olmalıdır.")]
        public string Surname { get; set; }

        [DisplayName("Resim"),
            StringLength(40, ErrorMessage = "'{0}' max. {1} karekter olmalıdır.")]
        public string ImagePath { get; set; }
    }
}
