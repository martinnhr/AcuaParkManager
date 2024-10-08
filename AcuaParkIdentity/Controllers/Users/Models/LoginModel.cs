﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AcuaParkIdentity.Controllers.Users.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
