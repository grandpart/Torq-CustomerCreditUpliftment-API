using gmTemporaryCustomerCreditLimit.DataTransferObject;
using gmTemporaryCustomerCreditLimit.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace gmTemporaryCustomerCreditLimit.Data
{
    public class CreditUpliftmentData
    {
        #region Save
        public static bool InsertCreditUpliftment(string connString, TempCreditLimitUpliftmentDTO tempDetails,string referenceNo)
        {

            StringBuilder sb = new();
            
            sb.AppendLine("Insert into CustomerCreditUpliftment ");
            sb.AppendLine("SELECT @User,@Reference,getdate() ,@Customer,@Amount,@Motivation,@FileName");
          
            using (SqlConnection connection = new(connString))
            {
                try
                {

                    SqlCommand command = new(sb.ToString(), connection);
                    command.CommandType = CommandType.Text;

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@User", tempDetails.UserKey);
                    command.Parameters.AddWithValue("@Reference", referenceNo);
                    command.Parameters.AddWithValue("@FileName", tempDetails.AttachmentFileName);
                    command.Parameters.AddWithValue("@Customer", tempDetails.CustomerAccount);
                    command.Parameters.AddWithValue("@Amount", tempDetails.UpliftmentValue);
                    command.Parameters.AddWithValue("@Motivation", tempDetails.Details);

                    connection.Open();

                    command.ExecuteNonQuery();


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
            return true;
        }
        #endregion
    }
}
