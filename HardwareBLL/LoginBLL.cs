using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareBLL
{
    public class LoginBLL
    {
        public bool KiemTraDangNhap(string user, string pass)
        {
          
            if (user == "admin" && pass == "123")
            {
                return true;
            }
            return false;
        }
    }
}
