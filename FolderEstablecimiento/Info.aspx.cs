﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Soria_v2.FolderEstablecimiento
{
    public partial class Info : System.Web.UI.Page
    {
        private readonly NegocioEstablecimiento negocioEstablecimiento = new NegocioEstablecimiento();

        public Establecimiento Aux = new Establecimiento();
        public Usuario usuario = new Usuario();
        public Persona persona = new Persona();
        public Docente docente = new Docente();
        private void Update()
        {
            txtNombre.Value = Aux.Name;
            txtNivel.Value  = Aux.Nivel;
            txtNumero.Value = Aux.Number.ToString();
            txtCalle.Value  = Aux.Direccion.Calle;
            txtAltura.Value = Aux.Direccion.Number;
        }
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
                if (Request.QueryString["idE"] == null)
                {
                    //por si accede a la pagina con el link
                    Session["Error" + Session.SessionID] = "Ups, Aún no has seleccionado un Establecimiento.";
                    Response.Redirect("/frmLog.aspx", false);
                }
                Int64 idE = Convert.ToInt32(Request.QueryString["idE"]);
                Aux = negocioEstablecimiento.ListarEstablecimiento().Find(P => P.ID == idE);
                btnVolver.Attributes.Add("onclick", "history.back(); return false;");
                Update();
            }
            catch (Exception ex)
            {
                Session["Error" + Session.SessionID] = ex;
                Response.Redirect("/frmLog.aspx");
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (Aux.ID != 0)
            {
                negocioEstablecimiento.Eliminar(Aux.ID);
                Response.Redirect("~/Establecimientos.aspx");
            }
        }
    }
}