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
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                usuario = (Usuario)Application["Usuario"];
                persona = (Persona)Application["Persona"];
                //IDCXE = Convert.ToInt64(Request.QueryString["IDCXE"]);
                IDCXE = Request.QueryString["IDCXE"] != null ? Convert.ToInt64(Request.QueryString["IDCXE"]) : 0;
                //establecimiento = negocioEstablecimiento.GetCursoByEstablecimientoWithID(IDCXE);
                List<Persona> ListaPersonas = new List<Persona>();
                if (!IsPostBack)
                {
                    if (usuario == null || usuario.ID == 0)
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                    //persona = negocioPersona.GetPersonaWithId(usuario.ID);
                    if (IDCXE == 0)
                    {
                        //por si accede a la pagina con el link
                        Session["Error" + Session.SessionID] = "Ups, Aún no has seleccionado un Establecimiento.";
                        Response.Redirect("/frmLog.aspx", false);
                    }
                    txtMeEmail.Text = negocioPersona.GetMeEmail(persona.ID);
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
            NegocioMessenger negocioMessenger = new NegocioMessenger();
            negocioMessenger.SenderEmail(txtMeEmail.Text, persona.Name, txtPass.Text, Destinatarios, txtAsunto.Text, txtBody.Text);
            if ( negocioMessenger.Estado)
            {
                Response.Write("Mensaje enviado.");
            }
            else
            {
                Response.Write(negocioMessenger.LogError);
            }
            //Response.Redirect("~/Usuarios/DocentePrincipal.aspx", false);
        }
    }
}