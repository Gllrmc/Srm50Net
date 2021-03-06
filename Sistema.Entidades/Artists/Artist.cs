using Sistema.Entidades.Checkins;
using Sistema.Entidades.Searchs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Artists
{
    public class Artist
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string fullname { get; set; }
        [ForeignKey("skill")]
        public int? mainroleid { get; set; }
        public string projectsworked { get; set; }
        public string cost { get; set; }
        public DateTime? costingdate { get; set; }
        public int? costinguserid { get; set; }
        public string email { get; set; }
        public string alternativecontact { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string imgartist { get; set; }
        public int? proveedorid { get; set; }
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
        public Skill skill { get; set; }
        public ICollection<Skillartist> skillartists { get; set; }
        public ICollection<Selectedartist> selectedartists { get; set; }
        public ICollection<Schedule> schedules { get; set; }
        public ICollection<Rating> ratings { get; set; }
        public ICollection<Note> notes { get; set; }
        public ICollection<Portfolio> portfolios { get; set; }
        public ICollection<Checkinartist> checkinartists { get; set; }
    }
}
