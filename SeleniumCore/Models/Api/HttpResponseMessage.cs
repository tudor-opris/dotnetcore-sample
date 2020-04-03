using System.Collections.Generic;

namespace SeleniumCore.Models
{
    class HttpResponseMessage
    {
        public string Version { get; set; }
        public object Content { get; set; }
        public int StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public List<object> Headers { get; set; }
        public object RequestMessage { get; set; }
        public bool IsSuccessStatusCode { get; set; }
    }
}
