using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Workshop.API.Middlewares;
using Workshop.Domain.Entities;
using Workshop.Domain.Exceptions;
using Xunit;


namespace Workshop.API.Tests.Middlewares
{
    public class ErrorHandlingMiddlewareTest
    {
        private readonly ErrorHandlingMiddleware _middleware;
        private readonly Mock<ILogger<ErrorHandlingMiddleware>> _loggerMock;
        private readonly DefaultHttpContext _context;
        private readonly Mock<RequestDelegate> _nextDelegateMock;

        public ErrorHandlingMiddlewareTest()
        {
            _loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            _middleware = new ErrorHandlingMiddleware(_loggerMock.Object);
            _context = new DefaultHttpContext();
            _nextDelegateMock = new Mock<RequestDelegate>();
        }

        [Fact]
        public async Task InvokeAsync_WhenNoExceptionThrown_ShouldCallNextDelegate()
        {
            await _middleware.InvokeAsync(_context, _nextDelegateMock.Object);

            _nextDelegateMock.Verify(next => next(_context), Times.Once);
        }

        [Fact]
        public async Task InvokeAsync_WhenNotFoundExceptionThrown_ShouldSetStatusCode404()
        {
            var notFoundException = new NotFoundException(nameof(Reservation), "1");

            await _middleware.InvokeAsync(_context, _ => throw notFoundException);

            _context.Response.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task InvokeAsync_WhenForbidExceptionThrown_ShouldSetStatusCode403()
        {
            var unauthorizedException = new UnauthorizedException();

            await _middleware.InvokeAsync(_context, _ => throw unauthorizedException);

            _context.Response.StatusCode.Should().Be(401);
        }

        [Fact]
        public async Task InvokeAsync_WhenGenericExceptionThrown_ShouldSetStatusCode500()
        {
            var unauthorizedException = new Exception();

            await _middleware.InvokeAsync(_context, _ => throw unauthorizedException);

            _context.Response.StatusCode.Should().Be(500);
        }


        [Fact]
        public async Task InvokeAsync_WhenCarNotAvailableExceptions_ShouldSetStatusCode400()
        {
            var carNotAvailableException = new CarNotAvailableException("1", DateOnly.FromDateTime(DateTime.Now),
                DateOnly.FromDateTime(DateTime.Now));

            await _middleware.InvokeAsync(_context, _ => throw carNotAvailableException);

            _context.Response.StatusCode.Should().Be(400);
        }
    }
}