using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Preselects
{
    public class Preselect
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string code { get; set; }
        [Required]
        public string preselect { get; set; }
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
        public IEnumerable<Preselectartist> preselectartists { get; set; }
    }
}
