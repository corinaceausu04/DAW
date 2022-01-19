using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAW.Data.DTO
{
    public class RegistrationResponseDto
    {
        public bool IsOkRegistration { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
