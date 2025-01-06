using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspiringQuotes.Data.DTOs.ResponseDTO
{
    public class QuotePaginationDTO: QuoteResponseDTO
    {
        public int TotalCount { get; set; }
        public int RowNumber { get; set; }
    }
}
