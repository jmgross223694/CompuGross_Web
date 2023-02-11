using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ItemPresupuesto
    {
        public string Codigo { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        public decimal Subtotal { get; set; }

        public ItemPresupuesto() { }
    }
}
