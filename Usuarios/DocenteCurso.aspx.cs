﻿using System;
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
        //private readonly NegocioEstablecimiento negocioEstablecimiento = new NegocioEstablecimiento();
        //private readonly NegocioCurso           negocioCurso           = new NegocioCurso();
        //private readonly NegocioPersona         negocioPersona         = new NegocioPersona();
        private readonly NegocioAlumno          negocioAlumno          = new NegocioAlumno();

        public List<Alumno> alumnos { get; set; }
        public Establecimiento establecimiento = new Establecimiento();
        public Usuario usuario = new Usuario();
        public Persona persona = new Persona();
        public Docente docente = new Docente();
        public Curso   curso   = new Curso();
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
                
                establecimiento.Name = "Escuela";
                curso.Name = "1° A";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}