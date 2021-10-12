using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Artists
{
    public class RatingCreateModel
    {
        [Required]
        public int artistid { get; set; }
        public string projectname { get; set; }
        [Required]
        public int score { get; set; }
        [Required]
        public int iduseralta { get; set; }
    }
}
