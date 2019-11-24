<%@ Page Title="Alumnos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AsistenciaAlumnos.aspx.cs" Inherits="TPC_Soria_v2.AsistenciaAlumnos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Label ID="lblApellido" runat="server" Font-Size="X-Large" Text=""><% = maestra.Apellido %></asp:Label>
        <asp:Label ID="lvlName" runat="server" Font-Size="X-Large" Text=""><% = maestra.Name %> </asp:Label>
    </div>
    <h2><%: Title %></h2>

    <nav aria-label="breadcrumb">
      <ol class="breadcrumb">
        <li class="breadcrumb-item"><a href="MaestraPrincipal.aspx?idM=<% = maestra.ID %>">Escuelas</a></li>
        <li class="breadcrumb-item"><a href="../Curso/Cursos.aspx?idM=<% = maestra.ID %>">Cursos</a></li>
        <li class="breadcrumb-item active" aria-current="page">Alumnos</li>
      </ol>
    </nav>
    <h3>Lista de Alumnos.</h3>
             
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
            <% =Dias() %>
        </tr>
    </thead>
    <%var j = 1;
    foreach (var item in Personas)
    {%>
    <tbody>
        <tr>
            <th style="width: 5%"><% = j%></th>
            <th style="width: 15%"><% = item.Apellido %></th>
            <th style="width: 15%"><% = item.Name %></th>
            <% = Asistencias(item)%>
        <%if (DateTime.Today.DayOfWeek.ToString() != "Saturday" && DateTime.Today.DayOfWeek.ToString() != "Sunday")
        {%>
            <th>
                <div class="custom-control custom-checkbox">
                    <%--<input type ="checkbox" runat="server" class="custom-control-input" id=<% = j%>>--%>
                    <asp:CheckBox CssClass="custom-checkbox btn-lg" ID="checkbox" runat="server"/>
                </div>
            </th>
        <%}
        j++;}%>
        </tr>
    </tbody>
    </table>
</div>
    <div>
        <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" Text="Guardar" CssClass="btn btn-primary btn-lg"/>
    </div>
    <div>
        <a class="btn btn-primary" href="../Curso/Cursos.aspx?idM=<% = maestra.ID %>">Atras</a>
    </div>
    <address>
        One Microsoft Way<br />
        Redmond, WA 98052-6399<br />
        <abbr title="Phone">P:</abbr>
        425.555.0100
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:Support@example.com">Support@example.com</a><br />
        <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">Marketing@example.com</a>
    </address>
</asp:Content>
