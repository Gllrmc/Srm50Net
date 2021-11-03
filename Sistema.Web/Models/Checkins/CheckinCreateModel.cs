using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Checkins
{
    public class CheckinCreateModel
    {
        [Required]
        public string checkin { get; set; }
        public string detail { get; set; }
        [Required]
        public int iduseralta { get; set; }
    }
}
