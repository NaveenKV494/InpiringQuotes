using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspiringQuotes.Data.Models
{
    public class Quote
    {
        [Key]
        public int QuoteId { get; set; }
        public string Author { get; set; } = null!;
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public string QuoteText { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
    }
}
