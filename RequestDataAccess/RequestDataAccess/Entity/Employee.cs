using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RequestDataAccess.Entity
{
    [Table("Сотрудник")]
    public class Employee
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Фамилия")]
        public string Фамилия { get; set; } = String.Empty;
        [Column("Имя")]
        public string Имя { get; set; } = String.Empty;
        [Column("Отчество")]
        public string Отчество { get; set; } = String.Empty;
        [Column("Телефон")]
        public string Телефон { get; set; } = String.Empty;
        [Column("Должность")]
        public string Должность { get; set; } = String.Empty;
        [Column("Состояние")]
        public string Состояние {  get; set; } = String.Empty;
        [Column("Auto_Service_id")]
        public int Auto_Service_id { get; set; }
        [Column("Users_id")]
        public int Users_id { get; set; }

        [ForeignKey("Users_id")] 
        public virtual Users Users { get; set; }

        [ForeignKey("Auto_Service_id")] public virtual Car_service_center AutoServise { get; set; }

        public string ФИО => $"{Фамилия} {Имя} {Отчество}";
    }
}
