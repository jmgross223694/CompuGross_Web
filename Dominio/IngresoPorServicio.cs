using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class IngresoPorServicio
    {
        public int Cantidad { get; set; }
        public TipoServicio TipoServicio { get; set; }
        public decimal Ganancia { get; set; }
    }
}
