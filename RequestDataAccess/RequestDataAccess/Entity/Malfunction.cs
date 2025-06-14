using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDataAccess.Entity
{
    [Table("Неисправность")]
    public class Malfunction
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Описание")]
        public string Описание { get; set; }
    }
}
