using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Artists
{
    public class SkillCreateModel
    {
        [Required]
        public string skill { get; set; }
        [Required]
        public bool ismainrole { get; set; }
        [Required]
        public int iduseralta { get; set; }
    }
}
