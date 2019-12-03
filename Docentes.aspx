<%@ Page Title="Docentes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Docentes.aspx.cs" Inherits="TPC_Soria_v2.Docentes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <nav aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item active" aria-current="page">Docentes</li>
        </ol>
    </nav>
<div class="table-responsive">
    <table class="table">
        <thead>
            <tr>
                <th style="width: 5%; background-color: #00aa00">
                    # </th>
                <th style="width: 10%; background-color: #00cc00">
                    Nombre </th>
                <th style="width: 10%; background-color: #47ff47">
                    Apellido </th>
                <th style="width: 10%; background-color: #77ff77">
                    Nivel  </th>
                <th style="width: 10%; background-color: #87FF87">
                </th>
            </tr>
        </thead>
    <%var j = 1;
    foreach (var item in ListaDocentes)
    {%>
    <tbody>
        <tr>
            <th style="width:  5%"><% = j%></th>
            <th style="width: 15%"><% = item.Name %></th>
            <th style="width: 15%"><% = item.Apellido %></th>
            <th style="width: 15%"><% = item.Nivel %></th>
            <th style="width: 15%"> <a class="btn btn-primary" href="FolderDocente/Info.aspx?idD=<% = item.IdDocente %>">Más información</a> </th>
        <%j++;}%>
        </tr>
    </tbody>
    </table>
</div>
    <hr />
   <%-- <%if (usuario.Perfil == "Administrador")
        {%>--%>
    <div class="form-row">
        <div class="form-group col-md-5">
            <a class="btn btn-primary" href="FolderDocente/New.aspx">Ingresar nuevo Docente</a>
        </div>
    </div>
    <%--<%} %>--%>
</asp:Content>
