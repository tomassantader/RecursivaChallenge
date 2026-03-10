using HoroscopeChallenge.Application.Services;
using HoroscopeChallenge.Domain.Entities;
using HoroscopeChallenge.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Moq;
using FluentAssertions;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class LoginCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<ITokenService> _tokenServiceMock = new();

    private LoginCommandHandler CreateHandler()
    {
        return new LoginCommandHandler(
            _userRepositoryMock.Object,
            _tokenServiceMock.Object);
    }

    [Fact]
    public async Task Login_ShouldReturnUnauthorized_WhenUserDoesNotExist()
    {
        // Arrange
        _userRepositoryMock
            .Setup(x => x.GetByUsernameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((User)null);

        var handler = CreateHandler();

        var command = new LoginCommand
        {
            Username = "user",
            Password = "123456"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Login_ShouldReturnUnauthorized_WhenPasswordIsIncorrect()
    {
        // Arrange
        var passwordHasher = new PasswordHasher<User>();

        var user = new User
        {
            Username = "user",
            Email = "user@test.com",
            BirthDate = new DateTime(1995, 5, 10)
        };

        user.PasswordHash = passwordHasher.HashPassword(user, "correctPassword");

        _userRepositoryMock
            .Setup(x => x.GetByUsernameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var handler = CreateHandler();

        var command = new LoginCommand
        {
            Username = "user",
            Password = "wrongPassword"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Login_ShouldReturnToken_WhenCredentialsAreValid()
    {
        // Arrange
        var passwordHasher = new PasswordHasher<User>();

        var user = new User
        {
            Id = 1,
            Username = "user",
            Email = "user@test.com",
            BirthDate = new DateTime(1995, 5, 10)
        };

        user.PasswordHash = passwordHasher.HashPassword(user, "123456");

        _userRepositoryMock
            .Setup(x => x.GetByUsernameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _tokenServiceMock
            .Setup(x => x.GenerateToken(It.IsAny<User>()))
            .Returns("fake-jwt-token");

        var handler = CreateHandler();

        var command = new LoginCommand
        {
            Username = "user",
            Password = "123456"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        result.Result.Should().NotBeNull();
        result.Result!.token.Should().Be("fake-jwt-token");
    }
}