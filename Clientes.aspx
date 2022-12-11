<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="CompuGross_Web.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <link href="Styles/Style_Clientes.css?v=2.0" rel="stylesheet" />
    <script defer src="JavaScript/Script_Clientes.js" ></script>

    <div class="stl-div-content">

        <section id="section_botones_principales" class="stl-section-botones-principales activo" runat="server">

            <center>
                <h1>Clientes</h1>
            </center>

            <div class="stl-div-botones-principales">
                <span id="AgregarCliente" class="btn btn-dark stl-boton-principal agregar-cliente" runat="server">
                        Nuevo Cliente
                </span>
                <span id="ModificarCliente" class="btn btn-dark stl-boton-principal modificar-cliente" runat="server">
                        Modificar Cliente
                </span>
                <span id="EliminarCliente" class="btn btn-dark stl-boton-principal eliminar-cliente" runat="server">
                        Eliminar Cliente
                </span>
                <span id="Localidades" class="btn btn-dark stl-boton-principal abm-localidades" runat="server">
                        ABM Localidades
                </span>
            </div>
        </section>

        <section id="section_agregar_cliente" runat="server" class="stl-section-agregar-cliente">
            <div class="stl-div-contenedor-campos">
                <div class="stl-div-titulo">
                    <h1>Nuevo Cliente</h1>
                </div>
                <br />
                <div class="stl-div-campos">
                    <div class="stl-div-cuit-dni">
                        <asp:Label ID="LblNuevoClienteCuitDni" Text="CUIT/DNI" runat="server" CssClass="stl-label-campo" />
                        <asp:TextBox ID="TxtNuevoClienteCuitDni" placeholder="CUIT/DNI" runat="server" CssClass="stl-texto-campo form-control" />
                    </div>
                    <div class="stl-div-apenom">
                        <asp:Label ID="LblNuevoClienteApenom" Text="Apellido y Nombre" runat="server" CssClass="stl-label-campo" />
                        <asp:TextBox ID="TxtNuevoClienteApenom" placeholder="Apellido y Nombre" runat="server" CssClass="stl-texto-campo form-control" />
                    </div>
                    <div class="stl-div-direccion">
                        <asp:Label ID="LblNuevoClienteDireccion" Text="Dirección" runat="server" CssClass="stl-label-campo" />
                        <asp:TextBox ID="TxtNuevoClienteDireccion" placeholder="Dirección" runat="server" CssClass="stl-texto-campo form-control" />
                    </div>
                    <div class="stl-div-localidad">
                        <asp:Label ID="LblNuevoClienteLocalidad" Text="Localidad" runat="server" CssClass="stl-label-campo" />
                        <asp:DropDownList ID="DdlNuevoClienteLocalidad" runat="server" AppendDataBoundItems="true" CssClass="stl-texto-campo form-control">
                            <asp:ListItem Text="Seleccione" />
                        </asp:DropDownList>
                    </div>
                    <div class="stl-div-telefono">
                        <asp:Label ID="LblNuevoClienteTelefono" Text="Teléfono" runat="server" CssClass="stl-label-campo" />
                        <asp:TextBox ID="TxtNuevoClienteTelefono" placeholder="Teléfono" runat="server" CssClass="stl-texto-campo form-control" TextMode="Number" />
                    </div>
                    <div class="stl-div-mail">
                        <asp:Label ID="LblNuevoClienteMail" Text="Mail" runat="server" CssClass="stl-label-campo" />
                        <asp:TextBox ID="TxtNuevoClienteMail" placeholder="Mail" runat="server" CssClass="stl-texto-campo form-control" TextMode="Email" />
                    </div>
                </div>
                <br /><br />
                <div class="stl-div-boton">
                    <asp:Button ID="BtnNuevoClienteConfirmar" Text="Confirmar Cliente" class="btn btn-dark" runat="server" />
                    <span id="NuevoClienteCancelar" class="btn btn-dark stl-btn-cancelar-agregar" runat="server">
                        Cancelar
                    </span>
                </div>
            </div>
        </section>

        <section id="section_modificar_cliente" runat="server" class="stl-section-modificar-cliente">
            <div class="stl-div-titulo">
                <h1>Modificar Cliente</h1>
            </div>
            <div class="stl-div-campos">

            </div>
            <div class="stl-div-boton">
                <span id="ModificarClienteCancelar" class="btn btn-dark stl-btn-cancelar-modificar" runat="server">
                    Cancelar
                </span>
            </div>
        </section>

        <section id="section_eliminar_cliente" runat="server" class="stl-section-eliminar-cliente">
            <div class="stl-div-titulo">
                <h1>Eliminar Cliente</h1>
            </div>
            <div class="stl-div-campos">

            </div>
            <div class="stl-div-boton">
                <span id="EliminarClienteCancelar" class="btn btn-dark stl-btn-cancelar-eliminar" runat="server">
                    Cancelar
                </span>
            </div>
        </section>

        <section id="section_localidades" runat="server" class="stl-section-localidades">
            <div class="stl-div-titulo">
                <h1>ABM Localidades</h1>
            </div>
            <div class="stl-div-campos">

            </div>
            <div class="stl-div-boton">
                <span id="LocalidadesCancelar" class="btn btn-dark stl-btn-cancelar-localidades" runat="server">
                    Cancelar
                </span>
            </div>
        </section>

    </div>

    <asp:HiddenField ID="hfError" runat="server" />

    <script>

        function errorConfirm() {
            var message = $('[id$=hfError]').val()
            if ($('[id$=hfError]').val() != '') {
                Swal.fire({
                    position: 'center',
                    icon: 'warning',
                    title: message,
                    showConfirmButton: false,
                    timer: 2000
                })
            }
            $('[id$=hfError]').val('')
        }

        $(function () {
            errorConfirm();
        });

    </script>

</asp:Content>
