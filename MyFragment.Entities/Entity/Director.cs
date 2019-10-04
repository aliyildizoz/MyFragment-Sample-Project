using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFragment.Entities.Entity
{
    [Table("Director")]
    public class Director : EntityBase
    {
        public Director()
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
