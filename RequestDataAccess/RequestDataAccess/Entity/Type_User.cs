using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


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
