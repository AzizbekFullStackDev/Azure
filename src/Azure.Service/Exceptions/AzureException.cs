namespace Azure.Service.Exceptions
{
    public class AzureException : Exception
    {
        public int StatusCode { get; set; }
        public AzureException(int code, string message) : base(message)
        {
            this.StatusCode = code;
        }
    }
}
