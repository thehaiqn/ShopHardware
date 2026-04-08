using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hardware.DAL
{
    public class LoginDAL
    {
        private DataAccessor db = new DataAccessor();

        public DataTable getLogin(string user, string pass)
        {
            string sql = "SELECT * FROM Users WHERE Username = @user AND Password = @pass";
            SqlParameter[] pars = {
                new SqlParameter("@user", user),
                new SqlParameter("@pass", pass)
            };
            return db.GetDataTable(sql, pars);
        }

  
        public bool InsertAccount(string user, string pass)
        {
            string sql = "INSERT INTO Users (Username, Password) VALUES (@user, @pass)";
            SqlParameter[] pars = {
                new SqlParameter("@user", user),
                new SqlParameter("@pass", pass)
            };
            return db.ExecuteNonQuery(sql, pars);
        }

 
        public bool IsUsernameExists(string user)
        {
            string sql = "SELECT COUNT(*) FROM Users WHERE Username = @user";
            SqlParameter[] pars = { new SqlParameter("@user", user) };
            DataTable dt = db.GetDataTable(sql, pars);
            return int.Parse(dt.Rows[0][0].ToString()) > 0;
        }
    }
}
