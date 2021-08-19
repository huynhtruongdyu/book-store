namespace Portal.API.Models
{
    public class ErrorVM
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }

        public override string ToString()
        {
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            return jsonString;
        }
    }
}