﻿<%@ Page Title="Lista de Alumnos." Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PasarAsistencia.aspx.cs" Inherits="TPC_Soria_v2.FolderFormularios.PasarAsistencia" EnableEventValidation="false"%>
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
    <asp:GridView CssClass="table table-success" ID="dgvAlumnos" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>  
            <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
            <asp:BoundField DataField="Name" HeaderText="Nombre" />
            <%--<asp:BoundField DataFormatString="{0:C}" DataField="Precio" HeaderText="Precio" />--%>
            <asp:TemplateField HeaderText="Presente">
                    <ItemTemplate >
                        <asp:CheckBox ID="cbxPresente" CssClass="custom-checkbox" runat="server" AutoPostBack="false"  Checked='false' />
                    </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="IDAlumno" HeaderText="IDA" Visible="false" />
        </Columns>  
        <EditRowStyle BackColor="#7C6F57" />
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#E3EAEB" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F8FAFA" />
        <SortedAscendingHeaderStyle BackColor="#246B61" />
        <SortedDescendingCellStyle BackColor="#D4DFE1" />
        <SortedDescendingHeaderStyle BackColor="#15524A" />
    </asp:GridView>
    <br />
    <hr />

    <div class="form-row">
        <div class="form-group col-md-5">
            <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-primary btn-lg" Text="Volver" />
        </div>
        <div class="form-group col-md-7">
            <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" Text="Guardar presentismo" CssClass="btn btn-success btn-lg"/>
        </div>
    </div>
</asp:Content>
