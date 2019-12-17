﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Soria_v2.FolderAlumno
{
    public partial class New : System.Web.UI.Page
    {
        private readonly NegocioDireccion negocioDireccion = new NegocioDireccion();
        private readonly NegocioPersona negocioPersona = new NegocioPersona();
        private readonly NegocioAlumno negocioAlumno = new NegocioAlumno();

        public Usuario usuario = new Usuario();
        public Persona persona = new Persona();
        public Alumno alumno = new Alumno();
        public Direccion direccion = new Direccion();
        public Direccion TDireccion = new Direccion();
        public Persona TPersona = new Persona();
        public Int64 isNew;
        long IDCXE;

        public string ConvertToAMD(DateTime fecha)
        {
            string DMA = fecha.ToString().Split(' ')[0];
            string d = DMA.Split('/')[0];
            if (d.Length == 1)
            {
                d = '0' + d;
            }
            string m = DMA.Split('/')[1];
            if (m.Length == 1)
            {
                m = '0' + m;
            }
            return DMA.Split('/')[2] + '-' + m + '-' + d;
        }
        public string ConvertToDMA(DateTime fecha)
        {
            string AMD = fecha.ToString().Split(' ')[0];
            string d = AMD.Split('/')[2];
            if (d.Length == 1)
            {
                d = '0' + d;
            }
            string m = AMD.Split('/')[1];
            if (m.Length == 1)
            {
                m = '0' + m;
            }
            return AMD.Split('/')[0] + '-' + m + '-' + d;
        }
        private bool Completed(string text)
        {
            return text.ToString().Trim() != "";
        }

        public string FirstTime()
        {
            if (Request.QueryString["IDA"] == null || Convert.ToInt32(Request.QueryString["IDA"]) == 0)
            {
                //New Alumno
                return "Ingrese un nuevo alumno.";
            }
            else
            {
                //Edit Alumno.
                return "Está por editar éste alumno.";
            }
        }
        private void Update()
        {
            //Info alumno
            txtNombre.Value = alumno.Name;
            txtApellido.Value = alumno.Apellido;
            txtDNI.Text = alumno.DNI.ToString();
            txtEmail.Value = alumno.Email;
            string AMD = ConvertToAMD(alumno.Nacimiento);
            txtNacimiento.Value = AMD;
            txtCalle.Value = alumno.Direccion.Calle;
            txtAltura.Value = alumno.Direccion.Number;
            //Info tutor
            txtTNombre.Value = alumno.Tutor.Name;
            txtTApellido.Value = alumno.Tutor.Apellido;
            txtTDNI.Value = alumno.Tutor.DNI.ToString();
            txtTEmail.Value = alumno.Tutor.Email;
            string TutorAMD = ConvertToAMD(alumno.Nacimiento);
            txtTNac.Value = TutorAMD;
            txtTCalle.Value = alumno.Tutor.Direccion.Calle;
            txtTAltura.Value = alumno.Tutor.Direccion.Number;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                usuario = (Usuario)Application["Usuario"];
                persona = (Persona)Application["Persona"];
                IDCXE = (long)Session["IDCXE" + Session.SessionID];
                Session["IDCXE" + Session.SessionID] = IDCXE;
                isNew = Convert.ToInt32(Request.QueryString["IDA"]);
                if (!IsPostBack)
                {
                    if (usuario == null || usuario.ID == 0)
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                    else
                    {
                        if ( isNew != 0)
                        {
                            alumno = negocioAlumno.GetAlumnoWithId(isNew);
                            Update();
                        }
                    }
                }
                btnVolver.Attributes.Add("onclick", "history.back(); return false;");
            }
            catch (Exception ex)
            {
                Session["Error" + Session.SessionID] = ex;
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int anio = DateTime.Today.Year;
            direccion.Calle = txtCalle.Value;
            direccion.Number = txtAltura.Value;
            negocioDireccion.Agregar(direccion);
            direccion = negocioDireccion.GetDireccion(direccion);

            alumno.Name = txtNombre.Value;
            alumno.Apellido = txtApellido.Value;
            alumno.DNI = txtDNI.Text;
            alumno.Email = txtEmail.Value;
            DateTime dt = Convert.ToDateTime(txtNacimiento.Value);
            alumno.Nacimiento = Convert.ToDateTime(ConvertToDMA(dt));

            alumno.Direccion = new Direccion
            {
                ID = direccion.ID,
                Calle = direccion.Calle,
                Number = direccion.Number
            };
            //Tutor
            TDireccion.Calle = txtTCalle.Value;
            TDireccion.Number = txtTAltura.Value;
            negocioDireccion.Agregar(TDireccion);
            TDireccion = negocioDireccion.GetDireccion(TDireccion);
            TPersona.Name = txtTNombre.Value;
            TPersona.Apellido = txtTApellido.Value;
            TPersona.DNI = txtTDNI.Value;
            TPersona.Email = txtTEmail.Value;
            DateTime TdateTime = Convert.ToDateTime(txtTNac.Value);
            TPersona.Nacimiento = Convert.ToDateTime(ConvertToDMA(TdateTime));

            TPersona.Direccion = new Direccion
            {
                ID = TDireccion.ID,
                Calle = TDireccion.Calle,
                Number = TDireccion.Number
            };

            alumno.Tutor = new Persona
            {
                Name = TPersona.Name,
                Apellido = TPersona.Apellido,
                DNI = TPersona.DNI,
                Email = TPersona.Email,
                Nacimiento = TPersona.Nacimiento,
            };

            alumno.Tutor.Direccion = new Direccion
            {
                ID = TPersona.Direccion.ID,
                Calle = TPersona.Direccion.Calle,
                Number = TPersona.Direccion.Number
            };

            if (isNew != 0)
            {
                alumno.IdAlumno = isNew;
                negocioDireccion.Modificar(persona.Direccion);
                negocioDireccion.Modificar(TPersona.Direccion);
                TPersona.ID = negocioPersona.GetIDWithDNI(TPersona.DNI);
                alumno.ID   = negocioPersona.GetIDWithDNI(alumno.DNI);
                negocioPersona.Modificar(TPersona);
                negocioPersona.Modificar(alumno);
            }
            else
            {
                negocioAlumno.AgregarFull(alumno, IDCXE);
                //negocio
            }

            string ex = "El alumno se ha guardado correctamente.";
            Session["Aviso" + Session.SessionID] = ex;
            Response.Redirect("~/Usuarios/DocenteCurso.aspx", false);
        }

        protected void txtDNI_TextChanged(object sender, EventArgs e)
        {
            NegocioAlumno negocioAlumno = new NegocioAlumno();
            string DNI = txtDNI.Text;
            alumno = negocioAlumno.GetAlumnoWithDNI(DNI);
            if (alumno.ID != 0)
            {
                Update();
                Response.Write("<script>alert('DNI existente. Se carga el alumno.')</script>");
            }
            else
            {

            }
        }
    }
}