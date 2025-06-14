using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    [Table("Программа лояльности")]
    public class Loyalty_Program
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Название")]
        public string Название { get; set; } = String.Empty;
        [Column("Условия участия")]
        public string Условия_участия { get; set; } = String.Empty;
        [Column("Описание")]
        public string Описание { get; set; } = String.Empty;
        [Column("Скидка")]
        public decimal Скидка { get; set; }
        [Column("Card_id")]
        public int Card_id { get; set; }
    }
}
