using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public long ID { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Mail { get; set; }
        public string Username { get; set; }
        public string Clave { get; set; }
        public string Codigo_Recuperar_Clave { get; set; }

        public Usuario() { }
    }
}
