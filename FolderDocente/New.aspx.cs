using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_Soria_v2.FolderDocente
{
    public partial class New : System.Web.UI.Page
    {
        public NegocioDocente negocioDocente     = new NegocioDocente();
        public NegocioDireccion negocioDireccion = new NegocioDireccion();
        public NegocioPersona negocioPersona     = new NegocioPersona();
        public Usuario usuario = new Usuario();
        public Direccion direccion = new Direccion();
        public Docente Aux         = new Docente();
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
            if (Request.QueryString["idE"] == null)
            {
                //New Docente
                return "Ingrese un nuevo docente.";
            }
            else
            {
                //Edit Docente.
                return "Está por editar éste docente.";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    usuario = (Usuario)Application["Usuario"];
                    if (usuario == null || usuario.ID == 0)
                    {
                        Response.Redirect("~/Login.aspx");
                    }

                    else if (Request.QueryString["idD"] == null)
                    {
                        //Debería agregar un nuevo aux.
                    }
                    else
                    {
                        Int64 auxId = Convert.ToInt32(Request.QueryString["idD"]);
                        Aux = negocioDocente.GetDocenteWithId(auxId);

                        //aux = NegocioPersona.ListarPersonas().Find(J => J.ID == auxId);
                        //grid.DataSource = NegocioVoucher.ListarVouchers();
                        //grid.DataBind();
                        //txtNombre.Text = aux.Name;
                        txtNombre.Value = Aux.Name;
                        txtApellido.Value = Aux.Apellido;
                        txtDNI.Value = Aux.DNI;
                        txtEmail.Value = Aux.Email;
                        switch (cbxNivel.SelectedIndex)
                        {
                            case 1:
                                Aux.Nivel = "Secundaria";
                                break;
                            case 2:
                                Aux.Nivel = "Facultad";
                                break;
                            case 3:
                                Aux.Nivel = "Universidad";
                                break;
                            default:
                                Aux.Nivel = "Primaria";
                                break;
                        }
                        string AMD = ConvertToAMD(Aux.Nacimiento);
                        txtNacimiento.Value = AMD;
                        txtCalle.Value = Aux.Direccion.Calle;
                        txtAltura.Value = Aux.Direccion.Number;
                    }
                    btnVolver.Attributes.Add("onclick", "history.back(); return false;");
                }
            }
            catch (Exception ex)
            {
                Session["Error" + Session.SessionID] = ex;
                Response.Redirect("/frmLog.aspx");
            }
        }

        public static void Limpiar(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Text = "";
                }
            }
        }
        public bool Validation()
        {
            return Completed(txtCalle.Value) && Completed(txtAltura.Value) && Completed(txtNombre.Value) && Completed(txtApellido.Value) && Completed(txtDNI.Value) && Completed(txtEmail.Value) && Completed(txtNacimiento.Value);
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                if ( Validation() )
                {
                    direccion.Calle  = txtCalle.Value;
                    direccion.Number = txtAltura.Value;
                    negocioDireccion.Agregar(direccion);
                    direccion       = negocioDireccion.GetDireccion(direccion);
                    Aux.Name        = txtNombre.Value;
                    Aux.Apellido    = txtApellido.Value;
                    Aux.DNI         = txtDNI.Value;
                    Aux.Email       = txtEmail.Value;
                    DateTime dt     = Convert.ToDateTime(txtNacimiento.Value);
                    //string format = "DD-MM-YYYY";            // Use this format
                    //DateTime dt1 = Convert.ToDateTime(dt.ToString(format)); // here Error 
                    Aux.Nacimiento = Convert.ToDateTime( ConvertToDMA(dt)) ;
                    switch (cbxNivel.SelectedIndex)
                    {
                        case 1:
                            Aux.Nivel = "Secundaria";
                            break;
                        case 2:
                            Aux.Nivel = "Facultad";
                            break;
                        case 3:
                            Aux.Nivel = "Universidad";
                            break;
                        default:
                            Aux.Nivel = "Primaria";
                            break;
                    }
                    Aux.Direccion = direccion;
                    if (Aux.ID != 0)
                    {
                        negocioDireccion.Modificar(Aux.Direccion);
                        negocioPersona.Modificar(Aux);
                        negocioDocente.Modificar(Aux);
                    }
                    else
                    {
                        negocioDocente.Agregar(Aux);
                    }

                    Response.Write("<script>alert('El Docente se a cargado correctamente')</script>");

                    //txtNombre.Text = "";
                    txtNombre.Value = "";
                    txtApellido.Value = "";
                    txtDNI.Value = "";
                    txtCalle.Value = "";
                    txtAltura.Value = "";
                    Response.Redirect("~/Default.aspx", false);
                    //Response.Redirect("javascript: window.history.back()");
                    //Response.Redirect(Request.UrlReferrer.ToString());
                }
                else
                {
                    Response.Write("<script>alert('Antes debe completar todos los campos')</script>");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ValidateFalse()
        {
            txtNombre.Value     = "1";
            txtApellido.Value   = "1";
            txtDNI.Value        = "1";
            txtEmail.Value      = "1";
            cbxNivel.Value      = "Primaria";
            txtNacimiento.Value = "1987/08/06";
            txtCalle.Value      = "1";
            txtAltura.Value     = "1";
        }
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            ValidateFalse();
            Response.Redirect("~/Docentes.aspx");
        }
    }
}