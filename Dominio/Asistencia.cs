using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominio
{
    public class Asistencia
    {
        public Int64    Id { get; set; }
        public Int64    IdAlumno { get; set; }
        public DateTime Fecha { get; set; }
    }
}