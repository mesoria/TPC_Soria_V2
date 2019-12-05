<%@ Page Title="Libro de asistencias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MostrarAsistencias.aspx.cs" Inherits="TPC_Soria_v2.FolderFormularios.MostrarAsistencias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h2><%: Title %></h2>

    <div class="form-row">
        <div class="form-group col-md-10">
            <%--<asp:Label ID="txtTitulo" runat="server" Font-Size="X-Large" Text="">Libro de asistencias</asp:Label>--%>
        </div>
        <div class="form-group col-md-2">
            <asp:Label ID="lblApellido" runat="server" Font-Size="X-Large" Text=""><% = persona.Apellido %></asp:Label>
        </div>
        <div class="form-group col-md-10">
            <asp:Label ID="Label1" runat="server" Font-Size="Large" Text="">Seleccione el mes del cual quiere ver el registro.</asp:Label>
        </div>
        <div class="form-group col-md-2">
            <asp:Label ID="lvlName" runat="server" Font-Size="X-Large" Text=""><% = persona.Name %> </asp:Label>
        </div>
    </div>
    <div class="form-row">
        <div class="form-group col-md-4">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <label for="cbxMes">Mes</label>
                    <select id="cbxMes" runat="server" class="form-control" required> <%--onchange="location.reload(true)">--%>
                        <option selected>Mes actual</option>
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
            </div>
        </div>
        <div class="form-group col-md-2">
            <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-primary btn-lg" Text="Actualizar"/>
            <%--<asp:Label ID="Date" CssClass="align-items-end" runat="server" Font-Size="X-Large" Text=""><% = DateTime.Today.Year+"-"+DateTime.Today.Month+"-"+DateTime.Today.Day%></asp:Label>--%>
        </div>
    </div>
    <%--<nav aria-label="breadcrumb">
      <ol class="breadcrumb">
        <li class="breadcrumb-item"><a href="../MaestraPrincipal.aspx?idM=<% = persona.ID %>">Escuelas</a></li>
        <li class="breadcrumb-item"><a href="../FolderCurso/Cursos.aspx?idM=<% = persona.ID %>">Cursos</a></li>
        <li class="breadcrumb-item active" aria-current="page">Alumnos</li>
      </ol>
    </nav>--%>
    <div class="form-row">
        <div class="form-group col-md-6">
            <asp:Label ID="Label2" runat="server" Font-Size="Large" Text="Nombre: ">Curso: </asp:Label>
            <asp:Label ID="txtCurso" runat="server" Font-Size="X-Large" Text="Nombre: "><% = establecimiento.Curso.Name %> </asp:Label>
        </div>
    </div>
    <style>
    .letrachica{font-size:15px;}
</style>
<div class="table-responsive">
    <table class="letrachica">
    <thead>
        <tr>
            <th style="width: 4%; background-color: #00aa00">
              # </th>
            <th style="width: 9%; background-color: #00cc00">
              Apellidos </th>
            <th style="width: 9%; background-color: #47ff47">
              Nombres </th>
            <% =DiasDelMes(cbxMes.SelectedIndex) %>
        </tr>
    </thead>
    <%var j = 1;
    foreach (var item in ListaAlumnos)
    {%>
    <tbody>
        <tr>
            <th style="width: 4%"><% = j%></th>
            <th style="width: 14%"><% = item.Apellido %></th>
            <th style="width: 14%"><% = item.Name %></th>
            <% = AsistenciasDelMes(item,cbxMes.SelectedIndex)%>
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
