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
        public NegocioPersona NegocioPersona = new NegocioPersona();
        public Persona maestra = new Persona();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["idM"] == null)
                {
                    //por si accede a la pagina con el link
                    Session["Error" + Session.SessionID] = "Ups, ¿Aún no eras maestra?";
                    Response.Redirect("MaestraPrincipal.aspx", false);
                }
                Int64 maestraId = Convert.ToInt32(Request.QueryString["idM"]);
                maestra = NegocioPersona.GetPersonaWithId(maestraId);
                
                //maestra = NegocioPersona.ListarPersonas().Find(J => J.ID == maestraId);
                //grid.DataSource = NegocioVoucher.ListarVouchers();
                //grid.DataBind();
            }
            catch (Exception ex)
            {
                Session["Error" + Session.SessionID] = ex;
                Response.Redirect("/frmLog.aspx");
            }
        }
    }
}