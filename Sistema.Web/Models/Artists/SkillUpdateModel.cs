using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Artists
{
    public class SkillUpdateModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string skill { get; set; }
        [Required]
        public bool ismainrole { get; set; }
        [Required]
        public int iduserumod { get; set; }
        [Required]
        public bool activo { get; set; }
    }
}
