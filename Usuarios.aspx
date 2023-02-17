<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="CompuGross_Web.Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="Styles/Style_Usuarios.css?v=2.0" rel="stylesheet" />
    <script defer src="JavaScript/Script_Usuarios.js" ></script>

    <center>
        <section id="section_titulo" runat="server">
            <div class="stl-div-titulo">
                <h1><asp:Label ID="LblTitulo" Text="Usuarios" runat="server" CssClass="stl-lbl-titulo" /></h1>
            </div>
        </section>
    </center>

    <section id="section_botones_principales" class="stl-section-botones-principales" runat="server">
        <div class="stl-div-botones-principales">
            <asp:Button ID="BtnAgregar" Text="Agregar" runat="server" CssClass="btn btn-dark stl-btn-agregar" OnClick="BtnAgregar_Click" />
            <asp:Button ID="BtnListar" Text="Listar" runat="server" CssClass="btn btn-dark stl-btn-listar" OnClick="BtnListar_Click" />
        </div>
    </section>

    <section id="section_agregar" class="stl-section-agregar" runat="server">
        <div class="stl-div-contenedor">
            <div class="stl-div-campo stl-div-form-tipo-usuario">
                <asp:Label ID="LblAgregarTipoUsuario" Text="Tipo de Usuario" runat="server" CssClass="stl-label-campo" />
                <asp:DropDownList ID="DdlAgregarTiposUsuario" ToolTip="Tipo de Usuario" runat="server" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control stl-texto-campo stl-ddl-agregar-tipo-usuario stl-ddl-tipo-usuario">
                    <asp:ListItem Value="0" Text="Seleccione..." />
                </asp:DropDownList>
            </div>

            <div class="stl-div-campo stl-div-form-apellido">
                <asp:Label ID="LblAgregarApellido" Text="Apellido" runat="server" CssClass="stl-label-campo" />
                <asp:TextBox ID="TxtAgregarApellido" runat="server" onpaste="return false" onkeypress="javascript:return soloLetrasAgregarApellido(event)" ToolTip="Apellido" placeholder="Apellido" MaxLength="25" CssClass="stl-texto-campo form-control" />
            </div>

            <div class="stl-div-campo stl-div-form-nombre">
                <asp:Label ID="LblAgregarNombre" Text="Nombre" runat="server" CssClass="stl-label-campo" />
                <asp:TextBox ID="TxtAgregarNombre" runat="server" onpaste="return false" onkeypress="javascript:return soloLetrasAgregarNombre(event)" ToolTip="Nombre" placeholder="Nombre" MaxLength="25" CssClass="stl-texto-campo form-control" />
            </div>

            <div class="stl-div-campo stl-div-form-mail">
                <asp:Label ID="LblAgregarMail" Text="Mail" runat="server" CssClass="stl-label-campo" />
                <asp:TextBox ID="TxtAgregarMail" runat="server" onpaste="return false" ToolTip="Mail" placeholder="Mail" MaxLength="50" CssClass="stl-texto-campo form-control" />
                <%--<asp:DropDownList ID="DdlMail" runat="server" AutoPostBack="true" AppendDataBoundItems="true" CssClass="stl-texto-campo form-control stl-ddl-mail" >
                    <asp:ListItem Text="@gmail.com" />
                    <asp:ListItem Text="@hotmail.com" />
                </asp:DropDownList>--%>
            </div>

            <div class="stl-div-campo stl-div-form-username">
                <asp:Label ID="LblAgregarUsername" Text="Usuario" runat="server" CssClass="stl-label-campo" />
                <asp:TextBox ID="TxtAgregarUsername" runat="server" onpaste="return false" onkeypress="javascript:return soloNumerosAgregarUsername(event)" ToolTip="Usuario" placeholder="Usuario" MaxLength="11" CssClass="stl-texto-campo form-control" />
            </div>

            <div class="stl-div-campo stl-div-form-clave">
                <asp:Label ID="LblAgregarClave" Text="Contraseña" runat="server" CssClass="stl-label-campo" />
                <asp:TextBox ID="TxtAgregarClave" runat="server" onpaste="return false" ToolTip="Contraseña" placeholder="Contraseña" MaxLength="8" CssClass="stl-texto-campo form-control" />
            </div>

            <div class="stl-div-boton">
                <asp:Button ID="BtnConfirmarAgregar" Text="Confirmar Nuevo Usuario" runat="server" CssClass="btn btn-dark stl-btn-confirmar-agregar" OnClick="BtnConfirmarAgregar_Click" />
                <asp:Button ID="BtnCancelarAgregar" Text="Cancelar" runat="server" CssClass="btn btn-dark stl-btn-cancelar-agregar" OnClick="BtnCancelarAgregar_Click" />
            </div>
        </div>
    </section>

    <section id="section_modificar_eliminar" class="stl-section-modificar-eliminar" runat="server">
        <div class="stl-div-contenedor">

            <asp:HiddenField ID="hfIdUsuario" runat="server" />

            <div class="stl-div-campo stl-div-form-tipo-usuario">
                <asp:Label ID="LblModificarTipoUsuario" Text="Tipo de Usuario" runat="server" CssClass="stl-label-campo" />
                <asp:DropDownList ID="DdlModificarTiposUsuario" ToolTip="Tipo de Usuario" runat="server" AutoPostBack="true" AppendDataBoundItems="true" CssClass="stl-texto-campo form-control stl-ddl-agregar-tipo-usuario stl-ddl-tipo-usuario">
                    <asp:ListItem Value="Seleccione..." Text="Seleccione..." />
                </asp:DropDownList>
            </div>

            <div class="stl-div-campo stl-div-form-apellido">
                <asp:Label ID="LblModificarApellido" Text="Apellido" runat="server" CssClass="stl-label-campo" />
                <asp:TextBox ID="TxtModificarApellido" runat="server" onpaste="return false" onkeypress="javascript:return soloLetrasModificarApellido(event)" ToolTip="Apellido" placeholder="Apellido" MaxLength="25" CssClass="stl-texto-campo form-control" />
            </div>

            <div class="stl-div-campo stl-div-form-nombre">
                <asp:Label ID="LblModificarNombre" Text="Nombre" runat="server" CssClass="stl-label-campo" />
                <asp:TextBox ID="TxtModificarNombre" runat="server" onpaste="return false" onkeypress="javascript:return soloLetrasModificarNombre(event)" ToolTip="Nombre" placeholder="Nombre" MaxLength="25" CssClass="stl-texto-campo form-control" />
            </div>

            <div class="stl-div-campo stl-div-form-mail">
                <asp:Label ID="LblModificarMail" Text="Mail" runat="server" CssClass="stl-label-campo" />
                <asp:TextBox ID="TxtModificarMail" runat="server" onpaste="return false" ToolTip="Mail" placeholder="Mail" MaxLength="50" CssClass="stl-texto-campo form-control" />
            </div>

            <div class="stl-div-campo stl-div-form-username">
                <asp:Label ID="LblModificarUsername" Text="Usuario" runat="server" CssClass="stl-label-campo" />
                <asp:TextBox ID="TxtModificarUsername" runat="server" onpaste="return false" onkeypress="javascript:return soloNumerosModificarUsername(event)" ToolTip="Usuario" placeholder="Usuario" MaxLength="11" CssClass="stl-texto-campo form-control" />
            </div>

            <div class="stl-div-campo stl-div-form-clave">
                <asp:Label ID="LblModificarClave" Text="Contraseña" runat="server" CssClass="stl-label-campo" />
                <asp:TextBox ID="TxtModificarClave" runat="server" onpaste="return false" ToolTip="Contraseña" placeholder="Contraseña" MaxLength="8" CssClass="stl-texto-campo form-control" />
            </div>

            <div class="stl-div-boton">
                <asp:Button ID="BtnConfirmarModificar" Text="Confirmar Cambios" runat="server" CssClass="btn btn-dark stl-btn-confirmar-modificar" OnClick="BtnConfirmarModificar_Click" />
                <asp:Button ID="BtnCancelarModificar" Text="Cancelar" runat="server" CssClass="btn btn-dark stl-btn-cancelar-modificar" OnClick="BtnCancelarModificar_Click" />
            </div>
        </div>
    </section>

    <section id="section_confirmar_eliminar" class="stl-section-confirmar-eliminar" runat="server">
        <div class="stl-div-lbl-confirmar-eliminar">
            <h3><asp:Label ID="LblConfirmarEliminar" Text="" runat="server" CssClass="stl-lbl-confirmar-eliminar" /></h3>
        </div>

        <div class="stl-div-boton">
            <asp:Button ID="BtnConfirmarEliminar" Text="Confirmar" runat="server" CssClass="btn btn-danger stl-btn-confirmar-eliminar" onclick="BtnConfirmarEliminar_Click" />
            <asp:Button ID="BtnCancelarEliminar" Text="Cancelar" runat="server" CssClass="btn btn-dark stl-btn-cancelar-eliminar" OnClick="BtnCancelarEliminar_Click" />
        </div>
    </section>

    <section id="section_listar" class="stl-section-listar" runat="server">
        <div class="stl-div-titulos-listar card">
            <div class="stl-div-apellido card-body">
                <label class="stl-label-campos-listar"><b><u>Apellido</u></b></label>
            </div>

            <div class="stl-div-nombre card-body">
                <label class="stl-label-campos-listar"><b><u>Nombre</u></b></label>
            </div>

            <div class="stl-div-usuario card-body">
                <label class="stl-label-campos-listar"><b><u>Usuario</u></b></label>
            </div>

            <div class="stl-div-tipo-usuario card-body">
                <label class="stl-label-campos-listar"><b><u>Tipo de Usuario</u></b></label>
            </div>

            <div class="stl-div-mail card-body">
                <label class="stl-label-campos-listar"><b><u>Mail</u></b></label>
            </div>

            <div class="stl-div-btn-listar">
                <label class="stl-label-campos-listar"><%--<b><u>Acciones</u></b>--%></label>
            </div>
        </div>

        <asp:Repeater ID="RepeaterUsuarios" runat="server">
            <ItemTemplate>
                <div class="stl-div-campos-listar card">
                    <div class="stl-div-apellido card-body">
                        <label class="stl-label-campos-listar"><%# Eval("Apellido") %></label>
                    </div>

                    <div class="stl-div-nombre card-body">
                        <label class="stl-label-campos-listar"><%# Eval("Nombre") %></label>
                    </div>

                    <div class="stl-div-usuario card-body">
                        <label class="stl-label-campos-listar"><%# Eval("Username") %></label>
                    </div>

                    <div class="stl-div-tipo-usuario card-body">
                        <label class="stl-label-campos-listar"><%# Eval("TipoUsuario.Descripcion") %></label>
                    </div>

                    <div class="stl-div-mail card-body">
                        <label class="stl-label-campos-listar"><%# Eval("Mail") %></label>
                    </div>

                    <div class="stl-div-btn-listar">
                        <a href="Usuarios.aspx?IdUsuario=<%# Eval("ID") %>&AccionUsuario=CargarCamposModificar" style="text-decoration: none;">

                            <span class="btn btn-primary btn-sm stl-btn-modificar">Modificar</span>
                            <span class="btn btn-primary btn-sm stl-btn-aux">
                                <img src="img/OrdenesTrabajo/editar3.png" alt="Modif." class="stl-img-aux" />
                            </span>

                        </a>

                    <a href="Usuarios.aspx?IdUsuario=<%# Eval("ID") %>&AccionUsuario=ConfirmarEliminar" style="text-decoration: none;">
                            
                            <span class="btn btn-danger btn-sm stl-btn-eliminar">Eliminar</span>
                            <span class="btn btn-danger btn-sm stl-btn-aux">
                                <img src="img/OrdenesTrabajo/del-logo.png" alt="Elim." class="stl-img-aux" />
                            </span>

                        </a>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </section>

    <asp:HiddenField ID="hfMessage" runat="server" />
    <asp:HiddenField ID="hfError" runat="server" />

</asp:Content>
