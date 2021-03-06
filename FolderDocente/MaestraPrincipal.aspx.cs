﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Soria_v2.FolderDocente
{
    public partial class MaestraPrincipal : System.Web.UI.Page
    {
        public List<Establecimiento> ListaEstablecimiento { get; set; }
        private readonly NegocioPersona negocioPersona = new NegocioPersona();
        //private readonly NegocioDocente negocioDocente = new NegocioDocente();
        private readonly NegocioEstablecimiento negocioEstablecimiento = new NegocioEstablecimiento();
        public Persona persona = new Persona();
        public Usuario usuario = new Usuario();
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

                List<Int64> listID = negocioEstablecimiento.GetIDsEstablecimientosWithPersona(persona.ID);
                foreach (var item in listID)
                {
                    ListaEstablecimiento.Add(negocioEstablecimiento.GetEstablecimientoWithId(item));
                }
                //persona = negocioPersona.GetPersonaWithId(usuario.ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public string GetApellido()
        //{
        //    return docente.Apellido;
        //}
        //public string GetName()
        //{
        //    return docente.Name;
        //}
    }
}