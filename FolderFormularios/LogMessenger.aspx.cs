using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_Soria_v2.FolderFormularios
{
    public partial class LogMessenger : System.Web.UI.Page
    {
        public long IDCXE { get; set; }
        public string Mensaje { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            IDCXE = (long)Session["IDCXE" + Session.SessionID];
            Mensaje = (string)Session["Mensaje" + Session.SessionID];
            lblMensaje.Text = Mensaje;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FolderFormularios/Messenger.aspx", false);
        }
    }
}