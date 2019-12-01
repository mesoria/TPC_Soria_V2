<%@ Page Title="Información del establecimiento" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Info.aspx.cs" Inherits="TPC_Soria_v2.FolderEstablecimiento.Info" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Establecimiento</h1>
    <div class="form-row">
        <div class="form-group col-md-12">
            <label for="txtNombre">Nombre</label>
            <input type="Text" class="form-control" id="txtNombre"  runat="server" Value="" readonly>
        </div>
    </div>
        
    <div class="form-row">
        <div class="form-group col-md-6">
            <label for="txtNivel">Nivel</label>
            <input type="Text" class="form-control" id="txtNivel"  runat="server" Value="<% =Aux.Nivel %>" readonly>
        </div>
        <%if ( Aux.Nivel == "Primaria" || Aux.Nivel == "Secundaria" )
          {%>
        <div class="form-group col-md-6">
            <label for="txtNumero">Número</label>
            <input type="number" class="form-control" id="txtNumero" runat="server" Value="" readonly>
        </div>
        <%} %>
    </div>
    
    <hr />
    <h3>Dirección</h3>
    <div class="form-row">
        <div class="form-group col-md-8">
            <label for="txtCalle">Domicilio</label>
            <input type="text" class="form-control" id="txtCalle" runat="server" Value="<% =Aux.Direccion.Calle %>" readonly>
            <small class="form-text text-muted">Calle</small>
        </div>
        <div class="form-group col-md-4">
            <label for="txtAltura">Altura</label>
            <input type="text" class="form-control" id="txtAltura" runat="server" Value="<% =Aux.Direccion.Number %>" readonly>
            <small class="form-text text-muted">Número</small>
        </div>
    </div>

    <hr />
    <div class="form-row">
        <div class="form-group col-md-5">
            <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-primary btn-lg" Text="Volver" PostBackUrl="~/Establecimientos.aspx"/>
        </div>
        <div class="form-group col-md-6">
            <a class="btn-warning btn-lg" href="NewEstablecimiento.aspx?idE=<% = Aux.ID %>">Editar</a>
        </div>
        <div class="form-group col-md-1">
            <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Eliminar" CssClass="btn btn-danger btn-lg"/>
        </div>
    </div>
</asp:Content>
