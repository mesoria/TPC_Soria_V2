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
    public partial class MaestraPrincipal : System.Web.UI.Page
    {
        public List< Establecimiento > Escuelas { get; set; }
        private readonly NegocioPersona negocioPersona = new NegocioPersona();
        public Persona maestra = new Persona();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                NegocioEstablecimiento negocioEstablecimiento = new NegocioEstablecimiento();
                Escuelas = negocioEstablecimiento.ListarEstablecimiento();
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