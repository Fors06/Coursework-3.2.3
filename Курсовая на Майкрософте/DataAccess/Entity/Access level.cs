using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    [Table("Уровень доступа")]
    public class Access_level
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Уровень доступа")]
        public string Уровень_Доступа { get; set; } = string.Empty;
    }
}
