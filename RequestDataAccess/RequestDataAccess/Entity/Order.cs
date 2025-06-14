using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDataAccess.Entity
{
    [Table("Заказ")]
    public class Order
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Дата_начала")]
        public DateTime Дата_Начала { get; set; }
        [Column("Дата_окончания")]
        public DateTime Дата_Окончания {  get; set; }
        [Column("Стоимость")]
        public decimal Стоимость { get; set; }
        [Column("Cars_id")]
        public int Cars_id { get; set; }
        [Column("Clients_id")]
        public int Clients_id { get; set; }
        [Column("Master_id")]
        public int Master_id { get; set; }
        [Column("Auto_Service_id")]
        public int Auto_Service_id { get; set; }
        [Column("Statuses_id")]
        public int Statuses_id { get; set; }
        [Column("Services_id")]
        public int Services_id { get; set; }

        [ForeignKey("Cars_id")]
        public Cars Carsid { get; set; }
        [ForeignKey("Clients_id")]
        public Client Clientsid { get; set; }
        [ForeignKey("Master_id")]
        public Employee Masterid { get; set; }
        [ForeignKey("Auto_Service_id")]
        public Car_service_center Auto_Serviceid { get; set; }
        [ForeignKey("Services_id")]
        public Services Servicesid { get; set; }
        [ForeignKey("Statuses_id")]
        public Order_status Orderstatusid { get; set; }

    }
}
