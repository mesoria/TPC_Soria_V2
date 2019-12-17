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
    public partial class MostrarCalificaciones : System.Web.UI.Page
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
        }

        protected void dgvAlumnos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow fila = dgvAlumnos.Rows[e.RowIndex];
            long  IDA   = Convert.ToInt64(dgvAlumnos.DataKeys[e.RowIndex].Values[0]);
            byte nota1 = Convert.ToByte( (fila.FindControl("txtNota1") as TextBox).Text);
            byte nota2 = Convert.ToByte( (fila.FindControl("txtNota2") as TextBox).Text);
            byte nota3 = Convert.ToByte( (fila.FindControl("txtNota3") as TextBox).Text);

            Alumno a = negocioAlumno.GetAlumnoWithId(IDA);
            negocioCalificaciones.ModificarNotas(IDCXE, a.IdAlumno, Convert.ToInt16(today.Year), nota1, nota2, nota3);

            dgvAlumnos.EditIndex = -1;
            CargarGrilla();
        }
    }
}