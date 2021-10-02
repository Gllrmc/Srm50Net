using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Artists
{
    public class Skill
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string  skill { get; set; }
        [Required]
        public bool ismainrole { get; set; }
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
        public ICollection<Mainroleartist> mainroleartists { get; set; }
        public ICollection<Skillartist> skillartists { get; set; }
    }
}
