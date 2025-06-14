using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDataAccess.Entity
{
    [Table("Автомобиль")]
    public class Cars
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Марка")]
        public string Марка { get; set; } = String.Empty;
        [Column("Модель")]
        public string Модель { get; set; } = String.Empty;
        [Column("Год выпуска")]
        public int Год_Выпуска { get; set; }
        [Column("Client_id")]
        public int Client_id { get; set; }
        [Column("Malfunction_id")]
        public int Malfunction_id {  get; set; }
        [ForeignKey("Malfunction_id")]
        public Malfunction Malfunctionid { get; set; }

        [ForeignKey("Client_id")]
        public Client Clientid { get; set; }
    }
}
