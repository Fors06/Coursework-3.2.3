using Microsoft.EntityFrameworkCore;
using RequestDataAccess;
using RequestDataAccess.Repository;
using System.ComponentModel.DataAnnotations.Schema;
namespace TestProject1
{
    public class UnitTest1
    {
     
        private const string ConnectionString = "Server=Berkut\\SQLEXPRESS;Database=Red October;Trusted_Connection=True;";
        [Fact]
        public void Test1()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlServer(ConnectionString)
                .Options;
            ApplicationContext context = new ApplicationContext(options);
            EmployeeRepository EmpRep = new EmployeeRepository(context);

            var spendings = EmpRep.GetStatusList();
        }
    }
}