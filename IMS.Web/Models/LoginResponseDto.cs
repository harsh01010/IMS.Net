﻿using IMS.Web.Models;

namespace IMS.Web.Models
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }

        public string JwtToken {  get; set; }
    }
}
