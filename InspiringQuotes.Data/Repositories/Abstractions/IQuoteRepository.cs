using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InspiringQuotes.Data.DTOs.RequestDTO;
using InspiringQuotes.Data.DTOs.ResponseDTO;
using InspiringQuotes.Data.Models;

namespace InspiringQuotes.Data.Repositories.Abstractions
{
    public interface IQuoteRepository
    {
        Task<Quote> GetQuoteByIdAsync(int id);
        Task<IEnumerable<Quote>> GetQuoteAllAsync();
        Task<List<QuotePaginationDTO>> SearchQuoteAsync(QuoteFilter quoteFilter, CancellationToken cancellationToken);
        Task <bool> CreateQuoteAsync(List<Quote> quotes);
        Task UpdateQuoteAsync(Quote quote);
        Task DeleteQuoteAsync(int id);
    }
}
