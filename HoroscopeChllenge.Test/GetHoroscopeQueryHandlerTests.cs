using HoroscopeChallenge.Application.Queries.Horoscope;
using HoroscopeChallenge.Application.Services.UserService;
using HoroscopeChallenge.Domain.Entities;
using HoroscopeChallenge.Domain.Interfaces;
using HoroscopeChallenge.Domain.Repositories;
using Moq;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

public class GetHoroscopeQueryHandlerTests
{
    private readonly Mock<IUserRepository> _userRepository = new();
    private readonly Mock<IUserService> _userService = new();
    private readonly Mock<IHoroscopeService> _horoscopeService = new();
    private readonly Mock<IHoroscopeCacheRepository> _cacheRepository = new();
    private readonly Mock<IHoroscopeQueryRepository> _queryRepository = new();

    private GetHoroscopeQueryHandler CreateHandler()
    {
        return new GetHoroscopeQueryHandler(
            _userRepository.Object,
            _userService.Object,
            _horoscopeService.Object,
            _cacheRepository.Object,
            _queryRepository.Object);
    }

    [Fact]
    public async Task GetHoroscope_ShouldUseCache_WhenCacheExists()
    {
        // Arrange
        var user = new User { Id = 1, BirthDate = new DateTime(1995, 5, 10) };

        _userService.Setup(x => x.UserId).Returns(1);

        _userRepository
            .Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _cacheRepository
            .Setup(x => x.GetBySignAndDate(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new HoroscopeCache
            {
                Sign = "Taurus",
                Horoscope = "cached horoscope"
            });

        var handler = CreateHandler();

        // Act
        var result = await handler.Handle(new GetHoroscopeQuery(), CancellationToken.None);

        // Assert
        result.Result.Should().NotBeNull();
        result.Result!.Horoscope.Should().Be("cached horoscope");

        _horoscopeService.Verify(
            x => x.GetHoroscopeAsync(It.IsAny<string>(), It.IsAny<DateTime>()),
            Times.Never);
    }

    [Fact]
    public async Task GetHoroscope_ShouldCallApi_WhenCacheDoesNotExist()
    {
        // Arrange
        var user = new User { Id = 1, BirthDate = new DateTime(1995, 5, 10) };

        _userService.Setup(x => x.UserId).Returns(1);

        _userRepository
            .Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _cacheRepository
            .Setup(x => x.GetBySignAndDate(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((HoroscopeCache)null);

        _horoscopeService
            .Setup(x => x.GetHoroscopeAsync(It.IsAny<string>(), It.IsAny<DateTime>()))
            .ReturnsAsync(new HoroscopeResponse { Horoscope = "api horoscope" });

        var handler = CreateHandler();

        // Act
        var result = await handler.Handle(new GetHoroscopeQuery(), CancellationToken.None);

        // Assert
        result.Result.Should().NotBeNull();
        result.Result!.Horoscope.Should().Be("api horoscope");

        _cacheRepository.Verify(
            x => x.AddAsync(It.IsAny<HoroscopeCache>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetHoroscope_ShouldReturnNotFound_WhenHoroscopeIsNull()
    {
        // Arrange
        var user = new User { Id = 1, BirthDate = new DateTime(1995, 5, 10) };

        _userService.Setup(x => x.UserId).Returns(1);

        _userRepository
            .Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _cacheRepository
            .Setup(x => x.GetBySignAndDate(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((HoroscopeCache)null);

        _horoscopeService
            .Setup(x => x.GetHoroscopeAsync(It.IsAny<string>(), It.IsAny<DateTime>()))
            .ReturnsAsync((HoroscopeResponse)null);

        var handler = CreateHandler();

        // Act
        var result = await handler.Handle(new GetHoroscopeQuery(), CancellationToken.None);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}