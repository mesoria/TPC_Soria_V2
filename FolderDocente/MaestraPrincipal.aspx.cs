using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Soria_v2.FolderDocente
{
    public partial class MaestraPrincipal : System.Web.UI.Page
    {
        public List< Docente > ListDocentes { get; set; }
        private readonly NegocioPersona negocioPersona = new NegocioPersona();
        private readonly NegocioDocente negocioDocente = new NegocioDocente();
        public Persona persona = new Persona();
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
                    NegocioEstablecimiento negocioEstablecimiento = new NegocioEstablecimiento();
                    ListDocentes = negocioDocente.ListarDocentes();
                    persona = negocioPersona.GetPersonaWithId(usuario.ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public string GetApellido()
        //{
        //    return docente.Apellido;
        //}
        //public string GetName()
        //{
        //    return docente.Name;
        //}
    }
}