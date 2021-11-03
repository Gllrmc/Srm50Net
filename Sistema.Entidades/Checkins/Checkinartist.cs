using Sistema.Entidades.Artists;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Checkins
{
    public class Checkinartist
    {
        [Key]
        public int id { get; set; }
        [Required]
        [ForeignKey("artist")]
        public int artistid { get; set; }
        [Required]
        [ForeignKey("checkin")]
        public int checkinid { get; set; }
        [Required]
        public int iduseralta { get; set; }
        [Required]
        public DateTime fecalta { get; set; }
        [Required]
        public int iduserumod { get; set; }
        [Required]
        public DateTime fecumod { get; set; }
        [Required]
        public bool activo { get; set; }
        public Artist artist { get; set; }
        public Checkin checkin { get; set; }
    }
}
