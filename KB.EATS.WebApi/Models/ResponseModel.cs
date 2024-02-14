namespace KB.EATS.WebApi.Models
{
    /// <summary>
    /// A generic response model for all API calls to return.
    /// </summary>
    public class ResponseModel
    {
        public bool Result { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
}
