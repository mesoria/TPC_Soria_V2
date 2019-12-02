﻿<%@ Page Title="Cursos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DocenteCurso.aspx.cs" Inherits="TPC_Soria_v2.Usuarios.DocenteCurso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Label ID="lblApellido" runat="server" Font-Size="X-Large" Text=""><% = persona.Apellido %></asp:Label>
        <asp:Label ID="lvlName" runat="server" Font-Size="X-Large" Text=""><% = persona.Name %> </asp:Label>
    </div>
    <h1>Establecimiento</h1>
    <div class="form-row">
        <div class="form-group col-md-12">
            <asp:Label ID="txtEstablecimiento" runat="server" Font-Size="X-Large" Text="Nombre: "><% = establecimiento.Name %> </asp:Label>
        </div>
    </div>
    <hr />
    <br />
    <h3>Curso</h3>
    <div class="form-row">
        <div class="form-group col-md-6">
            <asp:Label ID="txtCurso" runat="server" Font-Size="X-Large" Text="Nombre: "><% = curso.Name %> </asp:Label>
        </div>
    </div>
    
    <hr />
    <h3>Alumnos</h3>

<div class="table-responsive">
    <table class="table">
    <thead>
        <tr>
            <th style="width: 5%; background-color: #00aa00">
              # </th>
            <th style="width: 10%; background-color: #00cc00">
              Apellido </th>
            <th style="width: 10%; background-color: #47ff47">
              Nombre </th>
            <th style="width: 10%; background-color: #77ff77">
              Info </th>
            <th style="width: 10%; background-color: #87FF87">
            </th>
        </tr>
    </thead>
    <%var j = 1;
    foreach (var item in alumnos)
    {%>
    <tbody>
        <tr>
            <th style="width:  5%"><% = j%></th>
            <th style="width: 15%"><% = item.Apellido %></th>
            <th style="width: 15%"><% = item.Name %></th>
            <th style="width: 15%"> <a class="btn btn-primary" href="../FolderAlumno/Info.aspx?idA=<% = item.ID %>">Más información</a> </th>
        <%j++;}%>
        </tr>
    </tbody>
    </table>
</div>
    <hr />
    <div class="form-row">
        <div class="form-group col-md-5">
            <a class="btn btn-primary" href="../FolderAlumno/New.aspx?idCXE=<% = GetId(establecimiento.ID,curso.ID) %>">Nuevo Curso</a>
        </div>
        <%--<div class="form-group col-md-7">
            <a class="btn btn-primary" href="../Maestra/Cursos.aspx?idM=<% = maestra.ID %>">Ir a Alumnos</a>
        </div>--%>
    </div>

</asp:Content>
