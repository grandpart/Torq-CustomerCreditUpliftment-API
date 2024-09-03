using gmTemporaryCustomerCreditLimit.Client;
using gmTemporaryCustomerCreditLimit.Data;
using gmTemporaryCustomerCreditLimit.DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static gmTemporaryCustomerCreditLimit.Model.DriveCallDetails;
using gmTemporaryCustomerCreditLimit.Model;

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

            if (referenceNo == "")

            {
                Response.StatusCode = StatusCodes.Status200OK;

                return Response.WriteAsync("Invalid Reference Number");

            }

            // get attachment Location

            AttachmentPathDetails pathDetails  =AttachmentPathDetailsData.ReturnAttachmentTable(connSyspro, referenceNo);
          

            if (pathDetails == null)
            {
                Response.StatusCode = StatusCodes.Status200OK;
                return Response.WriteAsync(JsonConvert.SerializeObject(pathDetails));
            }
            else
            {
                Response.StatusCode = StatusCodes.Status200OK;
                return Response.WriteAsync("Unsuccessful");
            }


        }
        [HttpPut]
        [Route("InsertAttachments/{username}")]
        public Task InsertAttachments(AttachmentPathDTO attachmentPathDTO, int username)
        {
            var myConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string connSyspro = myConfig.GetValue<string>("ConnectionStrings:connSyspro") ?? string.Empty;

            if (attachmentPathDTO == null)

            {
                Response.StatusCode = StatusCodes.Status200OK;

                return Response.WriteAsync("Invalid Object Details");

            }

            // Insert attachment per location
            bool results = AttachmentPathDetailsData.InsertAttachment(connSyspro, attachmentPathDTO);

            if (results == true)
            {
                Response.StatusCode = StatusCodes.Status200OK;
                return Response.WriteAsync(JsonConvert.SerializeObject("Success"));
            }
            else
            {
                Response.StatusCode = StatusCodes.Status200OK;
                return Response.WriteAsync("Unsuccessful");
            }


        }
    }
}
