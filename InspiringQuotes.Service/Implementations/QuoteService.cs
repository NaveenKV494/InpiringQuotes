using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using InspiringQuotes.Common.AppResponse;
using InspiringQuotes.Data.DTOs.RequestDTO;
using InspiringQuotes.Data.DTOs.ResponseDTO;
using InspiringQuotes.Data.Repositories.Abstractions;
using InspiringQuotes.Service.Abstractions;
using InspiringQuotes.Data.Models;
using InspiringQuotes.Common.Constants;
using InspiringQuotes.Common.CustomExceptions;
using System.Net;

namespace InspiringQuotes.Service.Implementations
{
    public class QuoteService: IQuoteService
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly IMapper _autoMapper;
        
        public QuoteService(IQuoteRepository quoteRepository, IMapper mapper) 
        {
            _quoteRepository = quoteRepository;
            _autoMapper = mapper;
        }

        public async Task<GenericResponse<string>> CreateQuotes(List<QuoteRequestDTO> req)
        {
            List<Quote> quotes = _autoMapper.Map<List<Quote>>(req);
            var response = await _quoteRepository.CreateQuoteAsync(quotes);

            return AppResponseFactory.SuccessResponse(AppMessage.QuoteCreatedSuccess);
        }

        public async Task<GenericResponse<string>> DeleteQuote(int id)
        {
            var quote = _quoteRepository.DeleteQuoteAsync(id);
            if (quote == null)
                throw new UserFriendlyException(AppMessage.InvalidQuoteId);
            var response = _autoMapper.Map<QuoteResponseDTO>(quote);

            return AppResponseFactory.SuccessResponse(AppMessage.DeleteQuote);
        }

        public async Task<GenericResponse<List<QuoteResponseDTO>>> GetAllQuotes()
        {
            var quote = await _quoteRepository.GetQuoteAllAsync();
            var response = _autoMapper.Map<List<QuoteResponseDTO>>(quote);

            return AppResponseFactory.SuccessResponse(response);
        }

        public async Task<GenericResponse<QuoteResponseDTO>> GetQuoteById(int id)
        {
            var quote = await _quoteRepository.GetQuoteByIdAsync(id);
            if (quote == null)
                throw new UserFriendlyException(AppMessage.InvalidQuoteId);
            var response = _autoMapper.Map<QuoteResponseDTO>(quote);

            return AppResponseFactory.SuccessResponse(response);
        }

        public async Task<GenericResponse<List<QuotePaginationDTO>>> SearchQuote(QuoteFilter filter, CancellationToken cancellationToken)
        {
            var quotes = await _quoteRepository.SearchQuoteAsync(filter, cancellationToken);
            return AppResponseFactory.SuccessResponse(quotes);
        }

        public async Task<GenericResponse<QuoteResponseDTO>> UpdateQuote(int id, QuoteRequestDTO req, string userRole)
        {
            // Retrieve the existing quote
            var existingQuote = await _quoteRepository.GetQuoteByIdAsync(id);

            if (existingQuote == null)
                throw new UserFriendlyException(AppMessage.InvalidQuoteId);

            // Update the quote properties
            existingQuote.QuoteText = req.QuoteText;
            existingQuote.Author = req.Author;
            existingQuote.Tags = req.Tags.Select(tagName => new Tag { TagName = tagName }).ToList();

            // Save changes to the database
            await _quoteRepository.UpdateQuoteAsync(existingQuote);

            // Map the updated entity to QuoteResponseDTO
            var updatedQuoteDTO = _autoMapper.Map<QuoteResponseDTO>(existingQuote);

            // Return success response
            return AppResponseFactory.SuccessResponse(updatedQuoteDTO);
        }

        

    }
}
