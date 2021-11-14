using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Preselects
{
    public class PreselectartistMassiveCreateModel
    {
        [Required]
        public string code { get; set; }
        [Required]
        public string preselect { get; set; }
        [Required]
        public int[] artistid { get; set; }
        [Required]
        public int iduseralta { get; set; }
    }
}
