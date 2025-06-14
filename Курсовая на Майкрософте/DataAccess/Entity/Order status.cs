using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    [Table("Статус заказа")]
    public class Order_status
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Статус")]
        public string Статус_Заказа { get; set; }
    }
}
