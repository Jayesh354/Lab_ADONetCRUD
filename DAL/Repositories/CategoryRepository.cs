using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CategoryRepository
    {
        private SqlConnection conn;
        public string SQLConnectionStr { get; set; }

        private void Connection()
        {
            conn = new SqlConnection(SQLConnectionStr);
        }
        public CategoryRepository(string sqlConnectionStr)
        {
            SQLConnectionStr= sqlConnectionStr;

        }

        public List<Category> GetCategories()
        {
            Connection();
            List<Category> list = new List<Category>();
            
            conn.Open();
            var command = "Select * from Categories";
            using(SqlCommand cmd = new SqlCommand(command,conn))
            {
                SqlDataReader reader= cmd.ExecuteReader();
                while (reader.Read()) 
                {
                    
                    Category cate= new Category();
                    cate.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                    cate.Name = reader["Name"].ToString();

                    list.Add(cate); 

                }
            }
            conn.Close();
            return list;
        }


    }
}
