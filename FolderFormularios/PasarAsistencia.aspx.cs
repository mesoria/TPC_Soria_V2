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
    public partial class PasarAsistencia : Page
    {
        private readonly NegocioPersona negocioPersona = new NegocioPersona();
        private readonly NegocioAlumno negocioAlumno  = new NegocioAlumno();
        private readonly NegocioEstablecimiento negocioEstablecimiento = new NegocioEstablecimiento();
        public NegocioAsistencia negocioAsistencia = new NegocioAsistencia();
        public List<Alumno> listaAlumnos = new List<Alumno>();
        Int64 IDCXE =0;
        readonly DateTime today = DateTime.Today;
        
        public Establecimiento establecimiento = new Establecimiento();
        public Persona persona = new Persona();
        public Usuario usuario = new Usuario();
        public Docente docente = new Docente();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                usuario = (Usuario)Application["Usuario"];
                persona = (Persona)Application["Persona"];
                docente = (Docente)Application["Docente"];
                //IDCXE = Convert.ToInt64(Request.QueryString["IDCXE"]);
                IDCXE = Request.QueryString["IDCXE"] != null ? Convert.ToInt64(Request.QueryString["IDCXE"]) : 0;
                establecimiento = negocioEstablecimiento.GetCursoByEstablecimientoWithID(IDCXE);
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
                    listaAlumnos = negocioAlumno.ListarAlumnosFromCurso(IDCXE);
                    //esto es lo que necesitamos para el repeater.
                    dgvAlumnos.DataSource = listaAlumnos;
                    dgvAlumnos.DataBind();
                    CargarAsistencias();
                    btnVolver.Attributes.Add("onclick", "history.back(); return false;");
                }
            }
            catch (Exception ex)
            {
                Session["Error" + Session.SessionID] = ex;
                Response.Redirect("/frmLog.aspx");
            }
        }

        protected void ValidarPersona_Click(object sender, EventArgs e)
        {
            try
            {
                NegocioPersona negocioPersona = new NegocioPersona();
                //var code = CodigoPersona.Value;
                //Persona voucher = negocioPersona.BuscarPersonas(code);
                //if (voucher.Code != null)
                //{
                //    Response.Redirect("/frmIncripcion.aspx");
                //}
                //else
                //{
                //    Response.Redirect("/Default.aspx");
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cbxArgument_CheckedChanged2(object sender, EventArgs e)
        {
            
        }
        protected void CargarAsistencias()
        {
            NegocioAsistencia negocioAsistencia = new NegocioAsistencia();
            List<Alumno> listaAlumnos;
            if (Session["listaAlumnos"] != null)
            {
                listaAlumnos = (List<Alumno>)Session["listaAlumnos"];
            }
            else
            {
                listaAlumnos = negocioAlumno.ListarAlumnosFromCurso(IDCXE);
            }
            foreach (GridViewRow dgvItem in this.dgvAlumnos.Rows)
            {
                CheckBox Sel = ((CheckBox)dgvAlumnos.Rows[dgvItem.RowIndex].FindControl("cbxPresente"));

                Sel.Checked = negocioAsistencia.CheckAsistencia(IDCXE, listaAlumnos[dgvItem.RowIndex].IdAlumno, today.Year, today.Month, today.Day);
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            NegocioAsistencia negocioAsistencia = new NegocioAsistencia();
            List<Alumno> listaAlumnos;
            if (Session["listaAlumnos"] != null)
            {
                listaAlumnos = (List<Alumno>)Session["listaAlumnos"];
            }
            else
            {
                listaAlumnos = negocioAlumno.ListarAlumnosFromCurso(IDCXE);
            }
            foreach (GridViewRow dgvItem in this.dgvAlumnos.Rows)
            {
                CheckBox Sel = ((CheckBox)dgvAlumnos.Rows[dgvItem.RowIndex].FindControl("cbxPresente"));
                if (Sel.Checked == true)
                {
                    negocioAsistencia.Agregar(listaAlumnos[dgvItem.RowIndex].IdAlumno, IDCXE);
                }
                else
                {
                    negocioAsistencia.Eliminar(listaAlumnos[dgvItem.RowIndex].IdAlumno, IDCXE);
                }
            }
            Response.Redirect("~/Usuarios/DocentePrincipal.aspx", false);
        }
    }
}