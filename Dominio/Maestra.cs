using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Maestra : Persona
    {
        public Int64    ID { get; set; }
        public Aula     Aula { get; set; }
        public Usuario  Usuario { get; set; }
    }
}
