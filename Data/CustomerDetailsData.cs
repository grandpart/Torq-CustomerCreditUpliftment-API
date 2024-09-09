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
            StringBuilder sb = new ();
            CustomerDetails customer = new ();
            
            sb.AppendLine("SELECT C.Customer,C.CreditLimit,Name,");
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
                    //customer.AvailableCredit = Convert.ToDouble(dtCustomer.Rows[0]["AvailableCredit"].ToString());
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
        public static List<CustomerDetails> GetAllActiveCustomerDetails(string connString)
        {
            StringBuilder sb = new ();
            List<CustomerDetails> customers = new();

            sb.AppendLine("SELECT C.Customer,C.CreditLimit,Name,");
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

                    foreach (DataRow row in dtCustomer.Rows)
                    {
                        customers.Add(new CustomerDetails
                        {
                            CreditLimit = Convert.ToDouble(row["CreditLimit"].ToString()),
                            Customer = row["Customer"].ToString() ?? string.Empty,
                            CustomerName = row["Name"].ToString() ?? string.Empty

                        });


                    }
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
            return customers;
        }
        public static List<CustomerDetails> GetAllActiveCustomerDetailsPerBranch(string connString, string branch)
        {
            StringBuilder sb = new();
            List<CustomerDetails> customers = new();

            sb.AppendLine("SELECT C.Customer,C.CreditLimit,Name,");
            sb.AppendLine("IIF (C.CustomerOnHold ='Y','Yes','No') [Status]");
            sb.AppendLine("FROM dbo.ArCustomer C WITH(NOLOCK)");
            sb.AppendLine("WHERE CustomerOnHold= 'N' ");
            sb.AppendLine("AND Branch like @branch ");
          

            using (SqlConnection connection = new(connString))
            {
                try
                {

                    SqlCommand command = new(sb.ToString(), connection);
                    command.CommandType = CommandType.Text;

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@branch", branch);

                    connection.Open();

                    DataTable dtCustomer = new();
                    SqlDataAdapter aCustomer = new(command);
                    aCustomer.Fill(dtCustomer);

                    foreach (DataRow row in dtCustomer.Rows)
                    {
                        customers.Add(new CustomerDetails
                        {
                            CreditLimit = Convert.ToDouble(row["CreditLimit"].ToString()),
                            Customer = row["Customer"].ToString() ?? string.Empty,
                            CustomerName = row["Name"].ToString() ?? string.Empty

                        });


                    }


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
            return customers;
        }
        public static List<CustomerDetails> SearchAllActiveCustomerDetails(string connString, string search)
        {
            StringBuilder sb = new();
            List<CustomerDetails> customers = new();

            sb.AppendLine("SELECT C.Customer,C.CreditLimit,Name,");
            sb.AppendLine("IIF (C.CustomerOnHold ='Y','Yes','No') [Status]");
            sb.AppendLine("FROM dbo.ArCustomer C WITH(NOLOCK)");
            sb.AppendLine("WHERE CustomerOnHold= 'N' ");
            sb.AppendLine("AND (Customer like '%" + search + "%' or Name like '% "+ search + "%') ");


            using (SqlConnection connection = new(connString))
            {
                try
                {

                    SqlCommand command = new(sb.ToString(), connection);
                    command.CommandType = CommandType.Text;
                   
                    
                    connection.Open();

                    DataTable dtCustomer = new();
                    SqlDataAdapter daCustomer = new(command);
                    daCustomer.Fill(dtCustomer);

                    foreach (DataRow row in dtCustomer.Rows)
                    {
                        customers.Add(new CustomerDetails
                        {
                            CreditLimit = Convert.ToDouble(row["CreditLimit"].ToString()),
                            Customer = row["Customer"].ToString() ?? string.Empty,
                            CustomerName = row["Name"].ToString() ?? string.Empty

                        });


                    }

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
            return customers;
        }
        #endregion
    }
}
