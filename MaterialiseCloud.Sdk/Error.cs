namespace MaterialiseCloud.Sdk
{
    public class Error
    {
        public Error() { }

        public Error(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public int Code { get; set; }

        public string Message { get; set; }
    }
}