namespace gmTemporaryCustomerCreditLimit.DataTransferObject
{
    public class TempCreditLimitUpliftmentDTO
    {
        public string Requester { get; set; } = string.Empty;
        public string DataModelId { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public double UpliftmentValue { get; set; }
        public double CreditLimit { get; set; }
        public string Application { get; set; } = string.Empty;
        public string CustomerAccount { get; set; } = string.Empty;
        public string AttachmentFileName { get; set; } = string.Empty;
        public string AttachmentGUID { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

       
       


    }
}
