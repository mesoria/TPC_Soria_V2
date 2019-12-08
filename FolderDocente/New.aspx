<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="New.aspx.cs" Inherits="TPC_Soria_v2.FolderDocente.New" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
         function validar() {
             var DNI = $('#txtDNI').val();
             if (!cantidad)
             {
                 alert('Debes ingresar una DNI!');
                 return false;

             }
             else if (isNaN(DNI) || DNI % 1 !== 0)
             {
                 alert('El campo "DNI" debe contener un numero entero!');
                 return false;
             }
             var Altura = $('#txtAltura').val();
             if (!Altura) {
                 alert('Debes ingresar una Altura!');
                 return false;

             }
             else if (isNaN(Altura) || Altura % 1 !== 0) {
                 alert('El campo "Altura" debe contener un numero entero!');
                 return false;
             }
         return true;
         }
    </script>

    <h3> <% =FirstTime() %></h3>
    <br />
    <h6>Información personal</h6>
    <div class="form-row">
        <div class="form-group col-md-6">
            <label>Nombres</label>
            <%--<asp:TextBox ID="txtNombre" runat="server" required></asp:TextBox>--%>
            <input type="Text" class="form-control" id="txtNombre"  runat="server" placeholder="Squall">
            
        </div>
        <div class="form-group col-md-6">
            <label>Apellidos</label>
            <%--<asp:TextBox ID="txtNombre" runat="server" required></asp:TextBox>--%>
            <input type="Text" class="form-control" id="txtApellido"  runat="server" placeholder="Lionheart">
        </div>
    </div>
    <div class="form-row">
        <div class="form-group col-md-4">
            <label>DNI</label>
                <%--<asp:TextBox ID="txtNombre" runat="server" required></asp:TextBox>--%>
            <input type="Text" class="form-control" id="txtDNI"  runat="server" placeholder="30111222">
        </div>
        <div class="form-group col-md-4">
            <label for="txtNacimiento">Fecha de nacimiento</label>
            <input type="date" id="txtNacimiento" name="trip-start" runat="server"
                value="1987/08/06"
                min="1950/01/01" max="2013/12/31" class="form-control">
        </div>
    </div>
    <div class="form-row">
        <div class="form-group col-md-8">
            <label for="txtCalle">Dirección</label>
            <input type="text" class="form-control" id="txtCalle" runat="server" placeholder="Balam Garden">
            <small class="form-text text-muted">Calle</small>
            <div class="invalid-feedback">
                Por favor, ingrese la calle.
            </div>
        </div>
        <div class="form-group col-md-4">
            <label for="txtAltura">Número</label>
            <input type="text" class="form-control" id="txtAltura" runat="server" placeholder="1234">
            <small class="form-text text-muted">Altura</small>
            <div class="invalid-feedback">
                Por favor, ingrese la altura.
            </div>
        </div>
    </div>
    <hr />
    <br />
    <h6>Contacto</h6>
    <div class="form-group col-md-6">
        <label for="txtEmail">Email</label>
            <div class="input-group">
            <div class="input-group-prepend">
                <div class="input-group-text">@</div>
            </div>
            <input type="text" class="form-control" id="txtEmail" runat="server" placeholder="name@example.com.ar">
            </div>
    </div>
    <hr />
    <br />
    <h6>Nivel Académico</h6>
    <div class="form-row">
        <div class="form-group col-md-6">
            <label for="cbxNivel">Nivel</label>
            <select id="cbxNivel" runat="server" class="form-control">
                <option selected>Primaria</option>
                <option>Secundaria</option>
                <option>Facultad</option>
                <option>Universidad</option>
            </select>
        </div>
    </div>
    <hr />
    <div class="form-row">
        <div class="form-group col-md-5">
            <asp:Button ID="btnVolver" runat="server" OnClick="btnVolver_Click" CssClass="btn btn-primary btn-lg" Text="Docentes"/>
        </div>
        <div class="form-group col-md-7">
            <asp:Button ID="btnGuardar" runat="server" Onclick="btnGuardar_Click" Text="Guardar" CssClass="btn btn-success btn-lg"/>
        </div>
    </div>


</asp:Content>
