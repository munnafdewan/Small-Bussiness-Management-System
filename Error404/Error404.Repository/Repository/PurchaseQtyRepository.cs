using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Error404.Model.Model;
using Error404.DatabaseContext.DatabaseContext;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;

namespace Error404.Repository.Repository
{
    public class PurchaseQtyRepository
    {
        string connectionString = @"Server = DESKTOP-IL4U8GL; Database = Error404;Integrated Security = true";

        double availableQuantity;
        public double GetAvailableProduct(int productId)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = "SELECT TOP 1  SUM(pd.Quantity) as AvailableQuantity FROM PurchaseDetails AS pd INNER JOIN Products AS p ON pd.ProductId =" + productId + " GROUP BY p.Name,p.Id";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable();
            int isFill = sqlDataAdapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {

                availableQuantity = double.Parse(dataTable.Rows[0][0].ToString());
            }
            return availableQuantity;
        }

        double saleQuantity;
        public double GetSaleAvailableProduct(int productId)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = "SELECT TOP 1 SUM(sd.Quantity) as AvailableQuantity FROM SaleDetails AS sd INNER JOIN Products AS p ON sd.ProductId = " + productId + " GROUP BY p.Name,p.Id";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable();
            int isFill = sqlDataAdapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {

                saleQuantity = double.Parse(dataTable.Rows[0][0].ToString());
            }
            return saleQuantity;
        }
    }
}
