using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Precio
    {
        public long ID { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Aclaraciones { get; set; }
        public decimal Pesos { get; set; }
        public decimal Dolares { get; set; }
        public bool Estado { get; set; }

        public Precio() { }
    }
}
