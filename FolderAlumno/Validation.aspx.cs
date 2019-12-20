using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace TPC_Soria_v2.FolderAlumno
{
    public partial class Validation : System.Web.UI.Page
    {
        public Alumno alumno = new Alumno();
        private bool IsEmpty(string s)
        {
            return s == null || s == string.Empty;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            alumno = (Alumno)Session["Alumno" + Session.SessionID];
            if ( alumno != null && !IsEmpty(alumno.DNI) )
            {
                lblDNI.Text = alumno.DNI;
                lblApellido.Text = alumno.Apellido + ", "+ alumno.Name;
            }
            else
            {
                Response.Redirect("~/Usuarios/DocentePrincipal.aspx");
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Session["Alumno" + Session.SessionID] = alumno;
            Response.Redirect("~/FolderAlumno/New.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["Alumno" + Session.SessionID] = null;
            Session["Backup" + Session.SessionID] = (Alumno)Session["Backup" + Session.SessionID];
            Response.Redirect("~/FolderAlumno/New.aspx");
        }
    }
}