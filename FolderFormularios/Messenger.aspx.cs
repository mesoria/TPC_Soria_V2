using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Soria_v2.FolderFormularios
{
    public partial class Messenger : System.Web.UI.Page
    {
        public NegocioUsuario negocioUsuario = new NegocioUsuario();
        public NegocioPersona negocioPersona = new NegocioPersona();
        Usuario usuario = new Usuario();
        Persona persona = new Persona();
        long IDCXE = 0;
        protected string MeEmail()
        {
            persona = (Persona)Application["Persona"];
            string email = negocioPersona.GetMeEmail(persona.ID);
            return email;
        }
        protected void BorrarCampos()
        {
            //txtPass.Text   = "";
            txtAsunto.Text = "";
            txtBody.Text   = "";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                usuario = (Usuario)Application["Usuario"];
                persona = (Persona)Application["Persona"];
                //IDCXE = Convert.ToInt64(Request.QueryString["IDCXE"]);
                IDCXE = Request.QueryString["IDCXE"] != null ? Convert.ToInt64(Request.QueryString["IDCXE"]) : 0;
                if (IDCXE == 0)
                {
                    IDCXE = (long)Session["IDCXE" + Session.SessionID];
                }
                List<Persona> ListaPersonas = new List<Persona>();
                if (!IsPostBack)
                {
                    if (usuario == null || usuario.ID == 0)
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                    if (IDCXE == 0)
                    {
                        //por si accede a la pagina con el link
                        Session["Error" + Session.SessionID] = "Ups, Aún no has seleccionado un Establecimiento.";
                        Response.Redirect("/frmLog.aspx", false);
                    }
                    txtAsunto.Text = Session["Asunto" + Session.SessionID] != null ? (string)Session["Asunto" + Session.SessionID] : "";
                    txtBody.Text   = Session["Cuerpo" + Session.SessionID] != null ? (string)Session["Cuerpo" + Session.SessionID] : "";
                    ListaPersonas = negocioPersona.GetContactoTutores(IDCXE);
                    dgvReceiver.DataSource = ListaPersonas;
                    dgvReceiver.DataBind();
                    btnVolver.Attributes.Add("onclick", "history.back(); return false;");
                }
            }
            catch (Exception ex)
            {
                Session["Error" + Session.SessionID] = ex;
                Response.Redirect("/frmLog.aspx");
            }
        }
        protected void DDLDestinatari_SelectedIndexChanged(object sender, EventArgs e)
        {
            NegocioPersona negocioPersona = new NegocioPersona();
            List<Persona> ListaPersonas = new List<Persona>();
            IDCXE = Request.QueryString["IDCXE"] != null ? Convert.ToInt64(Request.QueryString["IDCXE"]) : 0;
            if (DDLDestinatari.SelectedIndex == 0)
            {
                ListaPersonas = negocioPersona.GetContactoTutores(IDCXE);
            }
            else
            {
                ListaPersonas = negocioPersona.GetContactoEmpleados(IDCXE);
            }
            dgvReceiver.DataSource = ListaPersonas;
            dgvReceiver.DataBind();
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                NegocioAsistencia negocioAsistencia = new NegocioAsistencia();
                List<Persona> ListaPersonas;
                List<string> Destinatarios = new List<string>();

                if (Session["listaAlumnos"] != null)
                {
                    ListaPersonas = (List<Persona>)Session["listaAlumnos"];
                }
                else if (DDLDestinatari.SelectedIndex == 0)
                {
                    ListaPersonas = negocioPersona.GetContactoTutores(IDCXE);
                }
                else
                {
                    ListaPersonas = negocioPersona.GetContactoEmpleados(IDCXE);
                }
                foreach (GridViewRow dgvItem in this.dgvReceiver.Rows)
                {

                    CheckBox Sel = ((CheckBox)dgvReceiver.Rows[dgvItem.RowIndex].FindControl("cbxEnviar"));
                    if (Sel.Checked == true)
                    {
                        Destinatarios.Add(ListaPersonas[dgvItem.RowIndex].Email);
                    }
                }
                if (Destinatarios.Count() > 0)
                {
                    NegocioMessenger negocioMessenger = new NegocioMessenger();
                    //negocioMessenger.SenderEmail(txtMeEmail.Text, persona.Name, txtPass.Text, Destinatarios, txtAsunto.Text, txtBody.Text);
                    negocioMessenger.SenderEmail("aplicacion.miescuela@gmail.com", persona.Apellido + " " + persona.Name, "33294.frgp", Destinatarios, txtAsunto.Text, txtBody.Text);

                    if (negocioMessenger.Estado)
                    {
                        Session["Mensaje" + Session.SessionID] = "Mensaje enviado con exito.";
                        BorrarCampos();
                        Response.Redirect("~/FolderFormularios/LogMessengerExito.aspx");
                    }
                    else
                    {
                        Session["Mensaje" + Session.SessionID] = negocioMessenger.LogError;
                        Response.Redirect("~/FolderFormularios/LogMessenger.aspx");
                    }
                }
                else
                {
                    Session["Mensaje" + Session.SessionID] = "Debe agregar un destinatario";
                    Session["Asunto" + Session.SessionID]  = txtAsunto.Text;
                    Session["Cuerpo" + Session.SessionID]  = txtBody.Text;
                    Response.Redirect("~/FolderFormularios/LogMessenger.aspx");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}