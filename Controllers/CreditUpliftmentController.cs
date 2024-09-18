using gmTemporaryCustomerCreditLimit.Client;
using gmTemporaryCustomerCreditLimit.Data;
using gmTemporaryCustomerCreditLimit.DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static gmTemporaryCustomerCreditLimit.Model.DriveCallDetails;


namespace gmTemporaryCustomerCreditLimit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreditUpliftmentController : Controller
    {

        [HttpPost]
        [Route("RequestCreditUpliftment/")]
        public Task RequestCreditUpliftment(TempCreditLimitUpliftmentDTO creditLimitDTO)
        {
            var myConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string driveUsername = myConfig.GetValue<string>("DriveEngineCredentials:DriveUsername") ?? string.Empty;
            string drivePassword = myConfig.GetValue<string>("DriveEngineCredentials:DrivePassword") ?? string.Empty;
            string connTorq = myConfig.GetValue<string>("ConnectionStrings:Torq") ?? string.Empty;

            if (creditLimitDTO == null)

            {
                Response.StatusCode = StatusCodes.Status404NotFound;

                return Response.WriteAsync(JsonConvert.SerializeObject(""));

            }

            // call the drive workflow

            WSCRaiseFlag workFlowFlag = DriveCallDetailsData.GetFlagDetails(creditLimitDTO);
            string workFlowData = JsonConvert.SerializeObject(workFlowFlag);
            string ReferenceNo = Drive.Post(driveUsername, drivePassword, workFlowData);

            //Insert into Torq database 
            CreditUpliftmentData.InsertCreditUpliftment(connTorq, creditLimitDTO, ReferenceNo);
            

            return Response.WriteAsync(JsonConvert.SerializeObject(ReferenceNo));

        }


    }
}
