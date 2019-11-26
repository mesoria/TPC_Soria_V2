using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Soria_v2.Curso
{
    public partial class Cursos : System.Web.UI.Page
    {
        public List<Cursos> cursos { get; set; }
        private readonly NegocioPersona negocioPersona = new NegocioPersona();
        public Persona maestra = new Persona();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                NegocioCurso negocioCurso = new NegocioCurso();
                //cursos = negocioCurso.ListarCursos();
                maestra = negocioPersona.GetPersona("36475321");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetApellido()
        {
            return maestra.Apellido;
        }
        public string GetName()
        {
            return maestra.Name;
        }
    }
}