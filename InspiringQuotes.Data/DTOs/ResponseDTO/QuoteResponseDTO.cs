using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InspiringQuotes.Data.DTOs.RequestDTO;

namespace InspiringQuotes.Data.DTOs.ResponseDTO
{
    public class QuoteResponseDTO: QuoteRequestDTO
    {
        public int QuoteId { get; set; }
    }
}
