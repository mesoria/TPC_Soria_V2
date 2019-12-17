<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarCalificaciones.aspx.cs" Inherits="TPC_Soria_v2.FolderFormularios.EditarCalificaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label ID="lblApellido" runat="server" Text="Apellido: "></asp:Label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="border-success form-control"></asp:TextBox>
            </div>
            <div class="form-group col-md-6">
                <asp:Label ID="lblName" runat="server" Text="Nombre: "></asp:Label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="border-success form-control"></asp:TextBox>
            </div>
        </div>
    </div>
    <div>
        <div class="form-row">
            <div class="form-group col-md-2">
                <asp:Label ID="lblNota1" runat="server" Text="Primer parcial: "></asp:Label>
            </div>
            <div class="form-group col-md-2">
                <asp:TextBox ID="txtNota1" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group col-md-2">
                <asp:Button ID="btnGuardar1" runat="server" Text="Guardar" CssClass="btn btn-success"/>
            </div>
        </div>
    </div>
    <div>
        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label ID="lblNota2" runat="server" Text="Segundo parcial: "></asp:Label>
                <asp:TextBox ID="txtNota2" runat="server" CssClass="border-0 form-control"></asp:TextBox>
                <asp:Button ID="btnGuardar2" runat="server" Text="Guardar" CssClass="btn btn-success btn-lg form-control"/>
            </div>
        </div>  
    </div>
    <div>
        <div class="form-row">
            <div class="form-group col-md-6">
                <asp:Label ID="lblNota3" runat="server" Text="FINAL: "></asp:Label>
                <asp:TextBox ID="txtNota3" runat="server" CssClass="border-success form-control"></asp:TextBox>
                <asp:Button ID="btnGuardar3" runat="server" Text="Guardar" CssClass="btn btn-success btn-lg"/>
            </div>
        </div>
    </div>
    <div>
        <div class="form-group col-md-12">
            <asp:Label ID="lblCuerpo" runat="server" Text="Observación: "></asp:Label>
            <asp:TextBox ID="txtObservacion" runat="server" CssClass="border-success form-control" TextMode="MultiLine" Rows="10"> </asp:TextBox>
            <asp:Button ID="btnGuardarObs" runat="server" Text="Guardar" />
        </div>
    </div>
    <br />
    <hr />
    <div class="form-row">
        <div class="form-group col-md-5">
            <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-primary btn-lg" Text="Volver" />
        </div>
    </div>
</asp:Content>
