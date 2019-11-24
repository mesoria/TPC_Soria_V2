using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Curso
    {
        public Int64 ID { get; set; }
        public Int64        IdEstablecimiento { get; set; }
        public Int64        IdMaestras { get; set; }
        public List<Int64>  IdAlumnos { get; set; }
        public Int64        IdAula { get; set; }
        public string       Turno { get; set; }
    }
}
