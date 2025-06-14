using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    [Table("Услуги")]
    public class Services
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Наименование")]
        public string Наименование { get; set; } = String.Empty;
        [Column("Описание")]
        public string Описание { get; set; } = String.Empty;
        [Column("Стоимость")]
        public decimal Стоимость { get; set; }
        [Column("Auto_Service_id")]
        public int Auto_Service_id { get; set; }

    }
}
