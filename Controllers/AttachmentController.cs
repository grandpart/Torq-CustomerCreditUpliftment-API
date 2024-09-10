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
        [Route("GetAttachmentPathByReferenceNo/{referenceNo}/{username}")]
        public Task GetAttachmentPathByReferenceNo(string referenceNo, int username)
        {
            var myConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string connSyspro = myConfig.GetValue<string>("ConnectionStrings:connSyspro") ?? string.Empty;

           // get attachment Location

            AttachmentPathDetails pathDetails  =AttachmentPathDetailsData.ReturnAttachmentTable(connSyspro, referenceNo);

            return Response.WriteAsJsonAsync<AttachmentPathDetails> (pathDetails);


        }
        [HttpPut]
        [Route("InsertAttachments/{username}")]
        public Task InsertAttachments(AttachmentPathDTO attachmentPathDTO, int username)
        {
            var myConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string connSyspro = myConfig.GetValue<string>("ConnectionStrings:connSyspro") ?? string.Empty;
           

            // Insert attachment per location
            bool results = AttachmentPathDetailsData.InsertAttachment(connSyspro, attachmentPathDTO);

            return Response.WriteAsync(results.ToString());


        }
    }
}
