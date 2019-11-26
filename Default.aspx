<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPC_Soria_v2._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Mi Escuela</h1>
        <asp:Label ID="txtPerfil" runat="server" Text="Label"><% = usuario.Perfil %></asp:Label>
        <asp:Label ID="txtNombre" runat="server" Text="Label"><% = persona.Name+" "+persona.Apellido %></asp:Label>
        <p class="lead">Muchas gracias por utilizar <i>Mi escuela.</i>.</p>
        <%--<p><a href="https://www.youtube.com/channel/UCEDh0cNhzRa9_pDwdyrjjkQ" class="btn btn-primary btn-lg">Ir al canal &raquo;</a></p>--%>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Establecimientos.</h2>
            <p>
                Encontraras información tanto de escuelas primarias como secundarias y también de facultades y universidades.</p>
            <p>
                <a class="btn btn-default" href="~/Establecimientos">Ir a la lista de reproducción. &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Cursos</h2>
            <p>
                Encontraras información de tanto grados de una escuela como materias de una facultad o universidad.
            </p>
            <p>
                <a class="btn btn-default" href="~/Cursos">Ir a la lista de reproducción. &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Usuarios</h2>
            <p>
                Alumnos pertenecientes a tus cursos.
            </p>
            <p>
                <a class="btn btn-default" href="~/Docentes">Learn more &raquo;</a>
                <%--<a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>--%>
            </p>
        </div>
    </div>
    <div class="jumbotron">
        <p class="lead">Si ya viste mis videos y contas con un <i>Voucher</i> Ingresalo para participar de fabulosos premios.</p>
        <p><a runat="server" href="~/Premios.aspx" class="btn btn-primary btn-lg">Participar &raquo;</a></p>
    </div>
    
    
    
    
    
    
    
</asp:Content>
