using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    [Table("Неисправность")]
    public class Malfunction
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Описание")]
        public string Описание { get; set; } = String.Empty;
        [Column("Cars_id")]
        public int Cars_id { get; set; }
    }
}
