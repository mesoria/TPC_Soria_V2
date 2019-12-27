using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Calificaciones
    {
        public long     Id { get; set; }
        public sbyte    Trimestre { get; set; }
        public bool     Primario { get; set; }
        public Notas    Notas { get; set; }
        public Letras   Letras { get; set; }
        public string   Observaciones { get; set; }
    }
}
