using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kuldeep.Models
{
    public class Class1
    {
        public int id { get; set; }
        [Required(ErrorMessage = "name fild requred")]
        public string name { get; set; }
        [Required]
        public string email { get; set; }
    }
}