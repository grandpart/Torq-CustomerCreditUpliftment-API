using gmTemporaryCustomerCreditLimit.Client;
using gmTemporaryCustomerCreditLimit.Data;
using gmTemporaryCustomerCreditLimit.DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static gmTemporaryCustomerCreditLimit.Model.DriveCallDetails;
using gmTemporaryCustomerCreditLimit.Model;
using Microsoft.AspNetCore.Http;

namespace gmTemporaryCustomerCreditLimit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttachmentController : Controller
    {
        [HttpGet]
        [Route("GetAttachmentPathByReferenceNo/{attGUID}")]
        public Task GetAttachmentPathByReferenceNo(string attGUID)
        {
            var myConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string connSyspro = myConfig.GetValue<string>("ConnectionStrings:Syspro") ?? string.Empty;

            // get attachment Location

            string tempFolderId = CreditUpliftmentData.ReturnFolderId(connSyspro, attGUID);
            AttachmentPathDetails pathDetails  =AttachmentPathDetailsData.ReturnAttachmentTable(connSyspro, tempFolderId);

            return Response.WriteAsJsonAsync<AttachmentPathDetails> (pathDetails);


        }

        [HttpPut]
        [Route("InsertAttachments/")]
        public Task InsertAttachments(AttachmentPathDTO attachmentPathDTO)
        {
            var myConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string connSyspro = myConfig.GetValue<string>("ConnectionStrings:Syspro") ?? string.Empty;
           

            // Insert attachment per location
            bool results = AttachmentPathDetailsData.InsertAttachment(connSyspro, attachmentPathDTO);

            return Response.WriteAsync(results.ToString());


        }
    }
}
