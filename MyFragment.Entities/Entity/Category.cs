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
    [Table("Category")]
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Kategori türü"),
          Required(ErrorMessage = "'{0}' alanı boş geçilemez.")]
        public CategoryState CategoryState { get; set; }

        [DisplayName("Değer"),
          Required(ErrorMessage = "'{0}' alanı boş geçilemez."),
          StringLength(100, ErrorMessage = "'{0}' max. {1} karekter olmalıdır.")]
        public string Value { get; set; }

        public Category()
        {
            Movies = new List<Movie>();
        }
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
