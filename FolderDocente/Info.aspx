<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Info.aspx.cs" Inherits="TPC_Soria_v2.FolderDocente.Info" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Docente</h1>
    <br />
    <h3>Información personal</h3>
    <div class="form-row">
        <div class="form-group col-md-6">
            <label for="txtNombre">Nombre</label>
            <input type="Text" class="form-control" id="txtNombre"  runat="server" Value="" readonly>
        </div>
        <div class="form-group col-md-6">
            <label for="txtApellido">Apellido</label>
            <input type="Text" class="form-control" id="txtApellido"  runat="server" Value="" readonly>
        </div>
    </div>    
    <div class="form-row">
        <div class="form-group col-md-4">
            <label for="txtDNI">DNI</label>
            <input type="Text" class="form-control" id="txtDNI"  runat="server" Value="" readonly>
        </div>
        <div class="form-group col-md-4">
            <label for="txtNacimiento">Fecha de nacimiento</label>
            <input id="txtNacimiento" type="date" class="form-control" name="trip-start" runat="server" readonly>
        </div>
    </div>
    <h6>Dirección</h6>
    <div class="form-row">
        <div class="form-group col-md-8">
            <label for="txtCalle">Domicilio</label>
            <input type="text" class="form-control" id="txtCalle" runat="server" Value="" readonly>
            <small class="form-text text-muted">Calle</small>
        </div>
        <div class="form-group col-md-4">
            <label for="txtAltura">Altura</label>
            <input type="text" class="form-control" id="txtAltura" runat="server" Value="" readonly>
            <small class="form-text text-muted">Número</small>
        </div>
    </div>
    <h6>Contacto</h6>    
    <div class="form-row">
        <div class="form-group col-md-4">
            <label for="txtEmail">Email</label>
            <div class="input-group">
                <div class="input-group-prepend">
                    <div class="input-group-text">@</div>
                </div>
                <input type="Text" class="form-control" id="txtEmail"  runat="server" Value="" readonly>
            </div>
        </div>
    </div>
    <hr />
    <br />
    <h6>Información académica</h6>
    <div class="form-row">
        <div class="form-group col-md-6">
            <label for="txtNivel">Nivel</label>
            <input type="Text" class="form-control" id="txtNivel"  runat="server" Value="<% =Aux.Nivel %>" readonly />
        </div>
    </div>
    <hr />
    <div class="form-row">
        <div class="form-group col-md-5">
            <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-primary btn-lg" Text="Volver" PostBackUrl="~/Docentes.aspx"/>
        </div>
        <div class="form-group col-md-6">
            <a class="btn-warning btn-lg" href="New.aspx?idD=<% = Aux.IdDocente %>">Editar</a>
        </div>
        <div class="form-group col-md-1">
            <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Eliminar" CssClass="btn btn-danger btn-lg"/>
        </div>
    </div>
</asp:Content>
