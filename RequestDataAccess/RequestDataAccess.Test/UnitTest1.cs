using Microsoft.EntityFrameworkCore;
using RequestDataAccess.Entity;
using RequestDataAccess.Repository;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RequestDataAccess.Test
{
    public class UnitTest1
    {
        private const string ConnectionString = @"Server=(localdb)\MSSQLLocalDB;Database=Автомастерская;Trusted_Connection=True;";
        [Fact]
        public void Test1()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlServer(ConnectionString)
                .Options;
            ApplicationContext context = new ApplicationContext(options);
            ClientRepository EmpRep = new ClientRepository(context);
      
            EmpRep.GetStatusList();
         
        }
    }
 
}