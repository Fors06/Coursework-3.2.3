using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestDataAccess.Entity
{
    [Table("Клиент")]
    public class Client
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
        [Column("Users_id")]
        public int? Users_id { get; set; }

        [ForeignKey("Users_id")] public virtual Users Users { get; set; }


    }
}
