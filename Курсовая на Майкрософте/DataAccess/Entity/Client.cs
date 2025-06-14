using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    [Table("Клиент")]
    public class Client
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Фамилия")]
        public string Фамилия { get; set; } = String.Empty;
        [Column("Имя")]
        public string Имя { get; set; } = String.Empty;
        [Column("Отчество")]
        public string Отчество { get; set; } = String.Empty;
        [Column("Телефон")]
        public string Телефон { get; set; } = String.Empty;
        [Column("Program_id")]
        public int Program_id { get; set; }
        [Column("Card_id")]
        public int Card_id { get; set; }

        [Column("Users_id")]
        public int Users_id { get; set; }


        public virtual Users Usersid { get; set; }
        public virtual Loyalty_Card Cardid { get; set; }
        public virtual Loyalty_Program Loyalty_Programid { get; set; }
    }
}
