using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InspiringQuotes.Data.Models;
using System.Text.Json.Serialization;

namespace InspiringQuotes.Data.DTOs.RequestDTO
{
    public class QuoteRequestDTO
    {
        [Required]
        public string Author { get; set; }
        public List<string> Tags { get; set; }
        [Required]
        [JsonPropertyName("quote")]
        public string QuoteText { get; set; }
    }
}
