using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Preselects
{
    public class PreselectartistMassiveUpdateModel
    {
        [Required]
        public int preselectid { get; set; }
        [Required]
        public int[] artistid { get; set; }
        [Required]
        public int iduseralta { get; set; }

    }
}
