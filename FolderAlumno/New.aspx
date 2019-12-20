<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="New.aspx.cs" Inherits="TPC_Soria_v2.FolderAlumno.New" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function validar() {
            var txtDNI = $('#txtDNI').val();   
            if (!txtDNI)
            {
                alert('Debes ingresar una cantidad!');
                return false;
            }
            else if (isNaN(txtDNI) || txtDNI % 1 !== 0)
            {
                alert('El campo "DNI" debe contener un numero entero!');
                return false;
            }
            var txtAltura = $('#txtAltura').val();
            if (!txtAltura) {
                alert('Debes ingresar una cantidad!');
                return false;
            }
            else if (isNaN(txtAltura) || txtAltura % 1 !== 0) {
                alert('El campo "Altura" debe contener un numero entero!');
                return false;
            }
            var txtTDNI = $('#txtTDNI').val();
            if (!txtTNombre) {
                alert('Debes ingresar una cantidad!');
                return false;
            }
            else if (isNaN(txtTDNI) || txtTDNI % 1 !== 0) {
                alert('El campo "DNI" debe contener un numero entero!');
                return false;
            }
            var txtTAltura = $('#txtTAltura').val();
            if (!txtAltura) {
                alert('Debes ingresar una cantidad!');
                return false;
            }
            else if (isNaN(txtTAltura) || txtTAltura % 1 !== 0) {
                alert('El campo "Altura" debe contener un numero entero!');
                return false;
            }
            return true;
        }
    </script>
    <%--<h3> <% =FirstTime() %></h3>--%>
    <br />
    <h6>Información personal</h6>
    <div class="form-row">
        <div class="form-group col-md-6">
            <label>Nombres</label>
            <%--<asp:TextBox ID="txtNombre" runat="server" required></asp:TextBox>--%>
            <input type="Text" class="form-control" id="txtNombre"  runat="server" placeholder="Juan" required>
            
        </div>
        <div class="form-group col-md-6">
            <label>Apellidos</label>
            <%--<asp:TextBox ID="txtNombre" runat="server" required></asp:TextBox>--%>
            <input type="Text" class="form-control" id="txtApellido"  runat="server" placeholder="Perez" required>
        </div>
    </div>

    <div class="form-row">
        <div class="form-group col-md-4">
            <label>DNI</label>
            <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" OnTextChanged="txtDNI_TextChanged" AutoPostBack="true" type="number" min="1" CausesValidation="true" required="true"></asp:TextBox>
            <%--<input type="Text" class="form-control" id="txtDNI"  runat="server" placeholder="30111222">--%>
        </div>
        <div class="form-group col-md-4">
            <label for="txtNacimiento">Fecha de nacimiento</label>
            <input type="date" id="txtNacimiento" name="trip-start" runat="server"
                value="1987/08/06"
                min="1950/01/01" max="2013/12/31" class="form-control" required>
        </div>
    </div>
    <div class="form-row">
        <div class="form-group col-md-8">
            <label for="txtCalle">Dirección</label>
            <input type="text" class="form-control" id="txtCalle" runat="server" placeholder="Balam Garden" required>
        </div>
        <div class="form-group col-md-4">
            <label for="txtAltura">Número</label>

            <asp:TextBox ID="txtAltura" runat="server" CssClass="form-control"  type="number" min="1" CausesValidation="true" required="true"></asp:TextBox>
            <%--<input class="form-control" id="txtAltura" runat="server" placeholder="1234" type="text" min="1" CausesValidation="true" required>--%>
            <%--<small class="form-text text-muted">Altura</small>--%>
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
            <input type="text" class="form-control" id="txtEmail" runat="server" placeholder="name@example.com.ar" required>
            </div>
    </div>
    <hr />
    <br />
    <h6>Tutor</h6>
    <h5>Información del encargado del alumno</h5>
    <div class="form-row">
        <div class="form-group col-md-6">
            <label>Nombres</label>
            <%--<asp:TextBox ID="txtNombre" runat="server" required></asp:TextBox>--%>
            <input type="Text" class="form-control" id="txtTNombre"  runat="server" placeholder="Juan" required>
            
        </div>
        <div class="form-group col-md-6">
            <label>Apellidos</label>
            <%--<asp:TextBox ID="txtNombre" runat="server" required></asp:TextBox>--%>
            <input type="Text" class="form-control" id="txtTApellido"  runat="server" placeholder="Perez" required>
        </div>
    </div>
    <div class="form-row">
        <div class="form-group col-md-4">
            <label>DNI</label>
                <asp:TextBox ID="txtTDNI" runat="server" type="number" min="1" CausesValidation="true" required="true"></asp:TextBox>
            <%--<input class="form-control" id="txtTDNIq"  runat="server" placeholder="30111222" type="number" min="1" CausesValidation="true">--%>
        </div>
        <div class="form-group col-md-4">
            <label for="txtTNac">Fecha de nacimiento</label>
            <input type="date" id="txtTNac" name="trip-start" runat="server"
                value="1987/08/06"
                min="1950/01/01" max="2013/12/31" class="form-control">
        </div>
    </div>
    <div class="form-row">
        <div class="form-group col-md-8">
            <label for="txtTCalle">Dirección</label>
            <input type="text" class="form-control" id="txtTCalle" runat="server" placeholder="Balam Garden" required>
            <small class="form-text text-muted">Calle</small>
        </div>
        <div class="form-group col-md-4">
            <%--<label for="txtTAltura">Número</label>--%>
            <label>Número</label>
            <asp:TextBox class="form-control"  ID="txtTAltura" runat="server" type="number" min="1" CausesValidation="true" required="true"></asp:TextBox>
            <%--<input class="form-control" id="txtTAltura" runat="server" placeholder="1234" type="number" min="1" CausesValidation="true" required>--%>
            <small class="form-text text-muted">Altura</small>
        </div>
    </div>
    <hr />
    <br />
    <h6>Contacto</h6>
    <div class="form-group col-md-6">
        <label for="txtTEmail">Email</label>
            <div class="input-group">
            <div class="input-group-prepend">
                <div class="input-group-text">@</div>
            </div>
            <input type="text" class="form-control" id="txtTEmail" runat="server" placeholder="name@example.com.ar" required>
            </div>
    </div>
    <hr />
    <div class="form-row">
        <div class="form-group col-md-5">
            <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-primary btn-lg" Text="Volver" />
        </div>
        <div class="form-group col-md-7">
            <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" Text="Guardar" CssClass="btn btn-success btn-lg"/>
        </div>
    </div>

</asp:Content>
