using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ServicioPorCliente
    {
        public Cliente Cliente { get; set; }
        public TipoServicio TipoServicio { get; set; }
        public int TotalServiciosRealizados { get; set; }
        public decimal GananciaTotal { get; set; }

        public ServicioPorCliente()
        {
            TotalServiciosRealizados = 0;
            GananciaTotal = 0;
        }
    }
}
