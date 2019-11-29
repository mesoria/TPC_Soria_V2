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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnIniciar_Click(object sender, EventArgs e)
        {
            try
            {
                NegocioLogin negocioLogin = new NegocioLogin();
                NegocioPersona negocioPersona = new NegocioPersona();
                NegocioDocente negocioDocente = new NegocioDocente();
                if (negocioLogin.Autenticar(txtUsuario.Text, txtContraseña.Text) == true)
                {
                    Usuario usuario = negocioLogin.GetUsuario(txtUsuario.Text, txtContraseña.Text);
                    //DataTable tblUsuario = NegocioLogin.prConsultaUsuario(usuario, contraseña);
                    //NegocioLogin.Security(Convert.ToInt32(tblUsuario.Rows[0]["UsuarioID"]), usuario, DateTime.Now, Request.ServerVariables["REMOTE_ADDR"]);
                    Application["Usuario"] = usuario;
                    Application["Persona"] = negocioPersona.GetPersonaWithId(usuario.ID);
                    Application["Docente"] = negocioDocente.GetDocenteWithDNI(negocioPersona.GetPersonaWithId(usuario.ID).DNI);
                    //Manda a la principal en caso de ser correcto el login
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    //Mensaje de error en caso de no ser usuario registrado
                    lblMensaje.Text = "Usuario/Contraseña incorrecta verifique por favor.";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}