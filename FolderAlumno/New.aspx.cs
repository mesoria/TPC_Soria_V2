using System;
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

        private string ConvertToAMD(DateTime fecha)
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
        private string ConvertToDMA(DateTime fecha)
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
        private Alumno Backup()
        {
            Alumno aux = new Alumno();
            //Info alumno
            aux.Name = txtNombre.Value;
            aux.Apellido = txtApellido.Value;
            aux.DNI      = txtDNI.Text;
            aux.Email    = txtEmail.Value;
            if ( Completed(txtNacimiento.Value) )
            {
                DateTime dt  = Convert.ToDateTime(txtNacimiento.Value);
                aux.Nacimiento = Convert.ToDateTime(ConvertToDMA(dt));
            }
            Direccion direccion = new Direccion();
            aux.Direccion = direccion;
            aux.Direccion.Calle  = txtCalle.Value;
            aux.Direccion.Number = txtAltura.Value;
            //Info tutor
            Persona tutor = new Persona();
            aux.Tutor = tutor;
            aux.Tutor.Name      = txtTNombre.Value;
            aux.Tutor.Apellido  = txtTApellido.Value;
            aux.Tutor.DNI       = txtTDNI.Value;
            aux.Tutor.Email     = txtTEmail.Value;
            if (Completed(txtTNac.Value))
            {
                DateTime TdateTime = Convert.ToDateTime(txtTNac.Value);
                TPersona.Nacimiento = Convert.ToDateTime(ConvertToDMA(TdateTime));
            }
            aux.Tutor.Direccion = direccion;
            aux.Tutor.Direccion.Calle  = txtTCalle.Value;
            aux.Tutor.Direccion.Number = txtTAltura.Value;
            return aux;
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
        private void Update(Alumno aux)
        {
            //Info alumno
            txtNombre.Value = aux.Name;
            txtApellido.Value = aux.Apellido;
            txtDNI.Text = aux.DNI.ToString();
            txtEmail.Value = aux.Email;
            string AMD = ConvertToAMD(aux.Nacimiento);
            txtNacimiento.Value = AMD;
            txtCalle.Value = aux.Direccion.Calle;
            txtAltura.Value = aux.Direccion.Number;
            //Info tutor
            txtTNombre.Value = aux.Tutor.Name;
            txtTApellido.Value = aux.Tutor.Apellido;
            txtTDNI.Value = aux.Tutor.DNI.ToString();
            txtTEmail.Value = aux.Tutor.Email;
            string TutorAMD = ConvertToAMD(aux.Nacimiento);
            txtTNac.Value = TutorAMD;
            txtTCalle.Value = aux.Tutor.Direccion.Calle;
            txtTAltura.Value = aux.Tutor.Direccion.Number;
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
                if (Session["Alumno" + Session.SessionID] != null)
                {
                    alumno = (Alumno)Session["Alumno" + Session.SessionID];
                }
                else if (Session["Backup" + Session.SessionID] != null)
                {
                    alumno = (Alumno)Session["Backup" + Session.SessionID];
                }
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
                            Update(alumno);
                        }
                        else if (alumno.ID != 0)
                        {
                            Update(alumno);
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
            int anio          = DateTime.Today.Year;
            direccion.Calle   = txtCalle.Value;
            direccion.Number  = txtAltura.Value;
            negocioDireccion.Agregar(direccion);
            direccion         = negocioDireccion.GetDireccion(direccion);

            alumno.Name       = txtNombre.Value;
            alumno.Apellido   = txtApellido.Value;
            alumno.DNI        = txtDNI.Text;
            alumno.Email      = txtEmail.Value;
            DateTime dt       = Convert.ToDateTime(txtNacimiento.Value);
            alumno.Nacimiento = Convert.ToDateTime(ConvertToDMA(dt));

            alumno.Direccion  = new Direccion
            {
                ID     = direccion.ID,
                Calle  = direccion.Calle,
                Number = direccion.Number
            };
            //Tutor
            TDireccion.Calle  = txtTCalle.Value;
            TDireccion.Number = txtTAltura.Value;
            negocioDireccion.Agregar(TDireccion);
            TDireccion        = negocioDireccion.GetDireccion(TDireccion);
            TPersona.Name     = txtTNombre.Value;
            TPersona.Apellido = txtTApellido.Value;
            TPersona.DNI      = txtTDNI.Value;
            TPersona.Email    = txtTEmail.Value;
            DateTime TdateTime = Convert.ToDateTime(txtTNac.Value);
            TPersona.Nacimiento = Convert.ToDateTime(ConvertToDMA(TdateTime));

            TPersona.Direccion = new Direccion
            {
                ID     = TDireccion.ID,
                Calle  = TDireccion.Calle,
                Number = TDireccion.Number
            };

            alumno.Tutor = new Persona
            {
                Name       = TPersona.Name,
                Apellido   = TPersona.Apellido,
                DNI        = TPersona.DNI,
                Email      = TPersona.Email,
                Nacimiento = TPersona.Nacimiento,
            };

            alumno.Tutor.Direccion = new Direccion
            {
                ID     = TPersona.Direccion.ID,
                Calle  = TPersona.Direccion.Calle,
                Number = TPersona.Direccion.Number
            };

            if (isNew != 0)
            {
                alumno.IdAlumno = isNew;
                negocioDireccion.Modificar(persona.Direccion);
                negocioDireccion.Modificar(TPersona.Direccion);
                TPersona.ID = negocioPersona.GetIDWithDNI(TPersona.DNI);
                if ( TPersona.ID == 0)
                {
                    TPersona.ID = negocioPersona.GetIDWithDNI(TPersona.DNI);
                }
                alumno.ID = negocioPersona.GetIDWithDNI(alumno.DNI);
                if (alumno.ID == 0)
                {
                    alumno.ID = negocioPersona.GetIDWithDNI(alumno.DNI);
                }
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
                Session["Alumno" + Session.SessionID] = alumno;
                Session["Backup" + Session.SessionID] = Backup();
                Response.Redirect("~/FolderAlumno/Validation.aspx");
            }
        }
    }
}