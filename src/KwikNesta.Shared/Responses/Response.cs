namespace KwikNesta.Shared.Responses
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = default!;
        public int StatusCode { get; set; }
        public T Data { get; set; } = default!;

        protected Response() { }

        protected Response(T data, string message, int code)
        {
            Data = data;
            Message = message;
            Success = true;
            StatusCode = code;
        }

        protected Response(string message, int code)
        {
            Message = message;
            StatusCode = code;
        }

        /// <summary>
        /// Ok Response
        /// </summary>
        /// <param name="data">Items to return</param>
        /// <param name="statusCode">Expected to be HTTP Status Code. Defaulted to 200 OK</param>
        /// <param name="message">Optional Message</param>
        /// <returns></returns>
        public static Response<T> Ok(T data, int statusCode = 200, string message = "Successful")
        {
            return new Response<T>(data, message, statusCode);
        }

        /// <summary>
        /// Failed request
        /// </summary>
        /// <param name="message">Error Message</param>
        /// <param name="statusCode">Status Code. Expected to be a HTTP Status Code</param>
        /// <returns></returns>
        public static Response<T> Fail(string message, int statusCode)
        {
            return new Response<T>(message, statusCode);
        }
    }
}