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
    public partial class MostrarCalificaciones2 : System.Web.UI.Page
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
        public void CargarGrilla()
        {
            ListaAlumnos = negocioAlumno.ListarAlumnosByte(IDCXE);
            foreach (var item in ListaAlumnos)
            {
                item.Calificaciones = negocioCalificaciones.GetCalificacion(IDCXE, item.IdAlumno, (short)today.Year);
            }
            dgvAlumnos.DataSource = ListaAlumnos;
            dgvAlumnos.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                usuario = (Usuario)Application["Usuario"];
                persona = (Persona)Application["Persona"];
                docente = (Docente)Application["Docente"];
                //IDCXE = Convert.ToInt64(Request.QueryString["IDCXE"]);
                IDCXE = Request.QueryString["IDCXE"] != null ? Convert.ToInt64(Request.QueryString["IDCXE"]) : 0;
                Session["IDCXE " + Session.SessionID] = IDCXE;
                establecimiento = Session["establecimiento" + Session.SessionID] != null ? (Establecimiento)Session["establecimiento" + Session.SessionID] : establecimiento;
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
                    CargarGrilla();
                    btnVolver.Attributes.Add("onclick", "history.back(); return false;");
                }
            }
            catch (Exception ex)
            {
                Session["Error" + Session.SessionID] = ex;
                Response.Redirect("/frmLog.aspx");
            }
        }

        protected void dgvAlumnos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgvAlumnos.EditIndex = -1;
            CargarGrilla();
        }

        protected void dgvAlumnos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dgvAlumnos.EditIndex = e.NewEditIndex;
            CargarGrilla();
            GridViewRow fila = dgvAlumnos.Rows[dgvAlumnos.EditIndex];
            Calificaciones calificaciones = new Calificaciones();
            Letras Letras = new Letras();
            calificaciones.Letras = Letras;
            long IDA = Convert.ToInt64(dgvAlumnos.DataKeys[dgvAlumnos.EditIndex].Values[0]);
            calificaciones = negocioCalificaciones.GetCalificacion(IDCXE, IDA, (short)today.Year);
            (fila.FindControl("txtNota1") as DropDownList).SelectedValue = calificaciones.Letras.Letra1;
            (fila.FindControl("txtNota2") as DropDownList).SelectedValue = calificaciones.Letras.Letra2;
            (fila.FindControl("txtNota3") as DropDownList).SelectedValue = calificaciones.Letras.Letra3;

        }

        protected void dgvAlumnos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow fila = dgvAlumnos.Rows[e.RowIndex];
                long IDA = Convert.ToInt64(dgvAlumnos.DataKeys[e.RowIndex].Values[0]);
                string nota1 = (fila.FindControl("txtNota1") as DropDownList).Text;
                string nota2 = (fila.FindControl("txtNota2") as DropDownList).Text;
                string nota3 = (fila.FindControl("txtNota3") as DropDownList).Text;

                Alumno a = negocioAlumno.GetAlumnoWithId(IDA);
                negocioCalificaciones.ModificarCalificaciones(IDCXE, a.IdAlumno, Convert.ToInt16(today.Year), nota1, nota2, nota3);

                dgvAlumnos.EditIndex = -1;
                CargarGrilla();
            }
            catch (Exception)
            {
                Response.Redirect("~/FolderFormularios/LogMostrarCalificaciones.aspx");
            }

        }
    }
}