using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Artists
{
    public class ScheduleViewModel
    {
        public int id { get; set; }
        public int artistid { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public string reason { get; set; }
        public int? limboid { get; set; }
        public int? proyectoid { get; set; }
        public int iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
        public bool activo { get; set; }
    }
}
