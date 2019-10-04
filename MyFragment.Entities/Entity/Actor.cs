using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFragment.Entities.Entity
{
    [Table("Actor")]
    public class Actor : EntityBase
    {
        public Actor()
        {
            Movies = new List<Movie>();
        }
        public virtual ICollection<Movie> Movies { get; set; }
        public override string ToString()
        {
            return $"{Name} {Surname}";
        }
    }
}
