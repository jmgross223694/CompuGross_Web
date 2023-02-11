<%@ Page Title="Precios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Precios.aspx.cs" Inherits="CompuGross_Web.Precios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="Styles/Style_Precios.css?v=2.0" rel="stylesheet" />
    <script defer src="JavaScript/Script_Precios.js" ></script>

    <center>
        <section id="section_titulo" class="stl-section-titulo" runat="server">
            <div class="stl-div-titulo">
                <h1><asp:Label ID="LblTitulo" Text="Listado de Precios" runat="server" /></h1>
            </div>
        </section>
    </center>

    <br />

    <asp:Panel DefaultButton="BtnCargarPrecios" runat="server">
        <section id="section_precio_dolar" class="stl-section-precio-dolar" runat="server">
            <div class="stl-div-precio-dolar">
                <asp:TextBox ID="TxtPrecioDolar" CssClass="stl-txt-precio-dolar" onpaste="return false" ToolTip="Valor de 1 Dólar en Pesos" onkeypress="javascript:return validacionPrecioPrincipal(event)" PlaceHolder="Precio Dolar Hoy" runat="server" />
                <asp:Button ID="BtnCargarPrecios" CssClass="btn btn-dark stl-btn-cargar-precios" Text="Recargar precios" runat="server" OnClick="BtnCargarPrecios_Click" />
                <asp:Button ID="BtnAgregar" CssClass="btn btn-dark stl-btn-agregar" Text="Agregar nuevo" runat="server" OnClick="BtnAgregar_Click" />
            </div>
            <br />
        </section>
    </asp:Panel>

    <section id="section_listado" runat="server" class="stl-section-listado">
        <div class="stl-div-contenedor-titulos-listado card">
            <div class="stl-div-titulo-listado stl-codigo card-body">
                <label class="stl-label-campo-titulo-listado"><b><u>Código</u></b></label>
            </div>

            <div class="stl-div-titulo-listado stl-descripcion stl-div-titulo-listado-descripcion card-body">
                <label class="stl-label-campo-titulo-listado"><b><u>Descripción</u></b></label>
            </div>

            <div class="stl-div-titulo-listado stl-pesos card-body">
                <label class="stl-label-campo-titulo-listado"><b><u>Pesos</u></b></label>
            </div>

            <div class="stl-div-titulo-listado stl-dolares card-body">
                <label class="stl-label-campo-titulo-listado"><b><u>Dólares</u></b></label>
            </div>

            <div class="stl-div-btn-listado">
                <label class="stl-label-campo-titulo-listado stl-lbl-titulo-listado"><%--<b><u>Acciones</u></b>--%></label>
            </div>
        </div>

        <div>
            <asp:Repeater ID="RepeaterPrecios" runat="server">
                <ItemTemplate>
                    <div class="stl-div-contenedor-campos-listado card">
                        <div class="stl-div-campo-listado stl-codigo card-body">
                            <label class="stl-label-campos-listado"><%# Eval("Codigo") %></label>
                        </div>

                        <div class="stl-div-campo-listado stl-descripcion stl-div-campo-listado-descripcion card-body">
                            <label class="stl-label-campos-listado stl-label-descripcion"><%# Eval("Descripcion") %></label>
                        </div>

                        <div class="stl-div-campo-listado stl-pesos card-body">
                            <label class="stl-label-campos-listado"><%# Eval("Pesos") %></label>
                        </div>

                        <div class="stl-div-campo-listado stl-dolares card-body">
                            <label class="stl-label-campos-listado"><%# Eval("Dolares") %></label>
                        </div>

                        <div class="stl-div-campo-listado-btn">
                            <a href="Precios.aspx?IdPrecio=<%# Eval("ID") %>&AccionPrecio=Modificar" style="text-decoration: none;">

                                <span class="btn btn-primary btn-sm stl-btn">Modificar</span>
                                <span class="btn btn-primary btn-sm stl-btn-aux">
                                    <img src="img/OrdenesTrabajo/editar3.png" alt="Modif." class="stl-img-aux" />
                                </span>

                            </a>

                        <a href="Precios.aspx?IdPrecio=<%# Eval("ID") %>&AccionPrecio=Eliminar" style="text-decoration: none;">
                                
                                <span class="btn btn-danger btn-sm stl-btn">Eliminar</span>
                                <span class="btn btn-danger btn-sm stl-btn-aux">
                                    <img src="img/OrdenesTrabajo/del-logo.png" alt="Elim." class="stl-img-aux" />
                                </span>

                            </a>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </section>

    <asp:Panel DefaultButton="BtnConfirmarAgregar" runat="server">
        <section id="section_agregar" class="stl-section-agregar" runat="server">
            <div class="stl-div-contenedor-agregar">
                <div class="stl-div-contenido-campo-agregar">
                    <asp:Label ID="LblAgregarCodigo" Text="Código" ToolTip="Código" runat="server" CssClass="stl-label-campo" />
                    <asp:TextBox ID="TxtAgregarCodigo" Text="" MaxLength="8" PlaceHolder="Código" onpaste="return false" ToolTip="Código" runat="server" CssClass="stl-texto-campo stl-texto-campo-obligatorio form-control"/>
                    <b style="color: red;">*</b>
                </div>
                <div class="stl-div-contenido-campo-agregar">
                    <asp:Label ID="LblAgregarDescripción" Text="Descripción" ToolTip="Descripción" runat="server" CssClass="stl-label-campo" />
                    <asp:TextBox ID="TxtAgregarDescripcion" Text="" MaxLength="85" PlaceHolder="Descripción" onpaste="return false" ToolTip="Descripción" runat="server" CssClass="stl-texto-campo stl-texto-campo-obligatorio form-control"/>
                    <b style="color: red;">*</b>
                </div>
                <div class="stl-div-contenido-campo-agregar">
                    <asp:Label ID="LblAgregarAclaraciones" Text="Aclaraciones" ToolTip="Aclaraciones" runat="server" CssClass="stl-label-campo" />
                    <asp:TextBox ID="TxtAgregarAclaraciones" Text="" MaxLength="200" PlaceHolder="Aclaraciones" onpaste="return false" ToolTip="Aclaraciones" runat="server" CssClass="stl-texto-campo form-control"/>
                </div>
                <div class="stl-div-contenido-campo-agregar">
                    <asp:Label ID="LblAgregarPrecio" Text="Precio" ToolTip="Precio" runat="server" CssClass="stl-label-campo" />
                    <asp:TextBox ID="TxtAgregarPrecio" Text="" PlaceHolder="Precio" MaxLength="11" ToolTip="Precio" onpaste="return false" runat="server" onkeypress="javascript:return validacionPrecioAgregar(event)" CssClass="stl-texto-campo stl-texto-campo-obligatorio form-control"/>
                    <b style="color: red;">*</b>
                </div>
                <div class="stl-div-contenido-btn-agregar">
                    <asp:Button ID="BtnConfirmarAgregar" Text="Confirmar y Agregar" runat="server" OnClick="BtnConfirmarAgregar_Click" CssClass="btn btn-dark stl-btn-confirmar-agregar" />
                    <asp:Button ID="BtnCancelarAgregar" Text="Cancelar" runat="server" OnClick="BtnCancelarAgregar_Click" CssClass="btn btn-dark stl-btn-cancelar-agregar" />
                </div>
            </div>
        </section>
    </asp:Panel>

    <section id="section_modificar" class="stl-section-modificar" runat="server">
        <div class="stl-div-contenedor-modificar">
            <asp:HiddenField ID="HfIdPrecio" runat="server" />
            <div class="stl-div-contenido-campo-modificar">
                <asp:Label ID="LblModificarCodigo" Text="Código" ToolTip="Código" runat="server" CssClass="stl-label-campo" />
                <asp:TextBox ID="TxtModificarCodigo" Text="" PlaceHolder="Código" onpaste="return false" MaxLength="8" ToolTip="Código" runat="server" CssClass="stl-texto-campo stl-texto-campo-obligatorio form-control"/>
                <b style="color: red;">*</b>
            </div>
            <div class="stl-div-contenido-campo-modificar">
                <asp:Label ID="LblModificarDescripcion" Text="Descripción" ToolTip="Descripción" runat="server" CssClass="stl-label-campo" />
                <asp:TextBox ID="TxtModificarDescripcion" Text="" PlaceHolder="Descripción" onpaste="return false" MaxLength="85" ToolTip="Descripción" runat="server" CssClass="stl-texto-campo stl-texto-campo-obligatorio form-control"/>
                <b style="color: red;">*</b>
            </div>
            <div class="stl-div-contenido-campo-modificar">
                <asp:Label ID="LblModificarAclaraciones" Text="Aclaraciones" ToolTip="Aclaraciones" runat="server" CssClass="stl-label-campo" />
                <asp:TextBox ID="TxtModificarAclaraciones" Text="" PlaceHolder="Aclaraciones" onpaste="return false" ToolTip="Aclaraciones" runat="server" CssClass="stl-texto-campo form-control"/>
            </div>
            <div class="stl-div-contenido-campo-modificar">
                <asp:Label ID="LblModificarPrecio" Text="Precio" ToolTip="Precio" runat="server" CssClass="stl-label-campo" />
                <asp:TextBox ID="TxtModificarPrecio" Text="" PlaceHolder="Precio" onpaste="return false" MaxLength="11" ToolTip="Precio" runat="server" onkeypress="javascript:return validacionPrecioModificar(event)" CssClass="stl-texto-campo stl-texto-campo-obligatorio form-control"/>
                <b style="color: red;">*</b>
            </div>
            <div class="stl-div-contenido-btn-modificar">
                <asp:Button ID="BtnConfirmarModificar" Text="Confirmar cambios" runat="server" OnClick="BtnConfirmarModificar_Click" CssClass="btn btn-dark stl-btn-confirmar-modificar" />
                <asp:Button ID="BtnCancelarModificar" Text="Cancelar" runat="server" OnClick="BtnCancelarModificar_Click" CssClass="btn btn-dark stl-btn-cancelar-modificar" />
            </div>
        </div>
    </section>

    <section id="section_confirmar_eliminar" class="stl-section-confirmar-eliminar" runat="server">
        <div class="stl-div-confirmar-eliminar">
            <h3>
                <asp:Label ID="LblConfirmarEliminar" Text="¿Confirmar Eliminar el Precio?" runat="server" CssClass="stl-lbl-confirmar-eliminar" />
            </h3>
        </div>
        <div class="stl-div-btn-confirmar-eliminar">
            <asp:Button ID="BtnConfirmarEliminar" Text="Confirmar" runat="server" OnClick="BtnConfirmarEliminar_Click" CssClass="btn btn-danger stl-btn-confirmar-eliminar" />
            <asp:Button ID="BtnCancelarEliminar" Text="Cancelar" runat="server" OnClick="BtnCancelarEliminar_Click" CssClass="btn btn-dark stl-btn-cancelar-eliminar" />
        </div>
    </section>

    <asp:HiddenField ID="hfMessage" runat="server" />
    <asp:HiddenField ID="hfError" runat="server" />

</asp:Content>
