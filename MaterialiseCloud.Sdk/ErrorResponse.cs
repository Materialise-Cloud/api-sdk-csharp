namespace MaterialiseCloud.Sdk
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
            Errors = new Error[0];
        }

        public ErrorResponse(int code, string message)
        {
            Errors = new[]
            {
                new Error (code, message)
            };
        }

        public Error[] Errors { get; set; }
    }
}