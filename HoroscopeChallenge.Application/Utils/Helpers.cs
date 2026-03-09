using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoroscopeChallenge.Application.Utils
{
    public static class Helpers
    {
        public static string GetSign(DateTime birthDate)
        {
            int day = birthDate.Day;
            int month = birthDate.Month;

            return (month, day) switch
            {
                (1, <= 19) => "Capricorn",
                (1, _) => "Aquarius",
                (2, <= 18) => "Aquarius",
                (2, _) => "Pisces",
                (3, <= 20) => "Pisces",
                (3, _) => "Aries",
                (4, <= 19) => "Aries",
                (4, _) => "Taurus",
                (5, <= 20) => "Taurus",
                (5, _) => "Gemini",
                (6, <= 20) => "Gemini",
                (6, _) => "Cancer",
                (7, <= 22) => "Cancer",
                (7, _) => "Leo",
                (8, <= 22) => "Leo",
                (8, _) => "Virgo",
                (9, <= 22) => "Virgo",
                (9, _) => "Libra",
                (10, <= 22) => "Libra",
                (10, _) => "Scorpio",
                (11, <= 21) => "Scorpio",
                (11, _) => "Sagittarius",
                (12, <= 21) => "Sagittarius",
                _ => "Capricorn"
            };
        }

        public static int GetDaysUntilNextBirthday(DateTime birthDate)
        {
            var today = DateTime.Today;

            var nextBirthday = new DateTime(
                today.Year,
                birthDate.Month,
                birthDate.Day);

            if (nextBirthday < today)
                nextBirthday = nextBirthday.AddYears(1);

            return (nextBirthday - today).Days;
        }
    }
}
