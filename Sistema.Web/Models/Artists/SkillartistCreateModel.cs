using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Artists
{
    public class SkillartistCreateModel
    {
        [Required]
        public int artistid { get; set; }
        [Required]
        public int skillid { get; set; }
        [Required]
        public int iduseralta { get; set; }
    }
}
