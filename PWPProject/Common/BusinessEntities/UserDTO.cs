using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.BusinessEntities
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public string Email { get; set; }
        public string? Username { get; set; }

        public string? ImagePath { get; set; }

        public string Role { get; set; }

        public DateTime AccountCreatedDate { get; set; }

        // Method to get URL encoded username
        public string GetUrlEncodedImagePath()
        {
            return Common.Helper_Methods.URLConvertor.UrlConverter.ConvertToUrl(ImagePath);
        }

        public string GetUrlEncodedUserName()
        {
            return Common.Helper_Methods.URLConvertor.UrlConverter.ConvertToUrl(Username);
        }

    }

    public class UserDTOTemp
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }


    }
}
