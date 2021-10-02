using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Artists
{
    public class Mainroleartist
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int artistid { get; set; }
        [Required]
        public int skillid { get; set; }
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
        public Skill skill { get; set; }
    }
}
