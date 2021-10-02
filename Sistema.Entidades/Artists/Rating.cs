using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Artists
{
    public class Rating
    {
        [Key]
        public int id { get; set; }
        [Required]
        [ForeignKey("artist")]
        public int artistid { get; set; }
        public string projectname { get; set; }
        public int score { get; set; }
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
    }
}
