using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace bookstore.DataTransferObject.DTOs
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class RegisterAccountDTO
    {
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class AuthInformation
    {
        public string Token { get; set; }
        public DateTime? Expires { get; set; }
    }

    public class AccountDTO
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }

        public AuthInformation AuthInformation { get; set; }
    }
}
