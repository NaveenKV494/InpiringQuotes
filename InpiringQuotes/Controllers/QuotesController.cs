using System.Threading;
using InspiringQuotes.Data.DTOs.RequestDTO;
using InspiringQuotes.Service.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace InpiringQuotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly IQuoteService _quoteService;

        public QuotesController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }
        [HttpPost("CreateQuotes")]
        public async Task<IActionResult> CreateQuotes(List<QuoteRequestDTO> req)
        {
            var resp = await _quoteService.CreateQuotes(req);
            return StatusCode((int)resp.StatusCode, resp);
        }

        [HttpGet("GetAllQuotes")]
        public async Task<IActionResult> GetAllQuotes()
        {
            var resp = await _quoteService.GetAllQuotes();
            return StatusCode((int)resp.StatusCode, resp);
        }
        [HttpGet("GetQuote/{id}")]
        public async Task<IActionResult> GetQuoteById(int id)
        {
            var resp = await _quoteService.GetQuoteById(id);
            return StatusCode((int)resp.StatusCode, resp);
        }
        [HttpPut("UpdateQuote/{id}")]
        public async Task<IActionResult> UpdateQuote(int id, QuoteRequestDTO req)
        {
            var role = "User";
            if (HttpContext.Request.Headers.TryGetValue("UserRole", out StringValues userRole))
            {
                role = userRole.ToString();
            }
            var resp = await _quoteService.UpdateQuote(id, req, role.ToLower());
            return StatusCode((int)resp.StatusCode, resp);
        }
        [HttpDelete("DeleteQuote/{id}")]
        public async Task<IActionResult> DeleteQuote(int id)
        {
            var resp = await _quoteService.DeleteQuote(id);
            return StatusCode((int)resp.StatusCode, resp);
        }
        [HttpPost("Search")]
        public async Task<IActionResult> SearchQuote([FromBody] QuoteFilter req, CancellationToken cancellationToken)
        {
            req.TagsFilter = req.TagsFilter.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToList();
            var resp = await _quoteService.SearchQuote(req, cancellationToken);
            return StatusCode((int)resp.StatusCode, resp);
        }
    }
}
