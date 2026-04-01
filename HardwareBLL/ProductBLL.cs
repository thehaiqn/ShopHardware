using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hardwawe.DTO;
using Hardwawe.DAL;

namespace HardwareBLL
{
    public class ProductBLL
    {
        private ProductDAL productDAL = new ProductDAL();

        public DataTable GetProducts()
        {
            return productDAL.GetAllProducts();
        }

        public bool AddProduct(ProductDTO product)
        {
            
            if (string.IsNullOrEmpty(product.ProductName) || product.Price <= 0)
                return false;

            return productDAL.Insert(product);
        }

        public bool UpdateProduct(ProductDTO product)
        {
            return productDAL.Update(product);
        }

        public bool DeleteProduct(int productId)
        {
            return productDAL.Delete(productId);
        }
    }
}
