﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Soria_v2.FolderFormularios
{
    public partial class MostrarAsistencias : System.Web.UI.Page
    {
        private readonly NegocioPersona negocioPersona = new NegocioPersona();
        private readonly NegocioAlumno negocioAlumno = new NegocioAlumno();
        private readonly NegocioEstablecimiento negocioEstablecimiento = new NegocioEstablecimiento();
        public NegocioAsistencia negocioAsistencia = new NegocioAsistencia();
        public List<Alumno> ListaAlumnos = new List<Alumno>();
        public long IDCXE{   get; set;}
        readonly DateTime today = DateTime.Today;

        public Establecimiento establecimiento = new Establecimiento();
        public Persona persona = new Persona();
        public Usuario usuario = new Usuario();
        public Docente docente = new Docente();
        public void Page_Load(object sender, EventArgs e)
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

                IDCXE = Convert.ToInt64(Request.QueryString["IDCXE"]);
                //persona = negocioPersona.GetPersonaWithId(usuario.ID);
                if (Request.QueryString["IDCXE"] == null)
                {
                    //por si accede a la pagina con el link
                    Session["Error" + Session.SessionID] = "Ups, Aún no has seleccionado un Establecimiento.";
                    Response.Redirect("/frmLog.aspx", false);
                }
                //Int64 IDCXE = (Int64)Session["IDCXE" + Session.SessionID];
                //int mes = (int)Session["Mes" + Session.SessionID];
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
        public string TraductionDayWeek(string day)
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
            DateTime week = Convert.ToDateTime("01/" + month + "/" + year);

            return TraductionDayWeek(week.DayOfWeek.ToString());
        }
        public string DiasDelMes(int month)
        {
            if (month == 0)
            {
                month = today.Month;
            }
            string wk = DateOne(today.Year, month);

            int cant = DateTime.DaysInMonth(today.Year, month);
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
                }

                d++;
            }
            return strT;
        }       
        
        public string AsistenciasDelMes(Alumno item, int month, long IDCXE)
        {
            if (month == 0)
            {
                month = today.Month;
            }
            int d = 1;
            string wk = DateOne(today.Year, month);
            int cant = DateTime.DaysInMonth(today.Year, month);
            string strT = "";
            while (d <= cant)
            {
                switch (wk)
                {
                    case "Lu":
                        {
                            strT += "<th><div class=\"custom-control custom-checkbox\"><input type =\"checkbox\" class=\"custom-control-input\" id=\"" + d + "\" " + Asistio(IDCXE, item.IdAlumno, month,d) + " disabled=\"disabled\">" +
                                    "<label class=\"custom-control-label\" for=\"" + d + "\"></label></div></th>";
                            wk = "Ma";
                            break;
                        }
                    case "Ma":
                        {
                            strT += "<th><div class=\"custom-control custom-checkbox\"><input type =\"checkbox\" class=\"custom-control-input\" id=\"" + d + "\" " + Asistio(IDCXE, item.IdAlumno, month, d) + " disabled=\"disabled\">" +
                                    "<label class=\"custom-control-label\" for=\"" + d + "\"></label></div></th>";
                            wk = "Mi";
                            break;
                        }
                    case "Mi":
                        {
                            strT += "<th><div class=\"custom-control custom-checkbox\"><input type =\"checkbox\" class=\"custom-control-input\" id=\"" + d + "\" " + Asistio(IDCXE, item.IdAlumno, month, d) + " disabled=\"disabled\">" +
                                    "<label class=\"custom-control-label\" for=\"" + d + "\"></label></div></th>";
                            wk = "Ju";
                            break;
                        }
                    case "Ju":
                        {
                            strT += "<th><div class=\"custom-control custom-checkbox\"><input type =\"checkbox\" class=\"custom-control-input\" id=\"" + d + "\" " + Asistio(IDCXE, item.IdAlumno, month, d) + " disabled=\"disabled\">" +
                                    "<label class=\"custom-control-label\" for=\"" + d + "\"></label></div></th>";
                            wk = "Vi";
                            break;
                        }
                    case "Vi":
                        {
                            strT += "<th><div class=\"custom-control custom-checkbox\"><input type =\"checkbox\" class=\"custom-control-input\" id=\"" + d + "\" " + Asistio(IDCXE, item.IdAlumno, month,d) + " disabled=\"disabled\">" +
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
                }
                d++;
            }
            return strT;
        }

        public string Asistio(long IDCXE, Int64 ID, int month, int dia)
        {
            if (negocioAsistencia.CheckAsistencia(IDCXE, ID, today.Year, month, dia))
            {
                return "checked";
            }
            return "";
        }

        public string MesActual()
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
    }
}