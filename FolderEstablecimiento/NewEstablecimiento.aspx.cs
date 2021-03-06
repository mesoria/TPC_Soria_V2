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
    public partial class NewEstablecimiento : System.Web.UI.Page
    {
        public NegocioEstablecimiento negocioEstablecimiento = new NegocioEstablecimiento();
        public NegocioDireccion negocioDireccion = new NegocioDireccion();

        public Direccion direccion = new Direccion();
        public Establecimiento establecimiento = new Establecimiento();

        public Int64 isNew;
        private bool Completed(string text)
        {
            return text.ToString().Trim() != "";
        }
        private bool IsNivel()
        {
            if (cbxNivel.Value == "Primaria" || cbxNivel.Value == "Secundaria")
            {
                return Completed(txtNumero.Value);
            }
            else
            {
                if (cbxNivel.Value == "Facultad" || cbxNivel.Value == "Universidad")
                {
                    return true;
                }
            }
            return false;
        }
        public string IsNew()
        {
            if (Request.QueryString["idE"] == null)
            {
                //New Establecimiento
                return "Ingrese un nuevo establecimiento.";
            }
            else
            {
                //Edit Establecimiento.
                return "Está por editar éste establecimiento.";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["idE"] == null)
                {
                    isNew = 0;
                    //Debería agregar un nuevo establecimiento.
                }
                else
                {
                    isNew = Convert.ToInt32(Request.QueryString["idE"]);
                    if (!IsPostBack)
                    {
                        establecimiento = negocioEstablecimiento.GetEstablecimientoWithId(isNew);

                        //establecimiento = NegocioPersona.ListarPersonas().Find(J => J.ID == establecimientoId);
                        //grid.DataSource = NegocioVoucher.ListarVouchers();
                        //grid.DataBind();
                        //txtNombre.Text = establecimiento.Name;
                        txtNombre.Value = establecimiento.Name;
                        switch (establecimiento.Nivel)
                        {
                            case "Secundaria":
                                cbxNivel.SelectedIndex = 1;
                                txtNumero.Value = establecimiento.Number.ToString();
                                break;
                            case "Facultad":
                                cbxNivel.SelectedIndex = 2;
                                break;
                            case "Universidad":
                                cbxNivel.SelectedIndex = 3;
                                break;
                            default:
                                cbxNivel.SelectedIndex = 0;
                                txtNumero.Value = establecimiento.Number.ToString();
                                break;
                        }
                        txtCalle.Value = establecimiento.Direccion.Calle;
                        txtAltura.Value = establecimiento.Direccion.Number;
                    }
                }
                btnVolver.Attributes.Add("onclick", "history.back(); return false;");
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
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if ( Completed( txtCalle.Value) && Completed( txtAltura.Value) && Completed(txtNombre.Value) && IsNivel() )
                {
                    direccion.Calle = txtCalle.Value;
                    direccion.Number = txtAltura.Value;
                    negocioDireccion.Agregar(direccion);
                    direccion = negocioDireccion.GetDireccion(direccion);
                    //establecimiento.Name = txtNombre.Text;
                    establecimiento.Name = txtNombre.Value;
                    switch (cbxNivel.SelectedIndex)
                    {
                        case 1:
                            establecimiento.Nivel = "Secundaria";
                            break;
                        case 2:
                            establecimiento.Nivel = "Facultad";
                            break;
                        case 3:
                            establecimiento.Nivel = "Universidad";
                            break;
                        default:
                            establecimiento.Nivel = "Primaria";
                            break;
                    }
                    if (establecimiento.Nivel == "Facultad" || establecimiento.Nivel == "Universidad")
                    {
                        establecimiento.Number = 0;
                    }
                    else
                    {
                        establecimiento.Number = Convert.ToInt32(txtNumero.Value);
                    }
                    establecimiento.Direccion = new Direccion
                    {
                        ID     = direccion.ID,
                        Calle  = direccion.Calle,
                        Number = direccion.Number

                    };
                    if (isNew != 0)
                    {
                        establecimiento.ID = isNew;
                        negocioDireccion.Modificar(establecimiento.Direccion);
                        negocioEstablecimiento.Modificar(establecimiento);
                    }
                    else
                    {
                        negocioEstablecimiento.Agregar(establecimiento);
                    }

                    txtNombre.Value = "";
                    cbxNivel.SelectedIndex = 0;
                    txtCalle.Value = "";
                    txtNumero.Value = "";
                    txtAltura.Value = "";
                    txtDepartamento.Value = "";
                    txtPiso.Value = "";

                    //Response.Write("<script>alert('El establecimiento se a cargado correctamente')</script>");

                    //txtNombre.Text = "";
                    string ex = "El establecimiento se ha guardado correctamente.";
                    Session["Aviso" + Session.SessionID] = ex;
                    Response.Redirect("~/Establecimientos.aspx", false);

                    //Response.Redirect("javascript: window.history.back()");
                    //Response.Redirect(Request.UrlReferrer.ToString());
                }
                else
                {
                    //Campos necesarios.
                }

            }
            catch (Exception ex)
            {
                Session["Error" + Session.SessionID] = ex;
                Response.Redirect("frmLog.aspx");
            }
        }
    }
}