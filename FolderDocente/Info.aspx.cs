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
    public partial class Info : System.Web.UI.Page
    {
        private readonly NegocioDocente negocioDocente = new NegocioDocente();

        public Docente Aux = new Docente();
        public string ConvertToAMD(DateTime fecha)
        {
            string DMA = fecha.ToString().Split(' ')[0];
            string d = DMA.Split('/')[0];
            if (d.Length == 1)
            {
                d = '0' + d;
            }
            string m = DMA.Split('/')[1];
            if (m.Length == 1)
            {
                m = '0' + m;
            }
            return DMA.Split('/')[2] + '-' + m + '-' + d;
        }
        public string ConvertToDMA(DateTime fecha)
        {
            string AMD = fecha.ToString().Split(' ')[0];
            string d = AMD.Split('/')[2];
            if (d.Length == 1)
            {
                d = '0' + d;
            }
            string m = AMD.Split('/')[1];
            if (m.Length == 1)
            {
                m = '0' + m;
            }
            return AMD.Split('/')[0] + '-' + m + '-' + d;
        }
        private void Update()
        {
            txtNombre.Value     = Aux.Name;
            txtApellido.Value   = Aux.Apellido;
            txtDNI.Value        = Aux.DNI.ToString();
            txtEmail.Value      = Aux.Email;
            string AMD          = ConvertToAMD(Aux.Nacimiento);
            txtNacimiento.Value = AMD;
            txtCalle.Value      = Aux.Direccion.Calle;
            txtAltura.Value     = Aux.Direccion.Number;
            txtNivel.Value      = Aux.Nivel;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["idD"] == null)
                {
                    //por si accede a la pagina con el link
                    Session["Error" + Session.SessionID] = "Ups, Aún no has seleccionado un Docente.";
                    Response.Redirect("/frmLog.aspx", false);
                }
                Int64 idE = Convert.ToInt32(Request.QueryString["idD"]);
                Aux = negocioDocente.ListarDocentes().Find(P => P.IdDocente == idE);
                btnVolver.Attributes.Add("onclick", "history.back(); return false;");
                Update();
            }
            catch (Exception ex)
            {
                Session["Error" + Session.SessionID] = ex;
                Response.Redirect("/frmLog.aspx");
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Aux.ID != 0)
            {
                negocioDocente.Eliminar(Aux.ID);
                Response.Redirect("~/Docentes.aspx");
            }
        }
    }
}