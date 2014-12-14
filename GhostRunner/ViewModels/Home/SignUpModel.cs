using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GhostRunner.ViewModels.Home
{
    public class SignUpModel : ViewModel
    {
        public SignUpModel()
        {
            AllowAccountCreate = true;
        }

        public User User { get; set; }

        [Required(ErrorMessage = " * Required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = " * Requires at least 8 characters")]
        public String Password { get; set; }

        [Required(ErrorMessage = " * Required")]
        [Compare("Password", ErrorMessage = " * Your passwords do not match")]
        public String PasswordConfirm { get; set; }

        public Boolean AllowAccountCreate { get; set; }
    }
}