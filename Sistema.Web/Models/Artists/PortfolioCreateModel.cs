using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Artists
{
    public class PortfolioCreateModel
    {
        [Required]
        public int artistid { get; set; }
        [Required]
        public string url { get; set; }
        [Required]
        public int iduseralta { get; set; }
    }
}
