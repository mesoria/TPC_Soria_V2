<%@ Page Title="Cursos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cursos.aspx.cs" Inherits="TPC_Soria_v2.Curso.Cursos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Label ID="lblApellido" runat="server" Font-Size="X-Large" Text=""><% = maestra.Apellido %></asp:Label>
        <asp:Label ID="lvlName" runat="server" Font-Size="X-Large" Text=""><% = maestra.Name %> </asp:Label>
    </div>

    <nav aria-label="breadcrumb">
      <ol class="breadcrumb">
        <li class="breadcrumb-item"><a href="../Maestra/MaestraPrincipal.aspx?idM=<% = maestra.ID %>">Escuelas</a></li>
        <li class="breadcrumb-item active" aria-current="page">Cursos</li>
      </ol>
    </nav>

<div class="table-responsive">
    <table class="table">
    <thead>
        <tr>
            <th style="width: 5%; background-color: #00aa00">
              # </th>
            <th style="width: 10%; background-color: #00cc00">
              Nivel </th>
            <th style="width: 10%; background-color: #47ff47">
              Nombre </th>
            <th style="width: 10%; background-color: #77ff77">
              Número </th>
            <th style="width: 10%; background-color: #87FF87">
            </th>
        </tr>
    </thead>
    <%var j = 1;
    foreach (var item in Cursos)
    {%>
    <tbody>
        <tr>
            <th style="width:  5%"><% = j%></th>
            <th style="width: 15%"><% = item.Nivel %></th>
            <th style="width: 15%"><% = item.Name %></th>
            <th style="width: 15%"><% = item.Number %></th>
            <th style="width: 15%"> <a class="btn btn-primary" href="../FolderEstablecimiento/Info.aspx?idE=<% = item.ID %>">Más información</a> </th>
        <%j++;}%>
        </tr>
    </tbody>
    </table>
</div>
    <hr />
    <div class="form-row">
        <div class="form-group col-md-5">
            <a class="btn btn-primary" href="../Curso/New.aspx?idM=<% = maestra.ID %>">Nuevo Curso</a>
        </div>
        <div class="form-group col-md-7">
            <a class="btn btn-primary" href="../Maestra/Cursos.aspx?idM=<% = maestra.ID %>">Ir a Alumnos</a>
        </div>
    </div> 




    <div>
        <a class="btn btn-primary" href="../Maestra/MaestraPrincipal.aspx?idM=<% = maestra.ID %>">Atras</a>
    </div>
    <div>
        <a class="btn btn-primary" href="../Maestra/AsistenciaAlumnos.aspx?idM=<% = maestra.ID %>">Alumnos</a>
    </div>

</asp:Content>
