<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CompuGross_Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>CompuGross - Login</title>

    <link rel="Shortcut Icon" type="image/x-icon" href="favicon.ico" />

    <%--Sweet Alert--%>
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@10"></script>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

    <link href="Styles/Style_Login.css?v=2.0" rel="stylesheet" />

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.slim.min.js" integrity="sha256-u7e5khyithlIdTpu22PHhENmPcRdFiHRjhAuHcs05RI=" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />

    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin="" />
    <link href="https://fonts.googleapis.com/css2?family=Kaushan+Script&display=swap" rel="stylesheet" />

    <link rel="stylesheet" type="text/css" href="https://csshake.surge.sh/csshake.min.css"/>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" />

    <link href="https://cdn.jsdelivr.net/npm/alertifyjs@1.11.0/build/css/alertify.min.css" rel="stylesheet"/>
    <script src="https://cdn.jsdelivr.net/npm/alertifyjs@1.11.0/build/alertify.min.js"></script>

</head>
<body>
    <form id="frmLogin" runat="server">
        <section id="section_titulo">

            <center>

                <asp:Image ImageUrl="~/img/LogosCG/LogoCG.png" class="stl-img-logo" runat="server" />

                <h1 class="stl-titulo-imagen">CompuGross</h1>

            </center>

        </section>

        <br /><br />

        <section id="section_login" runat="server"> <%--Login--%>

            <center>

                <h3 class="stl-label-campos">Usuario</h3>
                <asp:TextBox id="TxtUsuario" placeholder="Usuario" class="stl-campo-texto text-white" runat="server" />
                
                <br /><br />

                <h3 class="stl-label-campos">Contraseña</h3>
                <asp:TextBox id="TxtClave" placeholder="Contraseña" class="stl-campo-texto text-white" runat="server" TextMode="Password" />

                <br />

                <input id="CbMostrarClave" type="checkbox" class="stl-checkbox-mostrar-clave" onclick="mostrarContrasena()" />
                <span class="text-white stl-link-recuperar-clave"> Mostrar Contraseña</span>

                <br /><br />

                <u>
                    <asp:LinkButton ID="LinkLabelRecuperarClave" Text="Olvidé mi contraseña" class="text-white stl-link-recuperar-clave" runat="server" onclick="LinkLabelRecuperarClave_Click" />
                </u>

                <br /><br />

                <asp:Button ID="BtnIngresar" Text="Ingresar" class="btn btn-success btn-lg stl-botones" runat="server" onclick="BtnIngresar_Click" />

            </center>

        </section>

        <section id="section_recuperar_clave" runat="server" style="display: none;"> <%--Recuperar Clave--%>

            <center>

                <asp:Label ID="LblRecuperarClave" Text="Ingrese su Usuario" runat="server" class="stl-label-campos" />
                
                <br />
                
                <asp:TextBox id="TxtRecuperarClave" class="stl-campo-texto text-white" runat="server" />

                <br /><br /><br />

                <asp:Button ID="BtnEnviarCodigo" Text="Enviar Código" class="btn btn-primary btn-lg stl-botones" runat="server" OnClick="BtnEnviarCodigo_Click" />

                <asp:Button ID="BtnCancelarRecuperarClave" Text="Cancelar" CssClass="btn btn-danger btn-lg stl-botones" runat="server" OnClick="BtnCancelarRecuperarClave_Click" />

            </center>

        </section>

        <section id="section_validar_codigo" runat="server" style="display: none;"> <%--Validar Codigo--%>

            <center>

                <asp:Label ID="LblValidarCodigo" Text="Ingrese el código enviado a su mail" runat="server" class="stl-label-campos" />
                
                <br />
                
                <asp:TextBox id="TxtValidarCodigo" class="stl-campo-texto text-white" runat="server" />

                <br /><br /><br />

                <asp:Button ID="BtnValidarCodigo" Text="Validar Código" class="btn btn-primary btn-lg stl-botones" runat="server" OnClick="BtnValidarCodigo_Click" />

                <asp:Button ID="BtnCancelarRecuperarClave2" Text="Cancelar" CssClass="btn btn-danger btn-lg stl-botones" runat="server" OnClick="BtnCancelarRecuperarClave_Click" />

            </center>

        </section>

        <section id="section_cambiar_clave" runat="server" style="display: none;"> <%--Cambiar Clave--%>

            <center>

                <asp:Label ID="LblCambiarClave" Text="Ingrese su nueva Clave" runat="server" class="stl-label-campos" />
                
                <br />
                
                <asp:TextBox id="TxtCambiarClave" class="stl-campo-texto text-white" runat="server" TextMode="Password" />

                <br />

                <input id="CbMostrarClave2" type="checkbox" class="stl-checkbox-mostrar-clave" onclick="mostrarContrasena2()" />
                <span class="text-white stl-link-recuperar-clave"> Mostrar Contraseña</span>

                <br /><br /><br />

                <asp:Button ID="BtnCambiarClave" Text="Cambiar Clave" class="btn btn-primary btn-lg stl-botones" runat="server" OnClick="BtnCambiarClave_Click" />

                <asp:Button ID="BtnCancelarRecuperarClave3" Text="Cancelar" CssClass="btn btn-danger btn-lg stl-botones" runat="server" OnClick="BtnCancelarRecuperarClave_Click" />

            </center>

        </section>

        <asp:HiddenField ID="hfMessage" runat="server" />
        <asp:HiddenField ID="hfError" runat="server" />

    </form>
</body>

    <script>
        function alertaConfirm() {
            var message = $('[id$=hfMessage]').val()

            if ($('[id$=hfMessage]').val() != '') {
                Swal.fire({
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    allowEnterKey: false,
                    title: message,
                    icon: 'success',
                })
            }
            $('[id$=hfMessage]').val('')
        }

        function errorConfirm() {
            var message = $('[id$=hfError]').val()

            if ($('[id$=hfError]').val() != '') {
                Swal.fire({
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    allowEnterKey: false,
                    title: message,
                    icon: 'error',
                })
            }
            $('[id$=hfError]').val('')
        }

        $(function () {
            errorConfirm();
            alertaConfirm();
        });

        function mostrarContrasena() {
            var tipo = document.getElementById("TxtClave");
            if (tipo.type == "password") {
                tipo.type = "text";
            } else {
                tipo.type = "password";
            }
        }

        function mostrarContrasena2() {
            var tipo = document.getElementById("TxtCambiarClave");
            if (tipo.type == "password") {
                tipo.type = "text";
            } else {
                tipo.type = "password";
            }
        }

    </script>

</html>
