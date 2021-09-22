using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades.Usuarios
{
    public class Rol
    {
        public int id { get; set; }
        [Required]
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool activo { get; set; }
        public ICollection<Usuario> usuarios { get; set; }
    }
}
