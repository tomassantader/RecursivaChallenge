using HoroscopeChallenge.Application.Queries.Horoscope;
using HoroscopeChallenge.Application.Services.UserService;
using HoroscopeChallenge.Domain.Entities;
using HoroscopeChallenge.Domain.Repositories;
using Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class GetHoroscopeStatsQueryHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IUserService> _userServiceMock = new();
    private readonly Mock<IHoroscopeQueryRepository> _horoscopeRepositoryMock = new();

    private GetHoroscopeStatsQueryHandler CreateHandler()
    {
        return new GetHoroscopeStatsQueryHandler(
            _userRepositoryMock.Object,
            _userServiceMock.Object,
            _horoscopeRepositoryMock.Object);
    }

    [Fact]
    public async Task GetHoroscopeStats_ShouldReturnMostSearchedSign()
    {
        // Arrange
        var history = new List<HoroscopeQuery>
        {
            new HoroscopeQuery { UserId = 1, Sign = "Aries", Horoscope = "test", QueryDate = DateTime.UtcNow },
            new HoroscopeQuery { UserId = 2, Sign = "Aries", Horoscope = "test", QueryDate = DateTime.UtcNow },
            new HoroscopeQuery { UserId = 3, Sign = "Leo", Horoscope = "test", QueryDate = DateTime.UtcNow }
        };

        _horoscopeRepositoryMock
            .Setup(x => x.GetHistoryAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(history);

        var handler = CreateHandler();

        // Act
        var result = await handler.Handle(new GetHoroscopeStatsQuery(), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        result.Result.Should().NotBeNull();
        result.Result!.MostSearchedZodiacSign.Should().Be("Aries");
    }

    [Fact]
    public async Task GetHoroscopeStats_ShouldReturnSearchHistory()
    {
        // Arrange
        var history = new List<HoroscopeQuery>
        {
            new HoroscopeQuery
            {
                UserId = 1,
                Sign = "Libra",
                Horoscope = "Great day",
                QueryDate = DateTime.UtcNow
            }
        };

        _horoscopeRepositoryMock
            .Setup(x => x.GetHistoryAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(history);

        var handler = CreateHandler();

        // Act
        var result = await handler.Handle(new GetHoroscopeStatsQuery(), CancellationToken.None);

        // Assert
        result.Result.Should().NotBeNull();
        result.Result!.SearchHistory.Should().HaveCount(1);

        var item = result.Result.SearchHistory.First();
        item.ZodiacSign.Should().Be("Libra");
    }
}