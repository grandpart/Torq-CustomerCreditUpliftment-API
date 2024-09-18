using gmTemporaryCustomerCreditLimit.DataTransferObject;
using gmTemporaryCustomerCreditLimit.Model;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace gmTemporaryCustomerCreditLimit.Data
{
    public class AttachmentPathDetailsData
    {
        #region Read Attachment Details
        public static AttachmentPathDetails ReturnAttachmentTable(string connString, string referenceNo)
        {
            AttachmentPathDetails pathDetails = new();


          

            using (SqlConnection connection = new(connString))
            {
                try
                {

                    SqlCommand command = new("[Drive].[dbo].GI_GetFolderPathByFolderIDAndTableName", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@FolderId", referenceNo);
                    
                    connection.Open();

                    DataTable dtPathDetails = new();
                    SqlDataAdapter aPathDetails = new(command);
                    aPathDetails.Fill(dtPathDetails);

                    if (dtPathDetails.Rows.Count > 0)
                    {
                        pathDetails.FolderPath = dtPathDetails.Rows[0]["FolderPath"].ToString() ?? string.Empty;
                        pathDetails.ProcessName = dtPathDetails.Rows[0]["ProcessName"].ToString() ?? string.Empty;
                        pathDetails.FolderName = dtPathDetails.Rows[0]["FolderName"].ToString() ?? string.Empty;
                        pathDetails.FolderId = referenceNo;
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
            return pathDetails;
        }
        #endregion
        #region Add Attachment
        public static bool InsertAttachment(string connString, AttachmentPathDTO attachmentPathDTO)
        {
          


            using (SqlConnection connection = new(connString))
            {
                try
                {

                    SqlCommand command = new("[Drive].[dbo].GI_insertAttachments", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@FolderId", attachmentPathDTO.FolderId);
                    command.Parameters.AddWithValue("@FileLocation", attachmentPathDTO.FilePath);
                    command.Parameters.AddWithValue("@FileName", attachmentPathDTO.FileName);
                    command.Parameters.AddWithValue("@ProcessName", attachmentPathDTO.ProcessName);
                    command.Parameters.AddWithValue("@FolderName", attachmentPathDTO.FolderName);

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
