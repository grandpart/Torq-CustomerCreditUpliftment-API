using gmTemporaryCustomerCreditLimit.DataTransferObject;
using gmTemporaryCustomerCreditLimit.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Principal;
using System.Text;
using static gmTemporaryCustomerCreditLimit.Model.DriveCallDetails;

namespace gmTemporaryCustomerCreditLimit.Data
{
    public class DriveCallDetailsData
    {
        #region Read

        public static WSCRaiseFlag GetFlagDetails(TempCreditLimitUpliftmentDTO tempCreditLimitUpliftment)
        {
            try
            {


                WSCRaiseFlag FlagData = new WSCRaiseFlag();

                FlagData.RaisedByApplication = "Torq";
                FlagData.FolderId = string.Empty;
                FlagData.DataModelId = tempCreditLimitUpliftment.DataModelId;
                FlagData.CallerId = tempCreditLimitUpliftment.Requester;
                FlagData.FlagToRaise = "flg_TCI_Raise_Web";
                
                FlagData.FlagData.Add("ServiceShortName", "Customer Applications");
                FlagData.FlagData.Add("ServiceFolderId", "0900000000000000000000000002015");

                FlagData.FlagData.Add("AttachmentFileName", tempCreditLimitUpliftment.AttachmentFileName);
                FlagData.FlagData.Add("AttGUID", tempCreditLimitUpliftment.AttachmentGUID);

                FlagData.FlagData.Add("AccountNumber", tempCreditLimitUpliftment.CustomerAccount);
                FlagData.FlagData.Add("JobTitle", tempCreditLimitUpliftment.JobTitle);
                FlagData.FlagData.Add("Firstname", tempCreditLimitUpliftment.FirstName);
                FlagData.FlagData.Add("Lastname", tempCreditLimitUpliftment.LastName);
                FlagData.FlagData.Add("Email", tempCreditLimitUpliftment.EmailAddress);
                FlagData.FlagData.Add("Telephone", tempCreditLimitUpliftment.Phone);
                FlagData.FlagData.Add("Value", tempCreditLimitUpliftment.UpliftmentValue.ToString());
                FlagData.FlagData.Add("Details", tempCreditLimitUpliftment.Details);
                FlagData.FlagData.Add("StatusBranchEscalate", "False");
                FlagData.FlagData.Add("BranchEscalationComments", "");
                FlagData.FlagData.Add("CurrentLimit", tempCreditLimitUpliftment.CreditLimit.ToString());

                return FlagData;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
