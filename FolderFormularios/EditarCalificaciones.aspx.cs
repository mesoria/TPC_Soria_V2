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
    public partial class EditarCalificaciones : System.Web.UI.Page
    {
        private readonly NegocioAlumno negocioAlumno = new NegocioAlumno();
        private readonly NegocioCalificaciones negocioCalificaciones = new NegocioCalificaciones();
        public List<Alumno> ListaAlumnos = new List<Alumno>();
        Int64 IDCXE = 0;
        readonly DateTime today = DateTime.Today;
        public Persona persona = new Persona();
        public Usuario usuario = new Usuario();
        public Docente docente = new Docente();
        public Establecimiento establecimiento = new Establecimiento();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                usuario = (Usuario)Application["Usuario"];
                persona = (Persona)Application["Persona"];
                docente = (Docente)Application["Docente"];
                //IDCXE = Convert.ToInt64(Request.QueryString["IDCXE"]);
                IDCXE = Request.QueryString["IDCXE"] != null ? Convert.ToInt64(Request.QueryString["IDCXE"]) : 0;
                IDCXE = Request.QueryString["IDCXE"] != null ? Convert.ToInt64(Request.QueryString["IDCXE"]) : 0;
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
                    ListaAlumnos = negocioAlumno.ListarAlumnosByte(IDCXE);
                    Calificaciones aux = new Calificaciones();
                    foreach (var item in ListaAlumnos)
                    {
                        item.Calificaciones = negocioCalificaciones.GetCalificacion(IDCXE, item.IdAlumno, (short)today.Year);
                    }
                    btnVolver.Attributes.Add("onclick", "history.back(); return false;");
                }
            }
            catch (Exception ex)
            {
                Session["Error" + Session.SessionID] = ex;
                Response.Redirect("/frmLog.aspx");
            }
        }
    }
}