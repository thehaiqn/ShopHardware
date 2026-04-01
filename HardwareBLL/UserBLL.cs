using Hardwawe.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareBLL
{
    public class UserBLL
    {
        UserDAL dal = new UserDAL();

        public string GetPassword(string user, string name)
        {
         
            return dal.LayLaiMatKhau(user, name);
        }
    }
}
