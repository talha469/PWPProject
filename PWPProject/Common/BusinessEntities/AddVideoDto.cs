using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.BusinessEntities
{
    public class AddVideoDto
    {
        [DefaultValue("")]
        [Required(ErrorMessage = "VideoId is required")]
        public string VideoId { get; set; }

        [DefaultValue("")]
        public string? Tags { get; set; }

        [DefaultValue("")]
        public string? Description { get; set; }
    }
}
