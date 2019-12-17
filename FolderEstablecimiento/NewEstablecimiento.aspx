<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewEstablecimiento.aspx.cs" Inherits="TPC_Soria_v2.FolderEstablecimiento.NewEstablecimiento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3> <% =IsNew() %></h3>
    <div class="form-row">
        <div class="form-group col-md-12">
            <label>Nombre</label>
            <input type="Text" class="form-control" id="txtNombre"  runat="server" placeholder="Nombre" required>
            <div class="valid-feedback">
                Se ve bien!
            </div>
            <div class="invalid-feedback">
                Por favor, ingrese el nombre.
            </div>
        </div>
    </div>
    <div class="form-row">
        <div class="form-group col-md-6">
            <label for="cbxNivel">Nivel</label>
            <select id="cbxNivel" runat="server" class="form-control" required>
                <option selected>Primaria</option>
                <option>Secundaria</option>
                <option>Facultad</option>
                <option>Universidad</option>
            </select>
        </div>
        
        <%if (cbxNivel.SelectedIndex == 0 || cbxNivel.SelectedIndex == 1)
            {%>
        <div class="form-group col-md-6">
            <label for="txtNumero">Número</label>
            <input type="number" class="form-control" id="txtNumero" runat="server" placeholder="5">
        </div>
        <%} %>
    </div>
    
    <hr />
    <div class="form-row">
        <div class="form-group col-md-8">
            <label for="txtCalle">Dirección</label>
            <input type="text" class="form-control" id="txtCalle" runat="server" placeholder="Miguél Cané" required>
            <small class="form-text text-muted">Calle</small>
            <div class="invalid-feedback">
                Por favor, ingrese la calle.
            </div>
        </div>
        <div class="form-group col-md-4">
            <label for="txtAltura">Número</label>
            <input type="text" class="form-control" id="txtAltura" runat="server" placeholder="1234" required>
            <small class="form-text text-muted">Altura</small>
            <div class="invalid-feedback">
                Por favor, ingrese la altura.
            </div>
        </div>
        <div class="form-group col-md-6">
            <label for="txtDepartamento">Departamento</label>
            <input type="text" class="form-control" id="txtDepartamento" runat="server">
        </div>
        <div class="form-group col-md-6">
            <label for="txtPiso">Piso</label>
            <input type="text" class="form-control" id="txtPiso" runat="server">
        </div>
    </div>
    <hr />
    <div class="form-row">
        <div class="form-group col-md-5">
            <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-primary btn-lg" Text="Establecimientos" PostBackUrl="~/Default.aspx"/>
        </div>
        <div class="form-group col-md-7">
            <asp:Button ID="btnGuardar" runat="server" Onclick="btnGuardar_Click" Text="Guardar" CssClass="btn btn-success btn-lg"/>
        </div>
    </div>
</asp:Content>
