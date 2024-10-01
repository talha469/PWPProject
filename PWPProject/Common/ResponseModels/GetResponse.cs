using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ResponseModels
{
    public class GetResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public DateTime Timestamp { get; set; }
        public string? RequestId { get; set; }

        public Dictionary<string, Dictionary<string, string>>? Namespaces { get; set; } // Namespaces for link relations

    }

}
