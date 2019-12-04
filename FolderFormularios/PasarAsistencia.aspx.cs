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
        public List<Alumno> ListaAlumnos = new List<Alumno>();
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
                if (!IsPostBack)
                {
                    if (usuario == null || usuario.ID == 0)
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                }
                persona = (Persona)Application["Persona"];
                docente = (Docente)Application["Docente"];
                persona = negocioPersona.GetPersonaWithId(usuario.ID);
                if (Request.QueryString["idCXE"] == null)
                {
                    //por si accede a la pagina con el link
                    Session["Error" + Session.SessionID] = "Ups, Aún no has seleccionado un Establecimiento.";
                    Response.Redirect("/frmLog.aspx", false);
                }
                Int64 IDCXE = Convert.ToInt64(Request.QueryString["idCXE"]);
                establecimiento = negocioEstablecimiento.GetCursoByEstablecimientoWithID(IDCXE);
                ListaAlumnos = negocioAlumno.ListarAlumnosFromCurso(IDCXE);
                btnVolver.Attributes.Add("onclick", "history.back(); return false;");
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
        public string TraductionDayWeek( string day)
        {
            switch (day)
            {
                case "Monday":
                    day = "Lu";
                    break;
                case "Tuesday":
                    day = "Ma";
                    break;
                case "Wednesday":
                    day = "Mi";
                    break;
                case "Thursday":
                    day = "Ju";
                    break;
                case "Friday":
                    day = "Vi";
                    break;
                case "Saturday":
                    day = "Sa";
                    break;
                case "Sunday":
                    day = "Do";
                    break;
                default:
                    break;
            }
            return day;
        }
        public string DateOne(int year, int month)
        {
            DateTime week = Convert.ToDateTime("01/"+month+"/"+year);

            return TraductionDayWeek( week.DayOfWeek.ToString());
        }
        public string Dias()
        {            
            string wk = DateOne( today.Year, today.Month);
            
            int cant = DateTime.DaysInMonth(today.Year, today.Month);
            int d = 1;
            string strT = "";
            while (d <= cant)
            {
                switch (wk)
                {
                    case "Lu":
                        strT += "<th style=\"width: auto; background-color: #77ff77\">" + wk + " " + d + " </ th > ";
                        wk = "Ma";
                        break;
                    case "Ma":
                        strT += "<th style=\"width: auto; background-color: #77ff77\"> " + wk + " " + d + " </ th > ";
                        wk = "Mi";
                        break;
                    case "Mi":
                        strT += "<th style=\"width: auto; background-color: #77ff77\"> " + wk + " " + d + " </ th > ";
                        wk = "Ju";
                        break;
                    case "Ju":
                        strT += "<th style=\"width: auto; background-color: #77ff77\"> " + wk + " " + d + " </ th > ";
                        wk = "Vi";
                        break;
                    case "Vi":
                        strT += "<th style=\"width: auto; background-color: #77ff77\"> " + wk + " " + d + " </ th > ";
                        wk = "Lu";
                        d += 2;
                        break;
                    case "Sa":
                        wk = "Lu";
                        d++;
                        break;
                    case "Do":
                        wk = "Lu";
                        break;
                    default:
                        break;
                }

                d++;
            }
            return strT;
        }
        public string Asistencia( Persona persona)
        {
            return "<th><div class=\"custom-control custom-checkbox\"><input type =\"checkbox\" class=\"custom-control-input\" id=\"" + 1 + " \"disabled=\"disabled\" "+ Asistio(1, persona.ID)+">" +
                                    "<label class=\"custom-control-label\" for=\"" + 1 + "\"></label></div></th>";
        }
        public string Asistencias( Persona item)
        {
            int d = 1;
            string wk = DateOne(today.Year, today.Month);
            string strT = "";
            while ( d < today.Day )
            {
                switch (wk)
                {
                    case "Lu":
                        {
                            strT += "<th><div class=\"custom-control custom-checkbox\"><input type =\"checkbox\" class=\"custom-control-input\" id=\"" + d + "\" " + Asistio(item.ID, d) + " disabled=\"disabled\">" +
                                    "<label class=\"custom-control-label\" for=\"" + d + "\"></label></div></th>";
                            wk = "Ma";
                            break;
                        }
                    case "Ma":
                        {
                            strT += "<th><div class=\"custom-control custom-checkbox\"><input type =\"checkbox\" class=\"custom-control-input\" id=\"" + d + "\" " + Asistio(item.ID, d) + " disabled=\"disabled\">" +
                                    "<label class=\"custom-control-label\" for=\"" + d + "\"></label></div></th>";
                            wk = "Mi";
                            break;
                        }
                    case "Mi":
                        {
                            strT += "<th><div class=\"custom-control custom-checkbox\"><input type =\"checkbox\" class=\"custom-control-input\" id=\"" + d + "\" " + Asistio(item.ID, d) + " disabled=\"disabled\">" +
                                    "<label class=\"custom-control-label\" for=\"" + d + "\"></label></div></th>";
                            wk = "Ju";
                            break;
                        }
                    case "Ju":
                        {
                            strT += "<th><div class=\"custom-control custom-checkbox\"><input type =\"checkbox\" class=\"custom-control-input\" id=\"" + d + "\" " + Asistio(item.ID, d) + " disabled=\"disabled\">" +
                                    "<label class=\"custom-control-label\" for=\"" + d + "\"></label></div></th>";
                            wk = "Vi";
                            break;
                        }
                    case "Vi":
                        {
                            strT += "<th><div class=\"custom-control custom-checkbox\"><input type =\"checkbox\" class=\"custom-control-input\" id=\"" + d + "\" " + Asistio(d, item.ID) + " disabled=\"disabled\">" +
                                    "<label class=\"custom-control-label\" for=\"" + d + "\" ></label></div></th>";
                            wk = "Lu";
                            d += 2;
                            break;
                        }
                    case "Sa":
                        wk = "Lu";
                        d++;
                        break;
                    case "Do":
                        wk = "Lu";
                        break;
                    default:
                        break;
                }
                d++;
            }
            return strT;
        }
        public string TablaPresentes()
        {
            string Tabla = "";
            Tabla += "<td>+" +
                        "<div class=\"custom-control custom-checkbox\">" +
                                "<input type = \"checkbox\" class=\"custom-control-input\" id=<%=item.ID %> checked>" +
                                "<label class=\"custom-control-label\" for=<%=item.ID %>></label>" +
                            "</div>" +
                     "</td>";
            return Tabla;
        }

        public string Asistio(Int64 ID, int dia)
        {
            if (negocioAsistencia.CheckAsistencia(ID, today.Year, today.Month, dia) )
            {
                return "checked";
            }
            return "";
        }
        public string Asistio(Int64 dia, Int64 ID)
        {
            string str = "";

            List<Asistencia> lista = negocioAsistencia.ListarAsistenciasActual(ID);
            foreach (Asistencia item in lista)
            {
                if (item.Fecha.Day == dia)
                {
                    return "checked";
                }
            }
            return str;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            
        }
    }
}