using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace RequestDataAccess.Entity
{
    [Table("Статус заказа")]
    public class Order_status
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Статус")]
        public string Статус_Заказа { get; set; } = string.Empty;
    }
    public enum OrderStatus
    {
        Принят = 1,
        Выполняется = 2,
        Отменён = 3,
        Готов = 4
    }
}
