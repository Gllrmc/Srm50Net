using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Checkins
{
    public class CheckinartistCreateModel
    {
        [Required]
        public int artistid { get; set; }
        [Required]
        public int checkinid { get; set; }
        [Required]
        public int iduseralta { get; set; }
    }
}
