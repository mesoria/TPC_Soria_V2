using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Docente : Persona
    {
        public Int64    IdDocente { get; set; }
        public string   Nivel { get; set; }
    }
}
