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
    public partial class _Default : Page
    {
        private readonly NegocioPersona negocioPersona = new NegocioPersona();
        public Persona persona = new Persona();
        public Usuario usuario = new Usuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //usuario = (Usuario)Session["Usuario"];
                usuario = (Usuario)Application["Usuario"];
                Session["Usuario"] = usuario;
                if (!IsPostBack)
                {
                    if (usuario == null)
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                }
                persona = (Persona)Application["Persona"];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}