using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using Microsoft.Data.SqlClient;
using Hardware.DAL;

namespace Hardware.BLL
{
    public class LoginBLL
    {
        LoginDAL loginDAL = new LoginDAL();
        public bool KiemTraDangNhap(string user, string pass)
        {
            DataAccessor da = new DataAccessor();


            string sql = "SELECT COUNT(*) FROM Users WHERE Username = @user AND Password = @pass";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@user", user),
        new SqlParameter("@pass", pass)
            };


            DataTable dt = da.GetDataTable(sql, parameters);


            if (dt != null && dt.Rows.Count > 0)
            {

                int count = Convert.ToInt32(dt.Rows[0][0]);
                return count > 0;
            }

            return false;
        }
        public DataTable getLogin(string user, string pass)
        {
            return loginDAL.getLogin(user, pass);
        }

        public string RegisterAccount(string user, string pass, string confirmPass)
        {
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
                return "Vui lòng nhập đầy đủ!";

            if (pass != confirmPass)
                return "Mật khẩu không khớp!";


            if (loginDAL.IsUsernameExists(user))
                return "Tài khoản đã tồn tại!";

            if (loginDAL.InsertAccount(user, pass))
                return "Đăng ký thành công!";

            return "Đăng ký thất bại!";
        }
    }
}

