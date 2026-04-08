using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hardware.DAL
{
    public class UserDAL
    {
       
        public string LayLaiMatKhau(string username, string fullName)
        {
            DataAccessor da = new DataAccessor();
            string sql = "SELECT Password FROM Users WHERE Username = @user AND FullName = @name";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@user", username),
                new SqlParameter("@name", fullName)
            };

            DataTable dt = da.GetDataTable(sql, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            return null;
        }
    }
}
