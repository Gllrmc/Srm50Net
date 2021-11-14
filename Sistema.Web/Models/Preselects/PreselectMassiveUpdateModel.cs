using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Preselects
{
    public class PreselectMassiveUpdateModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        public int[] artistid { get; set; }
        [Required]
        public int iduseralta { get; set; }

    }
}
