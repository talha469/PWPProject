using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common.Helper_Methods
{
    public class URLConvertor
    {
        public static class UrlConverter
        {
            public static string ConvertToUrl(string input)
            {
                if (string.IsNullOrEmpty(input))
                {
                    throw new ArgumentException("Input cannot be null or empty", nameof(input));
                }

                // Encode the URL
                return HttpUtility.UrlEncode(input);
            }

        }
    }
}
