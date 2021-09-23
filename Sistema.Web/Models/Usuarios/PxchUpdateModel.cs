using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Usuarios
{
    public class PxchUpdateModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string oldpassword { get; set; }
        [Required]
        public string newpassword { get; set; }
    }
}
