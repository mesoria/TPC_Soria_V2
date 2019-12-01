using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Establecimiento
    {
        public Int64        ID { get; set; }
        public string       Name { get; set; }
        public int          Number { get; set; }
        public string       Nivel { get; set; }
        public Direccion    Direccion { get; set; }
        public Directora    Directora { get; set; }
        public Curso        Curso { get; set; }
        //public List<Int64>  IdMaestras { get; set; }
    }
}
