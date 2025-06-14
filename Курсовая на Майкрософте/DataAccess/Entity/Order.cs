using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    [Table("Заказ")]
    public class Order
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Стоимость")]
        public decimal Стоимость { get; set; }
        [Column("Cars_id")]
        public int Cars_id { get; set; }
        [Column("Services_id")]
        public int Services_id { get; set; }
        [Column("Clients_id")]
        public int Clients_id { get; set; }
        [Column("Master_id")]
        public int Master_id { get; set; }
        [Column("Auto_Service_id")]
        public int Auto_Service_id { get; set; }
        [Column("Statuses_id")]
        public int Statuses_id { get; set; }
    }
}
