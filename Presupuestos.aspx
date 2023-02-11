<%@ Page Title="Presupuestos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Presupuestos.aspx.cs" Inherits="CompuGross_Web.Presupuestos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="Styles/Style_Presupuestos.css?v=2.0" rel="stylesheet" />
    <script defer src="JavaScript/Script_Presupuestos.js" ></script>

    <section id="section_titulo">
        <div class="stl-div-titulo">
            <h1>Presupuesto</h1>
        </div>
    </section>

    <br /><br />
    
    <asp:Panel DefaultButton="BtnAgregar" runat="server">

        <section id="section_campos">
            <div class="stl-div-contenedor">
                <div class="stl-div-campos">
                    <div class="stl-div-campo">
                        <asp:Label ID="LblFecha" Text="Fecha" runat="server" CssClass="stl-label-campo" />
                        <asp:TextBox ID="TxtFecha" runat="server" Type="date" CssClass="stl-texto-campo form-control" />
                    </div>

                    <div class="stl-div-campo">
                        <asp:LinkButton ID="LblCliente" Text="Cliente" runat="server" CssClass="stl-label-campo stl-label-cliente" OnClick="LblCliente_Click" />
                        <asp:TextBox ID="TxtCliente" runat="server" PlaceHolder="No seleccionado..." MaxLength="50" onkeypress="javascript:return validacionCliente(event)" CssClass="stl-texto-campo form-control" />
                    </div>

                    <div class="stl-div-campo">
                        <asp:Label ID="LblCodigo" Text="Codigo" runat="server" CssClass="stl-label-campo" />
                        <asp:TextBox ID="TxtCodigo" runat="server" MaxLength="8" onkeypress="javascript:return validacionCodigo(event)" CssClass="stl-texto-campo form-control" />
                    </div>

                    <div class="stl-div-campo">
                        <asp:Label ID="LblCantidad" Text="Cantidad" runat="server" CssClass="stl-label-campo" />
                        <asp:TextBox ID="TxtCantidad" runat="server" MaxLength="10" onkeypress="javascript:return validacionCantidad_y_Precio(event)" CssClass="stl-texto-campo form-control" />
                    </div>

                    <div class="stl-div-campo">
                        <asp:Label ID="LblPrecioUnitario" Text="PrecioUnitario" runat="server" CssClass="stl-label-campo" />
                        <asp:TextBox ID="TxtPrecioUnitario" runat="server" MaxLength="9" onkeypress="javascript:return validacionCantidad_y_Precio(event)" CssClass="stl-texto-campo form-control" />
                    </div>

                    <div class="stl-div-campo">
                        <asp:Label ID="LblDescripcion" Text="Descripcion" runat="server" CssClass="stl-label-campo stl-label-descripcion" />
                        <asp:TextBox ID="TxtDescripcion" TextMode="MultiLine" MaxLength="85" runat="server" CssClass="stl-texto-campo form-control" />
                    </div>
                </div>
            </div>
        </section>

        <br /><br />

        <section id="section_botones">
            <div class="stl-div-botones">
                <asp:Button ID="BtnAgregar" Text="Agregar item" runat="server" CssClass="btn btn-dark" OnClick="BtnAgregar_Click" />
                <asp:Button ID="BtnVaciar" Text="Borrar Presupuesto Actual" runat="server" CssClass="btn btn-dark" OnClick="BtnVaciar_Click" />
                <asp:Button ID="BtnExportar" Text="Guardar PDF" runat="server" CssClass="btn btn-dark stl-btn-exportar" OnClick="BtnExportar_Click" />
            </div>
        </section>

    </asp:Panel>

    <section id="section_totales_listado" runat="server" class="stl-section-totales-listado">
        <br />
        <div class="stl-div-totales_listado stl-div-total-items-listado card card-body">
            <b><asp:Label ID="LblTotalItems" Text="" runat="server" CssClass="stl-lbl-totales-listado" /></b>
        </div>
        <div class="stl-div-totales_listado card card-body">
            <b><asp:Label ID="LblTotalPrecio" Text="" runat="server" CssClass="stl-lbl-totales-listado" /></b>
        </div>
    </section>

    <section id="section_listado" runat="server" class="stl-section-listado">
        <br />
        <div class="stl-div-titulos-listado card">
            <div class="stl-div-titulo-listado-codigo stl-div-titulo-listado card-body">
                <label class="stl-label-titulo-listado"><b><u>Código</u></b></label>
            </div>

            <div class="stl-div-titulo-listado-cantidad stl-div-titulo-listado card-body">
                <label class="stl-label-titulo-listado"><b><u>Cant.</u></b></label>
            </div>

            <div class="stl-div-titulo-listado-precio stl-div-titulo-listado card-body">
                <label class="stl-label-titulo-listado"><b><u>$ x uni</u></b></label>
            </div>

            <div class="stl-div-titulo-listado-descripcion stl-titulo-listado-descripcion card-body">
                <label class="stl-label-titulo-listado"><b><u>Descripción</u></b></label>
            </div>
            
            <div class="stl-div-titulo-listado-subtotal stl-div-titulo-listado card-body">
                <label class="stl-label-titulo-listado"><b><u>$ Subt.</u></b></label>
            </div>            

            <div class="stl-div-titulo-listado-btn card body">
                <label class="stl-label-titulo-listado"><%--<b><u>Acciones</u></b>--%></label>
            </div>
        </div>

        <div class="stl-div-repeater-listado">
            <asp:Repeater ID="RepeaterListado" runat="server">
                <ItemTemplate>
                    <div class="stl-div-contenedor-listado-clientes card">
                        <div class="stl-div-titulo-listado-codigo stl-div-contenedor-listado card-body">
                            <label class="stl-label-campo-listado"><%# Eval("Codigo") %></label>
                        </div>

                        <div class="stl-div-titulo-listado-cantidad stl-div-contenedor-listado card-body">
                            <label class="stl-label-campo-listado"><%# Eval("Cantidad") %></label>
                        </div>

                        <div class="stl-div-titulo-listado-precio stl-valor-precio-listado stl-div-contenedor-listado card-body">
                            <label class="stl-label-campo-listado">$<%# Eval("Precio") %></label>
                        </div>

                        <div class="stl-div-titulo-listado-descripcion stl-texto-listado-descripcion card-body">
                            <label class="stl-label-campo-listado"><%# Eval("Descripcion") %></label>
                        </div>

                        <div class="stl-div-titulo-listado-subtotal stl-valor-subtotal-listado stl-div-contenedor-listado card-body">
                            <label class="stl-label-campo-listado">$<%# Eval("Subtotal") %></label>
                        </div>

                        <div class="stl-div-titulo-listado-btn stl-boton-normal">
                            <a href="Presupuestos.aspx?CodigoItem=<%# Eval("Codigo") %>" style="text-decoration: none;">
                                <span class="btn btn-danger btn-sm stl-btn-eliminar-item">Quitar</span>
                            </a>
                        </div>

                        <div class="stl-div-titulo-listado-btn stl-boton-chico">
                            <a href="Presupuestos.aspx?CodigoItem=<%# Eval("Codigo") %>" style="text-decoration: none;">
                                <span class="btn btn-danger btn-sm stl-btn-eliminar-item"><b>X</b></span>
                            </a>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </section>

    <asp:HiddenField ID="hfMessage" runat="server" />
    <asp:HiddenField ID="hfError" runat="server" />

</asp:Content>
