using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RequestDataAccess.Entity;

namespace RequestDataAccess.Entity
{
    [Table("Тип пользователя")]
    public class Type_User
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Роль")]
        public string Роль { get; set; } = String.Empty;
   

    }
    public enum UserTypes
    {
        Клиент = 1,
        Администратор = 2,
        Сотрудник = 3
    }
}
