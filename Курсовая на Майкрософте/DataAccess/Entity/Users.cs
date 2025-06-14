using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    [Table("Пользователь")]
    public class Users
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Фамилия")]
        public string Фамилия { get; set; } = String.Empty;
        [Column("Имя")]
        public string Имя { get; set; } = String.Empty;
       
        [Column("Телефон")]
        public string Телефон { get; set; } = String.Empty;
        [Column("Пароль")]
        public string Пароль { get; set; } = String.Empty;
        [Column("Email")]
        public string Email { get; set; } = String.Empty;
        [Column("User_Type_id")]
        public int User_Type_id { get; set; } 
    }
}
