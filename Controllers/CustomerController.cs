using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using gmTemporaryCustomerCreditLimit.Data;
using gmTemporaryCustomerCreditLimit.Model;


namespace gmTemporaryCustomerCreditLimit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {

        [HttpGet]
        [Route("GetCustomerDetails/{customerAccount}")]
        public Task GetCustomerDetailsByAccountNo( string customerAccount)
        {
            var myConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            
            string connSyspro = myConfig.GetValue<string>("ConnectionStrings:Syspro") ?? string.Empty;

            var customer = CustomerDetailsData.GetCustomerDetailsByAccountNo(connSyspro, customerAccount);

            if (customer != null)
            {
                Response.StatusCode = StatusCodes.Status200OK;
                return Response.WriteAsync(JsonConvert.SerializeObject(customer));
            }
            else
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return Response.WriteAsync(JsonConvert.SerializeObject(customer));
            }

        }
        [HttpGet]
        [Route("GetAllCustomerDetails/")]
        public Task GetAllCustomerDetails()
        {
            var myConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string connSyspro = myConfig.GetValue<string>("ConnectionStrings:Syspro") ?? string.Empty;

            var customer = CustomerDetailsData.GetAllActiveCustomerDetails(connSyspro);

            if (customer != null)
            {
                Response.StatusCode = StatusCodes.Status200OK;
                return Response.WriteAsync(JsonConvert.SerializeObject(customer));
            }
            else
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return Response.WriteAsync(JsonConvert.SerializeObject(customer));
            }

        }



    }
}
