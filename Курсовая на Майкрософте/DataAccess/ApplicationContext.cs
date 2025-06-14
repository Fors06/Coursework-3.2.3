using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ApplicationContext : DbContext
    {


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<Car_service_center> car_Service_Centers { get; set; }
        public DbSet<Cars> cars { get; set; }
        public DbSet<Client> clients { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Loyalty_Card> loyalty_Cards { get; set; }
        public DbSet<Loyalty_Program> loyalty_Programs { get; set; }
        public DbSet<Malfunction> malfunctions { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Services> serviceses { get; set; }
        public DbSet<Type_User> type_Users { get; set; }
        public DbSet<Users> users { get; set; }
        public DbSet<Access_level> access_Levels { get; set; }
        public DbSet<Card_status> card_Statuses { get; set; }
        public DbSet<Order_status> order_Statuses { get; set; }
        public DbSet<Card_Condition> card_Conditions { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Автомастерская;Trusted_Connection=True;");
        //}
    }
}
