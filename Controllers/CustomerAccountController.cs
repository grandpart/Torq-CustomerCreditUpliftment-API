using gmTemporaryCustomerCreditLimit.Data;
using gmTemporaryCustomerCreditLimit.Model;
using Microsoft.AspNetCore.Http;
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

            return Response.WriteAsJsonAsync<CustomerDetails>(customer);
            
        }

        [HttpGet]
        [Route("GetCustomerDetailsPerUserBranch/{branch}")]
        public Task GetCustomerDetailsPerBranch( string branch)
        {
            var myConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string connSyspro = myConfig.GetValue<string>("ConnectionStrings:Syspro") ?? string.Empty;

            var customer = CustomerDetailsData.GetAllActiveCustomerDetailsPerBranch(connSyspro, branch);

 
            return Response.WriteAsJsonAsync<List<CustomerDetails>>(customer);

        }

        [HttpGet]
        [Route("SearchAllCustomerDetails/{searchvalue}")]
        public Task SearchAllCustomerDetails(string searchvalue)
        {
            var myConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string connSyspro = myConfig.GetValue<string>("ConnectionStrings:Syspro") ?? string.Empty;

            var customer = CustomerDetailsData.SearchAllActiveCustomerDetails(connSyspro, searchvalue);


            return Response.WriteAsJsonAsync<List<CustomerDetails>>(customer);


        }



    }
}
