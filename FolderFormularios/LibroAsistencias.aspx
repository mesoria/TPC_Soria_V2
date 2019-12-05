<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LibroAsistencias.aspx.cs" Inherits="TPC_Soria_v2.FolderFormularios.LibroAsistencias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<h2><%: Title %></h2>--%>

    <div class="form-row">
        <div class="form-group col-md-10">
            <asp:Label ID="txtTitulo" runat="server" Font-Size="X-Large" Text="">Libro de asistencias</asp:Label>
        </div>
        <div class="form-group col-md-2">
            <asp:Label ID="lblApellido" runat="server" Font-Size="X-Large" Text=""><% = persona.Apellido %></asp:Label>
        </div>
        <div class="form-group col-md-10">
        </div>
        <div class="form-group col-md-2">
            <asp:Label ID="lvlName" runat="server" Font-Size="X-Large" Text=""><% = persona.Name %> </asp:Label>
        </div>
    </div>
    <div class="form-row">
        <div class="form-group col-md-4">
            <label for="cbxMes">Mes</label>
            <select id="cbxMes" runat="server" class="form-control" required>
                <option selected>Elegir mes...</option>
                <option>Enero</option>
                <option>Febrero</option>
                <option>Marzo</option>
                <option>Abril</option>
                <option>Mayo</option>
                <option>Junio</option>
                <option>Julio</option>
                <option>Agosto</option>
                <option>Septiembre</option>
                <option>Octubre</option>
                <option>Noviembre</option>
                <option>Diciembre</option>
            </select>
        </div>
        <div class="form-group col-md-2">
            <asp:Button ID="btnMostrar" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnMostrar_Click" Text="Mostrar"/>
            <%--<asp:Label ID="Date" CssClass="align-items-end" runat="server" Font-Size="X-Large" Text=""><% = DateTime.Today.Year+"-"+DateTime.Today.Month+"-"+DateTime.Today.Day%></asp:Label>--%>
        </div>
    </div>

    <nav aria-label="breadcrumb">
      <ol class="breadcrumb">
        <li class="breadcrumb-item"><a href="../MaestraPrincipal.aspx?idM=<% = persona.ID %>">Escuelas</a></li>
        <li class="breadcrumb-item"><a href="../FolderCurso/Cursos.aspx?idM=<% = persona.ID %>">Cursos</a></li>
        <li class="breadcrumb-item active" aria-current="page">Alumnos</li>
      </ol>
    </nav>
    <div class="form-row">
        <div class="form-group col-md-6">
            <asp:Label ID="txtCurso" runat="server" Font-Size="X-Large" Text="Nombre: "><% = establecimiento.Curso.Name %> </asp:Label>
        </div>
    </div>      
<div class="table-responsive">
    <table class="table">
    <thead>
        <tr>
            <th style="width: 5%; background-color: #00aa00">
              # </th>
            <th style="width: 10%; background-color: #00cc00">
              Apellidos </th>
            <th style="width: 10%; background-color: #47ff47">
              Nombres </th>
            <% if (!IsPostBack) { Dias(); } else { DiasDelMes( cbxMes.SelectedIndex); } %>
        </tr>
    </thead>
    <%var j = 1;
    foreach (var item in ListaAlumnos)
    {%>
    <tbody>
        <tr>
            <th style="width: 5%"><% = j%></th>
            <th style="width: 15%"><% = item.Apellido %></th>
            <th style="width: 15%"><% = item.Name %></th>
            <% = Asistencias(item)%>
       <% j++;}%>
        </tr>
    </tbody>
    </table>
</div>
    <br />
    <hr />

    <div class="form-row">
        <div class="form-group col-md-5">
            <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-primary btn-lg" Text="Volver" />
        </div>
    </div>
</asp:Content>
