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
    public partial class Docentes : System.Web.UI.Page
    {
        public List<Docente> ListaDocentes { get; set; }
        private readonly NegocioDocente negocioDocente = new NegocioDocente();
        public Docente docente = new Docente();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string log = (string)Session["Aviso" + Session.SessionID];
                if (log != null)
                {
                    Response.Write("<script>alert(log)</script>");
                }
                NegocioEstablecimiento negocioEstablecimiento = new NegocioEstablecimiento();
                ListaDocentes = negocioDocente.ListarDocentes();
                //maestra = negocioPersona.GetPersona("36475321");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public string GetApellido()
        //{
        //    return maestra.Apellido;
        //}
        //public string GetName()
        //{
        //    return maestra.Name;
        //}
    }
}