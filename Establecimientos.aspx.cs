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
    public partial class Establecimientos : Page
    {
        public List<Establecimiento> ListEstablecimientos { get; set; }
        //private readonly NegocioPersona negocioPersona = new NegocioPersona();
        public Persona maestra = new Persona();
        public Persona persona = new Persona();
        public Usuario usuario = new Usuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                usuario = (Usuario)Application["Usuario"];
                if (!IsPostBack)
                {
                    if(usuario == null || usuario.ID == 0)
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                }
                //persona = negocioPersona.GetPersonaWithId(usuario.ID);
                persona = (Persona)Application["Persona"];
                NegocioEstablecimiento negocioEstablecimiento = new NegocioEstablecimiento();
                ListEstablecimientos = negocioEstablecimiento.ListarEstablecimiento();
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