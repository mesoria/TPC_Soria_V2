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
        private readonly NegocioEstablecimiento negocioEstablecimiento = new NegocioEstablecimiento();
        public List<Establecimiento> ListaEstablecimientos  = new List<Establecimiento>();
        public Persona persona = new Persona();
        public Usuario usuario = new Usuario();
        public Docente docente = new Docente();
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
                docente = (Docente)Application["Docente"];

                //List<Int64> listID;
                //listID = negocioEstablecimiento.GetIDsEstablecimientosWithPersona(persona.ID);
                //foreach (var item in listID)
                //{
                //    ListaEstablecimientos.Add(negocioEstablecimiento.GetEstablecimientoWithId(item));

                //}
                //foreach (var item in ListaEstablecimientos)
                //{
                //    negocioCurso.GetMyCursoWithEstablecimiento(ListaCursos, item.ID, persona.ID);
                //}
                ListaEstablecimientos = negocioEstablecimiento.GetCursoByEstablecimiento(docente.IdDocente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int64 GetIDCXE( Int64 IDE, Int64 IDC)
        {
            return negocioEstablecimiento.GetIDCursoByEstablecimiento(IDE, IDC);
        }
    }
}