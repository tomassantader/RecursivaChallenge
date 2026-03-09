using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoroscopeChallenge.Application.DTOs
{
    public class HoroscopeRequestDto
    {
        public string Date { get; set; } = default!;
        public string Lang { get; set; } = "es";
        public string Sign { get; set; } = default!;
    }
}
