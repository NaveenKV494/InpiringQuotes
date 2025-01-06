using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using InpiringQuotes.Controllers;
using InspiringQuotes.Common.AppResponse;
using InspiringQuotes.Data.DTOs.RequestDTO;
using InspiringQuotes.Data.DTOs.ResponseDTO;
using InspiringQuotes.Data.Models;
using InspiringQuotes.Service.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InspiringQuotes.Tests.Controllers
{
    public class QuotesControllerTests
    {
        private readonly Mock<IQuoteService> _mockQuoteService;
        private readonly QuotesController _quotesController;

        public QuotesControllerTests()
        {
            _mockQuoteService = new Mock<IQuoteService>();
            _quotesController = new QuotesController(_mockQuoteService.Object);
        }

        [Fact]
        public async Task SearchQuote_ReturnsQuote_WhenQuotesExist()
        {
            // Arrange
            var sampleQuotes = new List<QuotePaginationDTO>
            {
                new QuotePaginationDTO { QuoteId = 1, Author = "Frank Zappa", QuoteText = "Sample quote" }
            };

            var filter = new QuoteFilter
            {
                AuthorFilter = "Frank Zappa",
                TagsFilter = new List<string> { "Motivation" },
                InspirationalQuoteFilter = "Sample",
                CurrentPage = 0,
                PageSize = 10
            };

            _mockQuoteService
                .Setup(service => service.SearchQuote(filter, default))
                .ReturnsAsync(AppResponseFactory.SuccessResponse(sampleQuotes));

            // Act
            var result = await _quotesController.SearchQuote(filter, default) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);

            var response = result.Value as GenericResponse<List<QuotePaginationDTO>>;
            response.Should().NotBeNull();
            response.Response.Should().BeEquivalentTo(sampleQuotes);
        }

        [Fact]
        public async Task SearchQuote_ReturnsNoQuotes_WhenNoQuotesExist()
        {
            // Arrange
            var filter = new QuoteFilter
            {
                AuthorFilter = "Unknown",
                TagsFilter = new List<string> { "NonExistentTag" },
                InspirationalQuoteFilter = "No Match",
                CurrentPage = 0,
                PageSize = 10
            };

            _mockQuoteService
                .Setup(service => service.SearchQuote(filter, default))
                .ReturnsAsync(AppResponseFactory.SuccessResponse(new List<QuotePaginationDTO>()));

            // Act
            var result = await _quotesController.SearchQuote(filter, default) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);

            var response = result.Value as GenericResponse<List<QuotePaginationDTO>>;
            response.Should().NotBeNull();
            response.Response.Should().BeEmpty();
        }

        [Fact]
        public async Task SearchQuote_ThrowsException_WhenServiceFails()
        {
            // Arrange
            var filter = new QuoteFilter
            {
                AuthorFilter = "ErrorTrigger",
                TagsFilter = new List<string> { "FaultyTag" },
                InspirationalQuoteFilter = "Failure",
                CurrentPage = 0,
                PageSize = 10
            };

            _mockQuoteService
                .Setup(service => service.SearchQuote(filter, default))
                .ThrowsAsync(new Exception("Service failure"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _quotesController.SearchQuote(filter, default);
            });

            exception.Message.Should().Be("Service failure");
        }

        //[Fact]
        //public async Task SearchQuote_ReturnsBadRequest_WhenFilterIsInvalid()
        //{
        //    // Arrange
        //    QuoteFilter filter = null;

        //    // Act
        //    var result = await _quotesController.SearchQuote(filter, default) as BadRequestObjectResult;

        //    // Assert
        //    result.Should().NotBeNull();
        //    result.StatusCode.Should().Be(400);
        //    result.Value.Should().Be("Invalid filter criteria.");
        //}
    }
}
