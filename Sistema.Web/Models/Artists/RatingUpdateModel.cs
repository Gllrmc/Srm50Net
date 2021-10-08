using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Artists
{
    public class RatingUpdateModel
    {
        [Required]
        public int id { get; set; }
        public string projectname { get; set; }
        [Required]
        public int score { get; set; }
        [Required]
        public int iduserumod { get; set; }
    }
}
