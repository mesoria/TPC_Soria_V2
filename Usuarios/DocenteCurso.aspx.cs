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
    public partial class DocenteCurso : System.Web.UI.Page
    {
        private readonly NegocioEstablecimiento negocioEstablecimiento = new NegocioEstablecimiento();
        private readonly NegocioAlumno          negocioAlumno          = new NegocioAlumno();

        public List<Alumno> alumnos { get; set; }
        public Establecimiento establecimiento = new Establecimiento();
        public Usuario usuario = new Usuario();
        public Persona persona = new Persona();
        public Docente docente = new Docente();
        public Alumno alumno;
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
                if (Request.QueryString["idCXE"] == null)
                {
                    //por si accede a la pagina con el link
                    Session["Error" + Session.SessionID] = "Ups, Aún no has seleccionado un Establecimiento.";
                    Response.Redirect("/frmLog.aspx", false);
                }
                cargarListMes();
                Int64 IDCXE = Convert.ToInt64( Request.QueryString["idCXE"]);
                establecimiento = negocioEstablecimiento.GetCursoByEstablecimientoWithID(IDCXE);
                alumnos = negocioAlumno.ListarAlumnosFromCurso(IDCXE);
                btnVolver.Attributes.Add("onclick", "history.back(); return false;");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void cargarListMes()
        {
           
        }
        public string mesActual()
        {
            string res;
            DateTime today = DateTime.Today;
            switch (today.Month)
            {
                case 12:
                    res = "Diciembre";
                    break;
                case 11:
                    res = "Noviembre";
                    break;
                case 10:
                    res = "Octubre";
                    break;
                case 9:
                    res = "Septiembre";
                    break;
                case 8:
                    res = "Agosto";
                    break;
                case 7:
                    res = "Julio";
                    break;
                case 6:
                    res = "Junio";
                    break;
                case 5:
                    res = "Mayo";
                    break;
                case 4:
                    res = "Abril";
                    break;
                case 3:
                    res = "Marzo";
                    break;
                case 2:
                    res = "Febrero";
                    break;
                default:
                    res = "Enero";
                    break;
            }
            return res;
        }
        public Int64 GetIDCXE(Int64 IDE, Int64 IDC)
        {
            return negocioEstablecimiento.GetIDCursoByEstablecimiento(IDE, IDC);
        }
    }
}