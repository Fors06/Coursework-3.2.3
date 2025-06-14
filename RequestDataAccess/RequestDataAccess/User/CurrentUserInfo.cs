using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDataAccess.User
{
    public class CurrentUserInfo
    {
        public static UserProfile LoggedInUser { get; set; }
    }
}
