using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Cliente
    {
        public long ID { get; set; }
        public string CuitDni { get; set; }
        public string Apenom { get; set; }
        public string Direccion { get; set; }
        public Localidad localidad { get; set; }
        public string Telefono { get; set; }
        public string Mail { get; set; }
        public string FechaAlta { get; set; }
        public bool Estado { get; set; }

        public Cliente() { }

    }
}
