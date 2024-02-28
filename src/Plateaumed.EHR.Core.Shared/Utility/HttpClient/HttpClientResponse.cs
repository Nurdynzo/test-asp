namespace Plateaumed.EHR.Utility.HttpClient
{
    public class HttpClientResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public bool isSuccess { get; set; } = false;
    }
}