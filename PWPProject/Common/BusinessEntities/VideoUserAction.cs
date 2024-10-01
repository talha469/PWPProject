using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Common.BusinessEntities
{
    public class VideoUserAction
    {
        public string VideoId { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }

        public bool? isBookMarked { get; set; }

        public int? VoteType { get; set; }
    }

    public class VideoUserActionDto
    {
        public string VideoId { get; set; }

        public bool? isBookMarked { get; set; }

        public int? VoteType { get; set; }
    }
}
