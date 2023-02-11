using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Equipo
    {
        public TipoEquipo Tipo { get; set; }
        public string MarcaModelo { get; set; }
        public string RAM { get; set; }
        public string PlacaMadre { get; set; }
        public string Microprocesador { get; set; }
        public string Almacenamiento { get; set; }
        public UnidadOptica UnidadOptica { get; set; }
        public string Alimentacion { get; set; }
        public string Adicionales { get; set; }
        public string NumSerie { get; set; }

        public Equipo() { }
    }
}
