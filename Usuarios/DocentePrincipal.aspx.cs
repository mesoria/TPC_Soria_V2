using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Soria_v2.Usuarios
{
    public partial class DocentePrincipal : System.Web.UI.Page
    {
        public List<Establecimiento> ListaEstablecimientos = new List<Establecimiento>();
        private readonly NegocioEstablecimiento negocioEstablecimiento = new NegocioEstablecimiento();
        private readonly NegocioDocente negocioDocente = new NegocioDocente();
        public Persona persona = new Persona();
        public Usuario usuario = new Usuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                usuario = (Usuario)Application["Usuario"];
                if (!IsPostBack)
                {
                    if (usuario == null || usuario.ID == 0)
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                }
                persona = (Persona)Application["Persona"];
                List<Int64> listID;
                listID = negocioEstablecimiento.GetIDsEstablecimientosWithPersona(persona.ID);
                foreach (var item in listID)
                {
                    ListaEstablecimientos.Add(negocioEstablecimiento.GetEstablecimientoWithId(item));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public string GetApellido()
        //{
        //    return docente.Apellido;
        //}
        //public string GetName()
        //{
        //    return docente.Name;
        //}
    }
}