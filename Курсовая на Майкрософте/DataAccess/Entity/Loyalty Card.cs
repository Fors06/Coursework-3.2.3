using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    [Table("Карта лояльности")]
    public class Loyalty_Card
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Баллы")]
        public int Баллы { get; set; }
        [Column("Card_Status_id")]
        public int Card_Status_id { get; set; }
        [Column("Card_Action_id")]
        public int Card_Action_id { get; set; } 
        [Column("Телефон")]
        public string Телефон { get; set; } = String.Empty;
        [Column("Клиент_id")]
        public int Клиент_id { get; set; } 
        [Column("Программа_id")]
        public int Программа_id { get; set; }
    }
}
