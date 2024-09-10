using gmTemporaryCustomerCreditLimit.Data;
using gmTemporaryCustomerCreditLimit.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace gmTemporaryCustomerCreditLimit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerAccountController : Controller
    {

        [HttpGet]
        [Route("GetCustomerDetails/{customerAccount}")]
        public Task GetCustomerDetailsByAccountNo(string customerAccount)
        {
            var myConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string connSyspro = myConfig.GetValue<string>("ConnectionStrings:Syspro") ?? string.Empty;

            var customer = CustomerDetailsData.GetCustomerDetailsByAccountNo(connSyspro, customerAccount);

            if (customer != null)
            {
                Response.StatusCode = StatusCodes.Status200OK;

            }
            else
            {
                Response.StatusCode = StatusCodes.Status404NotFound;

            }
            return Response.WriteAsJsonAsync<CustomerDetails>(customer);
            //return Response.WriteAsync(JsonConvert.SerializeObject(customer));
        }
        [HttpGet]
        [Route("GetCustomerDetailsPerUserBranch/{username}/{branch}")]
        public Task GetCustomerDetailsPerBranch(int username, string branch)
        {
            var myConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string connSyspro = myConfig.GetValue<string>("ConnectionStrings:Syspro") ?? string.Empty;

            var customer = CustomerDetailsData.GetAllActiveCustomerDetailsPerBranch(connSyspro, branch);

            if (customer != null)
            {
                Response.StatusCode = StatusCodes.Status200OK;
                return Response.WriteAsync(JsonConvert.SerializeObject(customer));
            }
            else
            {
                Response.StatusCode = StatusCodes.Status417ExpectationFailed;
                return Response.WriteAsync(JsonConvert.SerializeObject(customer));
            }

        }

        [HttpGet]
        [Route("SearchAllCustomerDetails/{search}")]
        public Task SearchAllCustomerDetails(string search)
        {
            var myConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string connSyspro = myConfig.GetValue<string>("ConnectionStrings:Syspro") ?? string.Empty;

            var customer = CustomerDetailsData.SearchAllActiveCustomerDetails(connSyspro, search);

            if (customer != null)
            {
                Response.StatusCode = StatusCodes.Status200OK;
                return Response.WriteAsync(JsonConvert.SerializeObject(customer));
            }
            else
            {
                Response.StatusCode = StatusCodes.Status408RequestTimeout;
                return Response.WriteAsync(JsonConvert.SerializeObject(customer));
            }

        }



    }
}
