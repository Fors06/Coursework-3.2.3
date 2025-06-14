using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    [Table("Тип пользователя")]
    public class Type_User
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Роль")]
        public string Роль { get; set; } = String.Empty;
        [Column("Access_level_id")]
        public int Access_level_id { get; set; }

        public virtual Access_level Access_Level { get; set; }
   

    }
}
