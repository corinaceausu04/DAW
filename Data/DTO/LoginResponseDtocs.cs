using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAW.Data.DTO
{
    public class LoginResponseDtocs
    {
        public bool IsLoginOk { get; set; }
        public string Errors { get; set; }
        public string Token { get; set; }
    }
}
