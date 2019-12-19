<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Log.aspx.cs" Inherits="TPC_Soria_v2.FolderAlumno.Log" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="lblTitulo" runat="server" Text="Ya existe un alumno con este DNI: "></asp:Label>
    <h6>Información personal</h6>
    <div class="form-row">
        <div class="form-group col-md-6">
            <asp:Label ID="lblDNI" runat="server" ></asp:Label>
        </div>
        <div class="form-group col-md-6">
            <asp:Label ID="lblApellido" runat="server" ></asp:Label>
        </div>
    </div>

    <div class="form-row">
        <div class="form-group col-md-6">
            <asp:Label ID="Label1" runat="server" ></asp:Label>
        </div>
        <div class="form-group col-md-6">
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary"/>
            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-success" />
        </div>
    </div>
</asp:Content>
