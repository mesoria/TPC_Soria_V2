<%@ Page Title="Mensajería" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Messenger.aspx.cs" Inherits="TPC_Soria_v2.FolderFormularios.Messenger" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%:Title%>.</h2>
    <div class="form-row">
        <div class="form-group col-md-12">
            <asp:Label ID="lblTDescripcion" CssClass="text-body" runat="server" Text="Seleccione grupo de destinatarios entre compañeros o alumnos."></asp:Label>
        </div>
        <div class="form-group col-md-12">
            <asp:DropDownList ID="DDLDestinatari" runat="server" OnSelectedIndexChanged="DDLDestinatari_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Value="0">Alumnos del curso</asp:ListItem>
                <asp:ListItem Value="1">Empleados del establecimiento</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <asp:GridView CssClass="table table-success" ID="dgvReceiver" runat="server" AutoGenerateColumns="false">
        <Columns>  
            <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
            <asp:BoundField DataField="Name" HeaderText="Nombre" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:TemplateField HeaderText="Enviar Email">
                    <ItemTemplate >
                        <asp:CheckBox ID="cbxEnviar" CssClass="custom-checkbox" runat="server" AutoPostBack="false"  Checked='false' />
                    </ItemTemplate>
            </asp:TemplateField>
        </Columns>  
    </asp:GridView>
    <br />
    <hr />
    <div>
        <div class="form-group col-md-12">
            <asp:Label ID="lblemail" runat="server" Text="De: "></asp:Label>
            <asp:TextBox ID="txtMeEmail" runat="server" CssClass="border-success form-control"></asp:TextBox>
        </div>
    </div>
    <div>
        <div class="form-group col-md-12">
            <asp:Label ID="lblContraseña" runat="server" Text="Contraseña: "></asp:Label>
            <asp:TextBox ID="txtPass" type="Password" runat="server" CssClass="border-success form-control">Mi email</asp:TextBox>
        </div>
    </div>
    <div>
        <div class="form-group col-md-12">
            <asp:Label ID="lblAsunto" runat="server" Text="Asunto: "></asp:Label>
            <asp:TextBox ID="txtAsunto" runat="server" CssClass="border-success form-control"> </asp:TextBox>
        </div>
    </div>
    <div>
        <div class="form-group col-md-12">
            <asp:Label ID="lblCuerpo" runat="server" Text="Mensaje: "></asp:Label>
            <asp:TextBox ID="txtBody" runat="server" CssClass="border-success form-control" TextMode="MultiLine" Rows="10"> </asp:TextBox>
        </div>
    </div>

    <br />
    <hr />

    <div class="form-row">
        <div class="form-group col-md-5">
            <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-primary btn-lg" Text="Volver" />
        </div>
        <div class="form-group col-md-7">
            <asp:Button ID="btnEnviar" runat="server" OnClick="btnEnviar_Click" Text="Enviar Email" CssClass="btn btn-success btn-lg"/>
        </div>
    </div>
</asp:Content>
