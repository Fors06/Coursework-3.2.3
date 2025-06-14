using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    [Table("Статус карты")]
    public class Card_status
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Статус")]
        public string Статус { get; set; } = string.Empty;
    }
}
