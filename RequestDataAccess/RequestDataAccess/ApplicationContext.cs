using Microsoft.EntityFrameworkCore;
using RequestDataAccess.Entity;

namespace RequestDataAccess
{
    public class ApplicationContext : DbContext
    {

        private static readonly DbContextOptions<ApplicationContext> _options;

        public DbSet<Car_service_center> car_Service_Centers { get; set; }
        public DbSet<Cars> cars { get; set; }
        public DbSet<Client> clients { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Malfunction> malfunctions { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Services> serviceses { get; set; }
        public DbSet<Type_User> type_Users { get; set; }
        public DbSet<Users> users { get; set; }
        public DbSet<Order_status> order_Statuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Автомастерская_2;Trusted_Connection=True;");
        }
    }


}
