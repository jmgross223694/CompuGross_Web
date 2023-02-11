using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Servicio
    {
        public long ID { get; set; }
        public TipoServicio TipoServicio { get; set; }
        public Cliente Cliente { get; set; }
        public Equipo Equipo { get; set; }
        public string FechaRecepcion { get; set; }
        public string FechaDevolucion { get; set; }
        public string Descripcion { get; set; }
        public string CostoRepuestos { get; set; }
        public string CostoTerceros { get; set; }
        public string Honorarios { get; set; }
        public string CostoTotal { get; set; }
        public bool Estado { get; set; }

        public Servicio() { }
    }
}
