using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Soria_v2.FolderAlumno
{
    public partial class Validation : System.Web.UI.Page
    {
        public Alumno alumno = new Alumno();
        protected void Page_Load(object sender, EventArgs e)
        {
            alumno = (Alumno)Session["Alumno" + Session.SessionID];
            lblDNI.Text = alumno.DNI;
            lblApellido.Text = alumno.Apellido + ", "+ alumno.Name;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Session["Alumno" + Session.SessionID] = alumno;
            Response.Redirect("~/FolderAlumno/New.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["Alumno" + Session.SessionID] = null;
            Response.Redirect("~/FolderAlumno/New.aspx");
        }
    }
}