using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Artists
{
    public class NoteUpdateModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string note { get; set; }
        [Required]
        public int iduserumod { get; set; }
    }
}
