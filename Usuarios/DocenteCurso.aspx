<%@ Page Title="Establecimientos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DocenteCurso.aspx.cs" Inherits="TPC_Soria_v2.Usuarios.DocenteCurso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-row">
        <div class="form-group col-md-10">
            <h3>Establecimiento</h3>
        </div>
        <div class="form-group col-md-2">
            <asp:Label ID="lblApellido" runat="server" Font-Size="X-Large" Text=""><% = persona.Apellido %></asp:Label>
        </div>
    </div>
    <div class="form-row">
        <div class="form-group col-md-10">
            <asp:Label ID="txtEstablecimiento" runat="server" Font-Size="X-Large" Text=""><% = establecimiento.Name %> </asp:Label>
        </div>
        <div class="form-group col-md-2">
            <asp:Label ID="lvlName" runat="server" Font-Size="X-Large" Text=""><% = persona.Name %> </asp:Label>
        </div>
    </div>
    <hr />
    <br />
    <h4>Curso</h4>
    <div class="form-row">
        <div class="form-group col-md-4">
            <asp:Label ID="txtCurso" runat="server" Font-Size="X-Large" Text="Nombre: "><% = establecimiento.Curso.Name %> </asp:Label>
        </div>
        <div class="form-group col-md-2">
            <a class="btn btn-primary" href="../FolderFormularios/PasarAsistencia.aspx?IDCXE=<% = GetIDCXE(establecimiento.ID,establecimiento.Curso.ID) %>">Pasar Asistencia</a>
        </div>
        <div class="form-group col-md-2">
            <a class="btn btn-primary" href="../FolderFormularios/Notas.aspx?IDCXE=<% = GetIDCXE(establecimiento.ID,establecimiento.Curso.ID) %>">Calificaciones</a>
        </div>
        <div class="form-group col-md-2">
            <a class="btn btn-primary" href="../FolderFormularios/MostrarAsistencias.aspx?IDCXE=<% = GetIDCXE(establecimiento.ID,establecimiento.Curso.ID) %>">Libro de Asistencias</a>
        </div>
    </div>
    <hr />
    <h5>Alumnos</h5>

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
            <th style="width: 15%"> <a class="btn btn-primary" href="../FolderAlumno/Info.aspx?IDA=<% =item.IdAlumno %>">Más información</a> </th>
        <%j++;}%>
        </tr>
    </tbody>
    </table>
</div>
    <hr />
    <div class="form-row">
        <div class="form-group col-md-5">
            <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-primary btn-lg" Text="Volver" PostBackUrl="~/Docentes.aspx"/>
        </div>
        <div class="form-group col-md-5">
            <a class="btn btn-primary" href="../FolderAlumno/New.aspx?IDA=0">Nuevo Alumno</a>
        </div>
        <%--<div class="form-group col-md-7">
            <a class="btn btn-primary" href="../Maestra/Cursos.aspx?idM=<% = maestra.ID %>">Ir a Alumnos</a>
        </div>--%>
    </div>

</asp:Content>
