using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Presupuesto
    {
        public string Fecha { get; set; }
        public Cliente Cliente { get; set; }
        public List<ItemPresupuesto> ListaItems { get; set; }
        public decimal TotalItems { get; set; }
        public decimal TotalMonto { get; set; }

        public Presupuesto() { }
    }
}
