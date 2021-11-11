using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Artists
{
    public class ScheduleCreateModel
    {
        [Required]
        public int artistid { get; set; }
        [Required]
        public DateTime startdate { get; set; }
        [Required]
        public DateTime enddate { get; set; }
        public string comment { get; set; }
        public int? limboid { get; set; }
        public int? proyectoid { get; set; }
        [Required]
        public int iduseralta { get; set; }
    }
}
