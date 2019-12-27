<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MostrarCalificaciones2.aspx.cs" Inherits="TPC_Soria_v2.FolderFormularios.MostrarCalificaciones2" %>
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
    <asp:GridView CssClass="table table-success" ID="dgvAlumnos" runat="server" AutoGenerateColumns="False"
        OnRowCancelingEdit="dgvAlumnos_RowCancelingEdit" 
        OnRowEditing="dgvAlumnos_RowEditing"
        OnRowUpdating="dgvAlumnos_RowUpdating" 
        DataKeyNames="IdAlumno"
        CellPadding="4" ForeColor="#333333" GridLines="None">

        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="Apellido" HeaderText="Apellido" ReadOnly="true"/>
            <asp:BoundField DataField="Name" HeaderText="Nombre" ReadOnly="true"/>
            <asp:TemplateField HeaderText="Primer trimestre">
                <ItemTemplate>
                    <asp:Label ID="lblNota1" runat="server" Text='<% # Bind("Calificaciones.Letras.Letra1")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="txtNota1" runat="server"
                        SelectedValue='<% # Bind("Calificaciones.Letras.Letra1")%>' >
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>R</asp:ListItem>
                        <asp:ListItem>B</asp:ListItem>
                        <asp:ListItem>MB</asp:ListItem>
                        <asp:ListItem>S</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Segundo Trimestre">
                <ItemTemplate>
                    <asp:Label ID="lblNota2" runat="server" Text='<% # Bind("Calificaciones.Letras.Letra2")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="txtNota2" runat="server"
                        SelectedValue='<% # Bind("Calificaciones.Letras.Letra2")%>' >
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>R</asp:ListItem>
                        <asp:ListItem>B</asp:ListItem>
                        <asp:ListItem>MB</asp:ListItem>
                        <asp:ListItem>S</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Tercer Trimestre">
                <ItemTemplate>
                    <asp:Label ID="lblNota3" runat="server" Text='<% # Bind("Calificaciones.Letras.Letra3")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="txtNota3" runat="server"
                        SelectedValue='<% # Bind("Calificaciones.Letras.Letra3")%>' >
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>R</asp:ListItem>
                        <asp:ListItem>B</asp:ListItem>
                        <asp:ListItem>MB</asp:ListItem>
                        <asp:ListItem>S</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ButtonType="Link" ShowEditButton="true" />
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
    </div>
</asp:Content>
