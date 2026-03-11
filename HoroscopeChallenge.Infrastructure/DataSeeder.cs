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

        var users = new List<User>
        {
            new User
            {
                Username = "user",
                Email = "user@test.com",
                BirthDate = new DateTime(1995,5,10),
                PasswordHash = "123456"
            },
            new User
            {
                Username = "admin",
                Email = "admin@test.com",
                BirthDate = new DateTime(1990,8,22),
                PasswordHash = "admin123"
            }
        };

        foreach (var u in users)
        {
            u.PasswordHash = passwordHasher.HashPassword(u, u.PasswordHash);
        }

        db.Users.AddRange(users);
        db.SaveChanges();
    }
}
