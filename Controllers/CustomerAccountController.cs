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
        [Route("GetCustomerDetailsPerUserBranch/{username}/{branch}")]
        public Task GetCustomerDetailsPerBranch(int username, string branch)
        {
            var myConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string connSyspro = myConfig.GetValue<string>("ConnectionStrings:Syspro") ?? string.Empty;

            var customer = CustomerDetailsData.GetAllActiveCustomerDetailsPerBranch(connSyspro, branch);

 
            return Response.WriteAsJsonAsync<List<CustomerDetails>>(customer);

        }

        [HttpGet]
        [Route("SearchAllCustomerDetails/{search}")]
        public Task SearchAllCustomerDetails(string search)
        {
            var myConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string connSyspro = myConfig.GetValue<string>("ConnectionStrings:Syspro") ?? string.Empty;

            var customer = CustomerDetailsData.SearchAllActiveCustomerDetails(connSyspro, search);


            return Response.WriteAsJsonAsync<List<CustomerDetails>>(customer);


        }



    }
}
