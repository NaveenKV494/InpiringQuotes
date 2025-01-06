using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InspiringQuotes.Data.DTOs.RequestDTO;
using InspiringQuotes.Data.DTOs.ResponseDTO;
using InspiringQuotes.Common.AppResponse;

namespace InspiringQuotes.Service.Abstractions
{
    public interface IQuoteService
    {
        Task<GenericResponse<string>> CreateQuotes(List<QuoteRequestDTO> req);
        Task<GenericResponse<List<QuoteResponseDTO>>> GetAllQuotes();
        Task<GenericResponse<QuoteResponseDTO>> GetQuoteById(int id);
        Task<GenericResponse<QuoteResponseDTO>> UpdateQuote(int id, QuoteRequestDTO req, string userRole);
        Task<GenericResponse<string>> DeleteQuote(int id);
        Task<GenericResponse<List<QuotePaginationDTO>>> SearchQuote(QuoteFilter filter, CancellationToken cancellationToken);
    }
}
