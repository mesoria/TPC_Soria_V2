﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Validation.aspx.cs" Inherits="TPC_Soria_v2.FolderAlumno.Validation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        a, a:visited
{
	border:none 0px;
	color:white;
	margin:0px;
	padding:0px;
	text-decoration:none;	
}
a:hover
{
	text-decoration:underline;
}

body 
{
	background-color:#F4F4F4;
	color:black;
	font-family: Lucida Grande,Arial,sans-serif;
	font-size:68%;
	margin: 10px;
	padding: 0;
}

img
{
	border:none 0px;
	margin:0px;
	padding:0px;
	float:left;
}
.cFL 
{
	float:left;
}
.cFR 
{
	float: right;
}
.cPosRel 
{
	position: relative;
}
.cPosAbs 
{
	position: absolute;
}
/*--------Empiezan Clases para el Login----------*/
.TextoMensaje
{
	font-size: 14px;
	color: #748ab3;
	font-family: Arial;
	background-color: transparent;
}
.TextoPregunta
{
	font-size: 16px;
	color: #748ab3;
	font-family: Arial;
    font:600;
	background-color: transparent;
}
.TextoLogin
{
	font-weight: bold;
	font-size: 11px;
	color: #717171;
	font-family: Arial;
}
.txtRegistro
{
	border-left: 1px solid #8E8E8E;
	border-right: 1px solid #B8B8B8;
	border-top: 1px solid #8E8E8E;
	border-bottom: 1px solid #B8B8B8;
	float:left;
	margin-left:14px;
	width:64%;
}
.txtCapit
{
	text-transform:capitalize;
}
.txtMultiline
{
	border-left: 1px solid #8E8E8E;
	border-right: 1px solid #B8B8B8;
	border-top: 1px solid #8E8E8E;
	border-bottom: 1px solid #B8B8B8;
	font-family: Lucida Grande,Arial,sans-serif;
	margin:5px 10px 11px 10px;
	text-align:center;
	width:80%;
}
.txtMultiline2
{
	border-left: 1px solid #8E8E8E;
	border-right: 1px solid #B8B8B8;
	border-top: 1px solid #8E8E8E;
	border-bottom: 1px solid #B8B8B8;
	float:left;
	margin-left:14px;
	text-align:center;
	width:80%;
}
.txtSingleLine
{
	border-left: 1px solid #8E8E8E;
	border-right: 1px solid #B8B8B8;
	border-top: 1px solid #8E8E8E;
	border-bottom: 1px solid #B8B8B8;
	height:16px;
	margin:5px 10px 11px 10px;
	width:80%;
}
/*Clases para formar el Login*/
.FormaLogin
{
	height:356px;
	width:530px;
	margin:80px auto auto auto;
}
.LeftFormaLogin
{
	background-image: url('../Images/login/izquierda.png');
	background-repeat: no-repeat;
	height: 356px;
	width: 22px;
	float:left;
}
.CenterFormaLogin
{
	background-image: url('../Images/login/centro.png');
	background-repeat:repeat-x;
	height: 356px;
	width: 450px;
	float:left;
}
.RightFormaLogin
{
	background-image: url('../Images/login/derecha.png');
	background-repeat: no-repeat;
	height: 356px;
	width: 41px;
	float:left;
}
.txtControl 
{
	border-top-style: none;
	font-family: Arial;
	border-right-style: none;
	border-left-style: none;
	background-color: #D7D7D7;
	border-bottom-style: none;
	margin-left:10px;
	width: 205px;
	height: 22px;
}

/*--------Terminan Clases para el Login----------*/

    </style>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="FormaLogin">
        <div class="LeftFormaLogin">
        </div>
        <div class="CenterFormaLogin">
            <div class="cPosRel" style="width: 370px; height: 90px; margin: 34px auto 0px auto; text-align:justify center;">
                <span class="TextoMensaje">Alumno ya existente en la base de datos.</span>
                <br />
                <br />
                <br />
                <span class="TextoPregunta">¿Quiére ingresar este alumno?</span>
            </div>
            <div class="cFL cPosRel" style="width: 450px; height: 150px;">
                <div class="cFL cPosRel" style="width: 100px; height: 25px;">
                    <span class="cFR cPosRel TextoLogin" style="margin-top: 7px;">DNI:</span>
                </div>
                <div class="cFL cPosRel" style="width: 290px; height: 25px;">
                    <asp:textBox ID="lblDNI" CssClass="txtControl" runat="server"></asp:textBox>
                </div>
                <div class="cFL cPosRel" style="width: 100px; height: 25px; margin-top: 15px;">
                    <span class="cFR cPosRel TextoLogin" style="margin-top: 7px;">Alumno:</span>
                </div>
                <div class="cFL cPosRel" style="width: 290px; height: 25px; margin-top: 15px;">
                    <asp:textBox ID="lblApellido" CssClass="txtControl" runat="server" ReadOnly="true"></asp:textBox>
                </div>
               
                <div class="cFL cPosRel" style="width: 120px; height: 25px; margin-top: 15px; margin-left: 115px">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" onclick="btnCancelar_Click" />
                </div>
                <div class="cFL cPosRel" style="width: 200px; height: 25px; margin-top: 15px;">
                        <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" Text="Aceptar" ReadOnly="true"/>                
                </div>
            </div>
            <asp:Label ID="lblMensaje" CssClass="cFL" runat="server" ForeColor="#996600"></asp:Label>
        </div>
        <div class="RightFormaLogin">
        </div>
    </div>
    </form>
</body>
</html>
