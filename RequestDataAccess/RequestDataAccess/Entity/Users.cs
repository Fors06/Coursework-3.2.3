using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace RequestDataAccess.Entity
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
        [Column("Отчество")]
        public string Отчество {  get; set; } = String.Empty;
        [Column("Email")]
        public string Email { get; set; } = String.Empty;
        [Column("Пароль")]
        public string Пароль { get; set; } = String.Empty;
        [Column("Телефон")]
        public string Телефон { get; set; } = String.Empty;
        [Column("User_Type_id")]
        public int User_Type_id { get; set; }

        [ForeignKey("User_Type_id")]
        public virtual Type_User User_Typeid { get; set; }

        // Навигационное свойство для сотрудников
        public ICollection<Employee> Employees { get; set; }

        // Навигационное свойство для клиентов
        public ICollection<Client> Clients { get; set; }


    }
}
