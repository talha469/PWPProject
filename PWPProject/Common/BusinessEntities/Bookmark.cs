using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CommonLibrary.BusinessEntities;

namespace Common.BusinessEntities
{
    public class BookMark
    {
        [Key]
        public int BookMarkId { get; set; }

        [ForeignKey("Video")]
        [MaxLength(40)]
        public string Id { get; set; }

        public int UserId { get; set; }

        public bool? isBookMarked { get; set; }

        public DateTime bookMarkDate { get; set; }

        //Navigation Property
        public Video Video { get; set; }

    }
}