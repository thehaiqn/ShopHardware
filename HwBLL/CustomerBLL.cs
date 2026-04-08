using Hardware.DAL;
using Hardwawe.DTO;
using HardwaweDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hardware.BLL
{
    public class CustomerBLL
    {
        private CustomerDAL customerDAL = new CustomerDAL();

        public DataTable GetCustomers()
        {
            return customerDAL.GetAllCustomers();
        }

        public bool AddCustomer(CustomerDTO c)
        {
            if (string.IsNullOrEmpty(c.CName)) return false; 
            return customerDAL.Insert(c);
        }

        public bool UpdateCustomer(CustomerDTO c)
        {
            return customerDAL.Update(c);
        }

        public bool DeleteCustomer(int id)
        {
            return customerDAL.Delete(id);
        }
    }
}

