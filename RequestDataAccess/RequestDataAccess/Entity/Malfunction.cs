using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace RequestDataAccess.Entity
{
    [Table("Неисправность")]
    public class Malfunction
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Описание")]
        public string Описание { get; set; }
    }
}
