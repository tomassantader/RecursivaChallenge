using HoroscopeChallenge.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace HoroscopeChallenge.Infrastructure.Persistence;

public static class DataSeeder
{
    public static void Seed(AppDbContext db)
    {
        if (db.Users.Any())
            return;

        var passwordHasher = new PasswordHasher<User>();

        var user = new User
        {
            Username = "user",
            Email = "user@test.com",
            BirthDate = new DateTime(1995, 5, 10)
        };

        user.PasswordHash = passwordHasher.HashPassword(user, "123456");

        db.Users.Add(user);
        db.SaveChanges();
    }
}
