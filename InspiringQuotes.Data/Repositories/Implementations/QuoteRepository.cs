using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InspiringQuotes.Data.DBContext;
using InspiringQuotes.Data.Models;
using InspiringQuotes.Data.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using InspiringQuotes.Data.DTOs.ResponseDTO;
using InspiringQuotes.Data.DTOs.RequestDTO;

namespace InspiringQuotes.Data.Repositories.Implementations
{
    public class QuoteRepository : IQuoteRepository
    {
        private QuoteDbContext _dbContext;

        public QuoteRepository(QuoteDbContext quoteDbContext)
        {
            _dbContext =  quoteDbContext;
        }
        
        public async Task<bool> CreateQuoteAsync(List<Quote> quotes)
        {
            await _dbContext.Quotes.AddRangeAsync(quotes);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task DeleteQuoteAsync(int id)
        {
            var quote = await _dbContext.Quotes.FindAsync(id);
            if (quote != null)
            {
                _dbContext.Quotes.Remove(quote);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Quote>> GetQuoteAllAsync()
        {
            return await _dbContext.Quotes.ToListAsync();
        }

        public async Task<Quote> GetQuoteByIdAsync(int id)
        {
            return await _dbContext.Quotes.FindAsync(id);
        }

        public async Task<List<QuotePaginationDTO>> SearchQuoteAsync(QuoteFilter quoteFilter, CancellationToken cancellationToken)
        {
            // Base query
            var query = _dbContext.Quotes
                .Include(q => q.Tags)
                .AsQueryable();

            // Apply filters
            if (!string.IsNullOrWhiteSpace(quoteFilter.AuthorFilter))
                query = query.Where(x => x.Author.ToLower().Contains(quoteFilter.AuthorFilter.ToLower()));

            if (quoteFilter.TagsFilter.Any())
                query = query.Where(x => x.Tags.Any(z => quoteFilter.TagsFilter.Contains(z.TagName)));

            if (!string.IsNullOrWhiteSpace(quoteFilter.InspirationalQuoteFilter))
                query = query.Where(x => x.QuoteText.ToLower().Contains(quoteFilter.InspirationalQuoteFilter.ToLower()));

            var totalCount = await query.CountAsync(cancellationToken);

            // Apply pagination and project
            var result = await query
                .OrderBy(q => q.QuoteId) // Sort order
                .Skip(quoteFilter.CurrentPage * quoteFilter.PageSize)
                .Take(quoteFilter.PageSize)
                .Select(q => new QuotePaginationDTO
                {
                    QuoteId = q.QuoteId,
                    Author = q.Author,
                    QuoteText = q.QuoteText,
                    Tags = q.Tags.Select(t => t.TagName).ToList(),
                    TotalCount = totalCount
                })
                .ToListAsync(cancellationToken);
            for (int i = 0; i < result.Count; i++)
            {
                result[i].RowNumber = i + (quoteFilter.CurrentPage * quoteFilter.PageSize) + 1;
            }

            return result;
        }


        public async Task UpdateQuoteAsync(Quote quote)
        {
            _dbContext.Quotes.Update(quote);
            await _dbContext.SaveChangesAsync();
        }
    }
}
