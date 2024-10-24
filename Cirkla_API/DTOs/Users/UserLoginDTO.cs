﻿using System.ComponentModel.DataAnnotations;

namespace Cirkla_API.DTOs.Users
{
    public class UserLoginDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}