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
    [Table("Movie")]
    public class Movie
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Isim"),
           Required(ErrorMessage = "'{0}' alanı boş geçilemez."),
           StringLength(60, ErrorMessage = "'{0}' max. {1} karekter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Yapım yılı"),
           Required(ErrorMessage = "'{0}' alanı boş geçilemez.")]
        public int MovieYear { get; set; }

        [DisplayName("Eklenme Tarihi")]
        public DateTime AddedDate { get; set; }

        [DisplayName("Imdb puanı"),
           Required(ErrorMessage = "'{0}' alanı boş geçilemez.")]
        public string ImdbPoint { get; set; }

        [DisplayName("Süre"),
           Required(ErrorMessage = "'{0}' alanı boş geçilemez.")]
        public TimeSpan Lenght { get; set; }

        [DisplayName("Resim"),
           StringLength(50, ErrorMessage = "'{0}' max. {1} karekter olmalıdır.")]
        public string ImagePath { get; set; }

        [DisplayName("YouTube embed kodu"),
           Required(ErrorMessage = "'{0}' alanı boş geçilemez."),
           StringLength(150, ErrorMessage = "'{0}' max. {1} karekter olmalıdır.")]
        public string EmbedKey { get; set; }

        [DisplayName("Özet"),
           Required(ErrorMessage = "'{0}' alanı boş geçilemez."),
           StringLength(1500, ErrorMessage = "'{0}' max. {1} karekter olmalıdır.")]
        public string Summary { get; set; }

        public Movie()
        {
            Actors = new List<Actor>();
            Directors = new List<Director>();
            Categories = new List<Category>();
            Users = new List<User>();
        }
        public virtual ICollection<Actor> Actors { get; set; }
        public virtual ICollection<Director> Directors { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
