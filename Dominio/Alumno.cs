﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Alumno : Persona
    {
        public Int64 IdAlumno { get; set; }
        public Persona Tutor { get; set; }
    }
}
