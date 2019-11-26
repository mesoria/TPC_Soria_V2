<%@ Page Title="Establecimientos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Establecimientos.aspx.cs" Inherits="TPC_Soria_v2.Establecimientos" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <br />
    <div>
        <asp:Label ID="lblApellido" runat="server" CssClass="align-content-lg-end" Font-Size="X-Large" Text=""><% = usuario.Perfil %>   </asp:Label>
        <asp:Label ID="lvlName" runat="server" cssClass="align-content-lg-end" Font-Size="X-Large" Text=""><% = GetName() %> </asp:Label><br /><br />
    </div>

    <nav aria-label="breadcrumb">
      <ol class="breadcrumb">
        <li class="breadcrumb-item active" aria-current="page">Establecimientos</li>
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
    foreach (var item in ListEstablecimientos)
    {%>
    <tbody>
        <tr>
            <th style="width:  5%"><% = j%></th>
            <th style="width: 15%"><% = item.Nivel %></th>
            <th style="width: 15%"><% = item.Name %></th>
            <th style="width: 15%"><% = item.Number %></th>
            <th style="width: 15%"> <a class="btn btn-primary" href="FolderEstablecimiento/Info.aspx?idE=<% = item.ID %>">Más información</a> </th>
        <%j++;}%>
        </tr>
    </tbody>
    </table>
</div>
    <hr />
    <div class="form-row">
        <div class="form-group col-md-5">
            <a class="btn btn-primary" href="FolderEstablecimiento/NewEstablecimiento.aspx?idM=<% = maestra.ID %>">Nuevo Establecimiento</a>
        </div>
        <%--<div class="form-group col-md-7">
            <a class="btn btn-primary" href="../Curso/Cursos.aspx?idM=<% = maestra.ID %>">Ir a Cursos</a>
        </div>--%>
    </div> 

</asp:Content>
