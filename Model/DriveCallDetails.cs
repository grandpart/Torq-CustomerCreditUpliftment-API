namespace gmTemporaryCustomerCreditLimit.Model
{
    public class DriveCallDetails
    {
        public class WSCRaiseFlag
        {
            public string CallerId { get; set; } = string.Empty;
            public string FolderId { get; set; } = string.Empty;
            public string DataModelId { get; set; } = string.Empty;
            public string FlagToRaise { get; set; } = string.Empty;
            public string RaisedByApplication { get; set; } = string.Empty;

            public Dictionary<string, string> FlagData = new();
        }
    }
}
