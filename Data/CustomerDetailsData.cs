using System.Data;
using System.Text;
using gmTemporaryCustomerCreditLimit.Model;
using Microsoft.Data.SqlClient;


namespace gmTemporaryCustomerCreditLimit.Data
{
    public class CustomerDetailsData
    {
        #region Read
        public static CustomerDetails GetCustomerDetailsByAccountNo(string connString, string customerAccount)
        {
            StringBuilder sb = new StringBuilder();
            CustomerDetails customer = new ();
            
            sb.AppendLine("SELECT C.Customer,C.CreditLimit,Name,");
            sb.AppendLine("C.CreditLimit - ISNULL(O.OutstOrdVal,0) - ISNULL(CB.CurrentBalance1,0) AS 'RemainingCredit',");
            sb.AppendLine("IIF (C.CustomerOnHold ='Y','Yes','No') [Status]");
            sb.AppendLine("FROM dbo.ArCustomer C WITH(NOLOCK)");
            sb.AppendLine("WHERE C.Customer = @CustID");

            using (SqlConnection connection = new(connString))
            {
                try
                {

                    SqlCommand command = new(sb.ToString(), connection);
                    command.CommandType = CommandType.Text;

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@CustID", customerAccount);
                    
                    connection.Open();

                    DataTable dtCustomer = new ();
                    SqlDataAdapter aCustomer = new (command);
                    aCustomer.Fill(dtCustomer);

                    customer.Customer = dtCustomer.Rows[0]["Customer"].ToString() ?? string.Empty;
                    customer.AvailableCredit = Convert.ToDouble(dtCustomer.Rows[0]["AvailableCredit"].ToString());
                    customer.CreditLimit= Convert.ToDouble(dtCustomer.Rows[0]["CreditLimit"].ToString());
                    customer.CustomerName = dtCustomer.Rows[0]["Name"].ToString() ?? string.Empty;


                }

                
                catch (SqlException ex)
                {
                   
                    throw new Exception(ex.Message);

                }
                finally
                {
                    connection.Close();
                }
            };
            return customer;
        }
        public static CustomerDetails GetAllActiveCustomerDetails(string connString)
        {
            StringBuilder sb = new StringBuilder();
            CustomerDetails customer = new();

            sb.AppendLine("SELECT C.Customer,C.CreditLimit,Name,");
            sb.AppendLine("C.CreditLimit - ISNULL(O.OutstOrdVal,0) - ISNULL(CB.CurrentBalance1,0) AS 'RemainingCredit',");
            sb.AppendLine("IIF (C.CustomerOnHold ='Y','Yes','No') [Status]");
            sb.AppendLine("FROM dbo.ArCustomer C WITH(NOLOCK)");
            sb.AppendLine("WHERE CustomerOnHold= 'N' ");

            using (SqlConnection connection = new(connString))
            {
                try
                {

                    SqlCommand command = new(sb.ToString(), connection);
                    command.CommandType = CommandType.Text;

                    connection.Open();

                    DataTable dtCustomer = new();
                    SqlDataAdapter aCustomer = new(command);
                    aCustomer.Fill(dtCustomer);

                    customer.Customer = dtCustomer.Rows[0]["Customer"].ToString() ?? string.Empty;
                    customer.AvailableCredit = Convert.ToDouble(dtCustomer.Rows[0]["AvailableCredit"].ToString());
                    customer.CreditLimit = Convert.ToDouble(dtCustomer.Rows[0]["CreditLimit"].ToString());
                    customer.CustomerName= dtCustomer.Rows[0]["Name"].ToString() ?? string.Empty;


                }


                catch (SqlException ex)
                {

                    throw new Exception(ex.Message);

                }
                finally
                {
                    connection.Close();
                }
            };
            return customer;
        }
        #endregion
    }
}
