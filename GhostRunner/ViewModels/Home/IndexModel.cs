using GhostRunner.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GhostRunner.ViewModels.Home
{
    public class IndexModel : ViewModel
    {
        public User User { get; set; }
        
        [Required(ErrorMessage = " * Required")]
        public String Password { get; set; }
    }
}