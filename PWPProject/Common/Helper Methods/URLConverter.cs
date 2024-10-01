using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common.Helper_Methods
{
    public class URLConverter
    {
        public static string Encode(string url)
        {
            return HttpUtility.UrlEncode(url);
        }

        public static string Decode(string encodedUrl)
        {
            return HttpUtility.UrlDecode(encodedUrl);
        }
    }
}
