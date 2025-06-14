using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    [Table("Автосервисная")]
    public class Car_service_center
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Название")]
        public string Название { get; set; } = String.Empty;
        [Column("Адрес")]
        public string Адрес { get; set; } = String.Empty;
        [Column("Телефон")]
        public string Телефон { get; set; } = String.Empty;
        [Column("Рейтинг")]
        public decimal Рейтинг { get; set; }
        [Column("Время работы")]
        public TimeSpan Время_Работы { get; set; }
       
    }
}
