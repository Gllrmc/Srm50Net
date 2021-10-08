using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Artists
{
    public class PortfolioUpdateModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        public int artistid { get; set; }
        [Required]
        public string url { get; set; }
        [Required]
        public int iduserumod { get; set; }
    }
}
