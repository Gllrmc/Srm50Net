using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Artists
{
    public class ScheduleUpdateModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        public DateTime startdate { get; set; }
        [Required]
        public DateTime enddate { get; set; }
        public string reason { get; set; }
        public int? limboid { get; set; }
        public int? proyectoid { get; set; }
        [Required]
        public int iduserumod { get; set; }
    }
}
