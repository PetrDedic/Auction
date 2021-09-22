using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aukce.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public double InstaBuy { get; set; }

        public DateTime End { get; set; }

        public User Creator { get; set; }
        [ForeignKey("Creator")]
        public int UserId { get; set; }

        public User LastBidder { get; set; }
    }
}