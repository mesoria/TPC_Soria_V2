using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Soria_v2
{
    public partial class Cursos : System.Web.UI.Page
    {
        private readonly NegocioEstablecimiento negocioEstablecimiento = new NegocioEstablecimiento();
        private readonly NegocioPersona negocioPersona = new NegocioPersona();
        private readonly NegocioDocente negocioDocente = new NegocioDocente();
        public Establecimiento Establecimiento = new Establecimiento();
        public Persona persona = new Persona();
        public Docente docente = new Docente();
        public Usuario usuario = new Usuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                usuario = (Usuario)Session["Usuario"];
                if (!IsPostBack)
                {
                    if (usuario == null)
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                }
                persona = negocioPersona.GetPersonaWithId(usuario.ID);
                docente = negocioDocente.GetDocenteWithDNI(persona.DNI);
                Session["Usuario"] = usuario;
                Session["Persona"] = persona;
                Session["Docente"] = docente;
                Establecimiento = negocioEstablecimiento.GetMyEstablecimiento(persona);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}