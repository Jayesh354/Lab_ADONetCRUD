using DAL.Entities;
using System.Data.SqlClient;

namespace DAL.Repositories
{
    public class ProductRepository
    {
        private SqlConnection conn;

        public string SQLConnectionStr { get; set; }
        private void Connection()
        {
            conn = new SqlConnection(SQLConnectionStr);
        }
        public ProductRepository(string connectionStr)
        {
            SQLConnectionStr=connectionStr;
        }

        public bool AddProduct(Product model)
        {
            bool result = false;
            var commandText = "AddNewProductDetails";
            Connection();
            conn.Open();
            using(SqlCommand cmd = new SqlCommand(commandText,conn)) 
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Description", model.Description);
                cmd.Parameters.AddWithValue("@UnitPrice", model.UnitPrice);
                cmd.Parameters.AddWithValue("@CategoryId", model.CategoryId);
                result = cmd.ExecuteNonQuery()>=1;
                
            }
            conn.Close();
            return result;
        }

        public bool UpdateProduct(Product model)
        {
            bool result = false;
            var commandText = "UpdateProductDetails";
            Connection();
            conn.Open();
            using (SqlCommand cmd=new SqlCommand(commandText,conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductId", model.ProductId);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Description", model.Description);
                cmd.Parameters.AddWithValue("@UnitPrice",model.UnitPrice);
                cmd.Parameters.AddWithValue("@CategoryId", model.CategoryId);
                result= cmd.ExecuteNonQuery()>=1;
            }
            conn.Close();
            return result;
        }

        public bool DeleteProduct(int Id) {
           
            bool result = false;
            var commandText = "DeleteProductById";
            Connection();
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@ProductId", Id);
                result= cmd.ExecuteNonQuery()>=1;   
            }
            conn.Close();
            return result;
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();   
            Connection();
            var commandText = "Select * from Products";
            conn.Open();

            using (SqlCommand cmd = new SqlCommand(commandText,conn)) 
            {
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader reader= cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product= new Product();

                    product.ProductId = Convert.ToInt32(reader["ProductId"]);
                    product.Name = reader["Name"].ToString();
                    product.Description = reader["Description"].ToString();
                    product.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]);
                    product.CategoryId = Convert.ToInt32(reader["CategoryId"]);

                    products.Add(product);
                }
            }
            conn.Close();
            return products;
        }

        public Product GetProduct(int id)
        {
            Product product= new Product();

            var commandText = "usp_getproduct";

            Connection();

            conn.Open();
            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType=System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductId", id);

                SqlDataReader reader= cmd.ExecuteReader();

                while(reader.Read())
                {
                    product.ProductId =  Convert.ToInt32(reader["ProductId"]);
                    product.Name = reader["Name"].ToString();
                    product.Description = reader["Description"].ToString();
                    product.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]);
                    product.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                    break;
                }
            }
            conn.Close();
            return product;
        }
    }
}
