using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    [Table("Состояние карты")]
    public class Card_Condition
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Состояние")]
        public string Condition { get; set; } = string.Empty;
    }
}
