using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Curso
    {
        public Int64        ID { get; set; }
        public string       Nombre { get; set; }
        public Int64        IdEstablecimiento { get; set; }
        public Docente      Docente { get; set; }
        public List<Alumno> Alumnos { get; set; }
    }
}
