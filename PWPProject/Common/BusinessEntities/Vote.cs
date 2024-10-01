using Common.BusinessEntities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CommonLibrary.BusinessEntities
{
    public class Vote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VoteId { get; set; }

        [ForeignKey("Video")]
        [MaxLength(40)]
        public string Id { get; set; }

        public int UserId { get; set; }

        public int? VoteType { get; set; }

        public DateTime VoteDate { get; set; }

        //Navigation Property
        public Video Video { get; set; }

        public Vote()
        {
            VoteType = 0;
            VoteDate = DateTime.Now;
        }
    }
}
