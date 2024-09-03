namespace gmTemporaryCustomerCreditLimit.Model
{
    public class CustomerDetails
    {
        public string Customer { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public double AvailableCredit { get; set; }
        public double CreditLimit { get; set; }
    }
}
