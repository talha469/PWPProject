namespace Common.ResponseModels
{
    public class PutResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T UpdatedData { get; set; }
        public DateTime Timestamp { get; set; }
        public string RequestId { get; set; }

    }
}
