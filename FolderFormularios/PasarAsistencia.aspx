<%@ Page Title="Lista de Alumnos." Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PasarAsistencia.aspx.cs" Inherits="TPC_Soria_v2.FolderFormularios.PasarAsistencia" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-row">
        <div class="form-group col-md-10">
            <asp:Label ID="lblApellido" runat="server" Font-Size="X-Large" Text=""><% = persona.Apellido %></asp:Label>
            <asp:Label ID="lvlName" runat="server" Font-Size="X-Large" Text=""><% = persona.Name %> </asp:Label>
        </div>
        <div class="form-group col-md-2">
            <asp:Label ID="Date" CssClass="align-items-end" runat="server" Font-Size="X-Large" Text=""><% = DateTime.Today.Year+"-"+DateTime.Today.Month+"-"+DateTime.Today.Day%></asp:Label>
        </div>
    </div>
    <h2><%: Title %></h2>

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
            <th style="width: 10%; background-color: #77ff77">
              Presente </th>
        </tr>
    </thead>
        <asp:Repeater runat="server" ID="repetidor">
            <ItemTemplate>
    <tbody>
                <tr>
                    <th style="width: 5%"><% %></th>
                    <th style="width: 15%"><%#Eval("Apellido")%></th>
                    <th style="width: 15%"><%#Eval("Name")%></th>
                    <th>
                        <asp:CheckBox ID="cbxArgument" CssClass="custom-switch" runat="server" CommandArgument='<%#Eval("ID")%>' OnCheckedChanged="cbxArgument_CheckedChanged"/>
                        <asp:Button ID="btnArgumento" CssClass="btn btn-primary" runat="server" CommandArgument='<%#Eval("Id")%>' CommandName="idBtn"  OnClick="btnArgumento_Click" Text="Presente"/>
                    </th>
                </tr>
    </tbody>
            </ItemTemplate>
        </asp:Repeater>

    <%--<%var j = 1;
    foreach (var item in ListaAlumnos)
    {%>
    <tbody>
        <tr>
            <th style="width: 5%"><% = j%></th>
            <th style="width: 15%"><% = item.Apellido %></th>
            <th style="width: 15%"><% = item.Name %></th>
            <th>
                <div class="custom-control custom-checkbox">
                    <%--<input type ="checkbox" runat="server" class="custom-control-input" id=<% = j%>>--%>
                    <%--<asp:CheckBox CssClass="custom-checkbox btn-lg" ID="<%=item.ID %>" runat="server"/>
                </div>
            </th>
       <% j++;}%>
        </tr>
    </tbody>--%>
    </table>
</div>
    <br />
    <hr />

    <div class="form-row">
        <div class="form-group col-md-5">
            <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-primary btn-lg" Text="Volver" />
        </div>
        <div class="form-group col-md-7">
            <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" Text="Guardar" CssClass="btn btn-success btn-lg"/>
        </div>
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
