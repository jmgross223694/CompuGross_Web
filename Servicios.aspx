<%@ Page Title="Servicios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="true" CodeBehind="Servicios.aspx.cs" Inherits="CompuGross_Web.Servicios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="Styles/Style_Servicios.css?v=2.0" rel="stylesheet" />
    <script defer src="JavaScript/Script_Servicios.js" ></script>

    <div class="stl-div-content">

        <section id="section_titulo" runat="server">
            <div class="stl-div-titulo">
                <h1><asp:Label id="LblTitulo" Text="Servicios" runat="server" /></h1>
            </div>
        </section>

        <br />

        <section id="section_principal" runat="server" class="stl-section-principal">
            <div class="stl-div-boton">
                <asp:Button ID="BtnAgregar" Text="Agregar Nuevo" runat="server" CssClass="btn btn-dark stl-btn-agregar-nuevo" OnClick="BtnAgregar_Click" />
                <asp:Button ID="BtnListar" Text="Listar Todos" runat="server" CssClass="btn btn-dark stl-btn-listar-todos" OnClick="BtnListar_Click" />
            </div>
        </section>
        <asp:Panel DefaultButton="BtnConfirmarAgregar"  runat="server">
            <section id="section_agregar" runat="server" class="stl-section-agregar">
                <section id="section_agregar_campos" runat="server" class="stl-section-agregar-campos">
                    <div class="stl-div-contenedor">
                        <div class="stl-div-campos">
                            <div class="stl-div-contenido-campo">
                                <asp:Label ID="LblAgregarTiposEquipo" Text="Tipo de Equipo" runat="server" CssClass="stl-label-campo" />
                                <asp:DropDownList ID="DdlAgregarTiposEquipo" AppendDataBoundItems="true" AutoPostBack="true" runat="server" CssClass="stl-texto-campo form-control stl-ddl" OnSelectedIndexChanged="DdlAgregarTiposEquipo_SelectedIndexChanged">
                                </asp:DropDownList>
                                <b style="color: red;">*</b>
                            </div>
                            <section id="section_agregar_campos2" runat="server" class="stl-section-agregar-campos2">
                                <div class="stl-div-contenido-campo">
                                    <asp:Label ID="LblAgregarTiposServicio" Text="Tipo de Servicio" runat="server" CssClass="stl-label-campo" />
                                    <asp:DropDownList ID="DdlAgregarTiposServicio" AppendDataBoundItems="true" AutoPostBack="true" runat="server" CssClass="stl-texto-campo form-control stl-ddl" OnSelectedIndexChanged="DdlAgregarTiposServicio_SelectedIndexChanged" >
                                    </asp:DropDownList>
                                    <b style="color: red;">*</b>
                                </div>
                                <div class="stl-div-contenido-campo">
                                    <asp:LinkButton ID="LblAgregarCliente" Text="Cliente" ToolTip="Haga click aquí para elegir un cliente" runat="server" CssClass="stl-label-campo stl-link-button-cliente" OnClick="LblAgregarCliente_Click" />
                                    <asp:TextBox ID="TxtAgregarCliente" Text="" ReadOnly="true" PlaceHolder="No seleccionado..." ToolTip="Cliente seleccionado" runat="server" CssClass="stl-texto-campo form-control"/>
                                    <b style="color: red;">*</b>
                                </div>
                                <div class="stl-div-contenido-campo">
                                    <asp:Label ID="LblAgregarFechaRecepcion" Text="Fecha Recepción" runat="server" CssClass="stl-label-campo" />
                                    <asp:TextBox ID="TxtAgregarFechaRecepcion" Text="" PlaceHolder="" ToolTip="" Type="date" runat="server" CssClass="stl-texto-campo form-control" />
                                    <b style="color: red;">*</b>
                                </div>
                                <div class="stl-div-contenido-campo">
                                    <asp:Label ID="LblAgregarFechaDevolucion" Text="Fecha Devolución" runat="server" CssClass="stl-label-campo" />
                                    <asp:TextBox ID="TxtAgregarFechaDevolucion" Text="" PlaceHolder="" ToolTip="" Type="date" Enable="false" Visible="false" runat="server" CssClass="stl-texto-campo form-control" />
                                    <asp:CheckBox ID="CbAgregarFechaDevolucion1" Text="" runat="server" Checked="false" AutoPostBack="true" CssClass="stl-checkbox-fecha-devolucion stl-checkbox-1-fecha-devolucion" OnCheckedChanged="CbAgregarFechaDevolucion_CheckedChanged" />
                                    <asp:CheckBox ID="CbAgregarFechaDevolucion2" Text="" runat="server" Visible="false" Checked="true" AutoPostBack="true" OnCheckedChanged="CbAgregarFechaDevolucion_CheckedChanged" />
                                </div>
                                <div class="stl-div-contenido-campo">
                                    <asp:Label ID="LblAgregarMarcaModelo" Text="Marca y Modelo" runat="server" CssClass="stl-label-campo" />
                                    <asp:TextBox ID="TxtAgregarMarcaModelo" Text="" PlaceHolder="Marca y Modelo" ToolTip="Marca y Modelo del Equipo" runat="server" CssClass="stl-texto-campo form-control" />
                                    <b style="color: red;">*</b>
                                </div>
                                <div class="stl-div-contenido-campo">
                                    <asp:Label ID="LblAgregarRam" Text="Memoria RAM" runat="server" CssClass="stl-label-campo" />
                                    <asp:TextBox ID="TxtAgregarRam" Text="" PlaceHolder="Memoria RAM" ToolTip="Marca, Modelo, Frecuencia y Velocidad de la memoria RAM del Equipo" runat="server" CssClass="stl-texto-campo form-control" />
                                </div>
                                <div class="stl-div-contenido-campo">
                                    <asp:Label ID="LblAgregarMicroprocesador" Text="Microprocesador" runat="server" CssClass="stl-label-campo" />
                                    <asp:TextBox ID="TxtAgregarMicroprocesador" Text="" PlaceHolder="Microprocesador" ToolTip="Marca, Modelo y Frecuencia del microprocesador del Equipo" runat="server" CssClass="stl-texto-campo form-control" />
                                </div>
                                <div class="stl-div-contenido-campo">
                                    <asp:Label ID="LblAgregarAlmacenamiento" Text="Almacenamiento" runat="server" CssClass="stl-label-campo" />
                                    <asp:TextBox ID="TxtAgregarAlmacenamiento" Text="" PlaceHolder="Almacenamiento" ToolTip="Marca, Modelo y Capacidad del almacenamiento del Equipo" runat="server" CssClass="stl-texto-campo form-control" />
                                </div>
                                <div class="stl-div-contenido-campo">
                                    <asp:Label ID="LblAgregarPlacaMadre" Text="Placa Madre" runat="server" CssClass="stl-label-campo" />
                                    <asp:TextBox ID="TxtAgregarPlacaMadre" Text="" PlaceHolder="Placa Madre" ToolTip="Marca y modelo de la Placa Madre" runat="server" CssClass="stl-texto-campo form-control" />
                                </div>
                                <div class="stl-div-contenido-campo">
                                    <asp:Label ID="LblAgregarNumSerie" Text="N° de Serie" runat="server" CssClass="stl-label-campo" />
                                    <asp:TextBox ID="TxtAgregarNumSerie" Text="" PlaceHolder="Número de Serie" ToolTip="Número de serie del Equipo" runat="server" CssClass="stl-texto-campo form-control" />
                                </div>
                                <div class="stl-div-contenido-campo">
                                    <asp:Label ID="LblAgregarAdicionales" Text="Adicionales" runat="server" CssClass="stl-label-campo" />
                                    <asp:TextBox ID="TxtAgregarAdicionales" Text="" PlaceHolder="Adicionales" ToolTip="Adicionales recibidos (Cargador, Cables, etc.)" runat="server" CssClass="stl-texto-campo form-control" />
                                </div>
                                <div class="stl-div-contenido-campo">
                                    <asp:Label ID="LblAgregarAlimentacion" Text="Alimentación" runat="server" CssClass="stl-label-campo" />
                                    <asp:TextBox ID="TxtAgregarAlimentacion" Text="" PlaceHolder="Alimentación" ToolTip="Tipo de alimentación del Equipo (Batería, Fuente, etc.)" runat="server" CssClass="stl-texto-campo form-control" />
                                </div>
                                <div class="stl-div-contenido-campo">
                                    <asp:Label ID="LblAgregarUnidadOptica" Text="Unidad Óptica" runat="server" CssClass="stl-label-campo" />
                                    <asp:DropDownList ID="DdlAgregarUnidadOptica" AppendDataBoundItems="true" runat="server" CssClass="stl-texto-campo form-control stl-ddl">
                                    </asp:DropDownList>
                                </div>
                                <div class="stl-div-contenido-campo">
                                    <asp:Label ID="LblAgregarDescripcion" Text="Descripción" runat="server" CssClass="stl-label-campo stl-label-descripcion" />
                                    <asp:TextBox ID="TxtAgregarDescripcion" Text="" TextMode="MultiLine" onpaste="return false" PlaceHolder="Descripción" ToolTip="Breve descripción del Servicio realizado" onkeypress="javascript:return validarDescripcion(event)" runat="server" CssClass="stl-texto-campo form-control" />
                                    <b style="display: inline-block; color: red; vertical-align: top; margin-top: 15px;">*</b>
                                </div>
                                <div class="stl-div-contenido-campo">
                                    <asp:Label ID="LblAgregarCostoRepuestos" Text="$ Repuestos" runat="server" CssClass="stl-label-campo" />
                                    <asp:TextBox ID="TxtAgregarCostoRepuestos" Text="" PlaceHolder="$ Repuestos" onpaste="return false" ToolTip="Costo de repuestos" onkeypress="javascript:return soloNumerosCostosNuevoServicio3(event)" runat="server" CssClass="stl-texto-campo form-control" />
                                </div>
                                    <div class="stl-div-contenido-campo">
                                    <asp:Label ID="LblAgregarHonorarios" Text="$ Mano de Obra" runat="server" CssClass="stl-label-campo" />
                                    <asp:TextBox ID="TxtAgregarHonorarios" Text="" PlaceHolder="$ Mano de Obra" onpaste="return false" ToolTip="Costo de mano de obra propio" onkeypress="javascript:return soloNumerosCostosNuevoServicio2(event)" runat="server" CssClass="stl-texto-campo form-control" />
                                    <b style="color: red;">*</b>
                                </div>
                                <div class="stl-div-contenido-campo">
                                    <asp:Label ID="LblAgregarCostoTerceros" Text="$ Terceros" runat="server" CssClass="stl-label-campo" />
                                    <asp:TextBox ID="TxtAgregarCostoTerceros" Text="" PlaceHolder="$ Terceros" onpaste="return false" ToolTip="Costo de Servicios terciarizados" onkeypress="javascript:return soloNumerosCostosNuevoServicio1(event)" runat="server" CssClass="stl-texto-campo form-control" />
                                </div>
                            </section>
                        </div>
                    </div>
                </section>

                <br />

                <section id="section_agregar_boton" runat="server" class="stl-section-agregar-boton">
                    <div class="stl-div-boton">
                        <asp:Button id="BtnConfirmarAgregar" Text="Confirmar Nuevo Servicio" runat="server" CssClass="btn btn-dark stl-btn-confirmar" OnClick="BtnConfirmarAgregar_Click" />
                        <asp:Button id="BtnCancelarAgregar" Text="Cancelar" runat="server" CssClass="btn btn-dark stl-btn-cancelar" OnClick="BtnCancelarAgregar_Click" />
                    </div>
                </section>
            </section>
        </asp:Panel>

        <div class="stl-div-contenedor-elegir-cliente">
            <section id="section_elegir_cliente" runat="server" class="stl-section-elegir-cliente">
                <asp:Panel DefaultButton="BtnElegirCLienteBusqueda" runat="server">
                    <div class="stl-elegir-div-busqueda">
                        <asp:TextBox ID="TxtElegirClienteBusqueda" Tooltip="Se busca coincidencias con Nombres, Apellidos, Telefono, CUIT/DNI, Mail, Dirección y/o Localidad" PlaceHolder="Búsqueda..." runat="server" CssClass="stl-texto-campo form-control stl-elegir-cliente-txt-buscar" />
                        <asp:Button ID="BtnElegirClienteBusqueda" Text="Buscar" runat="server" CssClass="btn btn-dark" OnClick="BtnElegirClienteBusqueda_Click" />
                    </div>
                </asp:Panel>

                <br />

                <section id="section_modificar_cliente_titulo_listado">
                    <div class="stl-div-titulos-listado-clientes card">
                        <div class="stl-div-apenom-cliente card-body">
                            <label class="stl-label-campos-modificar-cliente"><b><u>Apellido y Nombre</u></b></label>
                        </div>

                        <div class="stl-div-telefono-cliente card-body">
                            <label class="stl-label-campos-modificar-cliente"><b><u>Teléfono</u></b></label>
                        </div>

                        <div class="stl-div-mail-cliente card-body">
                            <label class="stl-label-campos-modificar-cliente"><b><u>Mail</u></b></label>
                        </div>

                        <div class="stl-div-cuitdni-cliente card-body">
                            <label class="stl-label-campos-modificar-cliente"><b><u>CUIT / DNI</u></b></label>
                        </div>

                        <div class="stl-div-btn-modificar-eliminar-cliente">
                            <label class="stl-label-campos-modificar-cliente stl-modificar-cliente-lbl-titulo-listado"><%--<b><u>Acciones</u></b>--%></label>
                        </div>
                    </div>
                </section>

                <asp:Repeater ID="RepeaterElegirClientes" runat="server">
                    <ItemTemplate>
                        <div class="stl-div-contenedor-listado-clientes card">
                            <div class="stl-div-apenom-cliente card-body">
                                <label class="stl-label-campos-modificar-cliente"><%# Eval("Apenom") %></label>
                            </div>

                            <div class="stl-div-telefono-cliente card-body">
                                <label class="stl-label-campos-modificar-cliente"><%# Eval("Telefono") %></label>
                            </div>

                            <div class="stl-div-mail-cliente card-body">
                                <label class="stl-label-campos-modificar-cliente"><%# Eval("Mail") %></label>
                            </div>

                            <div class="stl-div-cuitdni-cliente card-body">
                                <label class="stl-label-campos-modificar-cliente"><%# Eval("CuitDni") %></label>
                            </div>

                            <div class="stl-div-btn-modificar-eliminar-cliente">
                                <a href="Servicios.aspx?IdClienteSeleccionado=<%# Eval("ID") %>" style="text-decoration: none;">
                                    <span class="btn btn-primary btn-sm stl-btn-elegir-cliente">Seleccionar</span>
                                </a>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </section>
        </div>

        <section id="section_listar" runat="server" class="stl-section-listar">
            <section id="section_listar_lista" runat="server" class="stl-section-listar-campos">
                <asp:Panel DefaultButton="BtnListarBuscar" runat="server">
                    <div class="stl-elegir-div-busqueda">
                        <asp:TextBox ID="TxtListarBuscar" Tooltip="Se busca coincidencias con N° de Servicio, Tipo de Servicio, Tipo de Equipo, Cliente, Fecha Recepción, Fecha Devolución, Marca y Modelo, Costo Total" PlaceHolder="Búsqueda..." runat="server" CssClass="stl-texto-campo form-control stl-elegir-cliente-txt-buscar" />
                        <asp:Button ID="BtnListarBuscar" Text="Buscar" runat="server" CssClass="btn btn-dark stl-btn-listar-buscar" OnClick="BtnListarBuscar_Click" />
                        <asp:Button ID="BtnListarCancelar" Text="Cancelar" runat="server" CssClass="btn btn-dark" OnClick="BtnListarCancelar_Click" />
                    </div>
                </asp:Panel>

                <br />

                <section id="section_listar_modificar_lista_titulo">
                    <div class="stl-div-titulos-listado-servicios card">
                        <div class="stl-div-num-servicio stl-div-titulo-campo-listado-servicios card-body">
                            <label class="stl-label-campos-modificar-servicio"><b><u>N°</u></b></label>
                        </div>

                        <div class="stl-div-cliente-servicio stl-div-titulo-campo-listado-servicios card-body">
                            <label class="stl-label-campos-modificar-servicio stl-lbl-cliente-listar-servicios"><b><u>Cliente</u></b></label>
                        </div>

                        <div class="stl-div-fecha-recepcion-servicio stl-div-titulo-campo-listado-servicios card-body">
                            <label class="stl-label-campos-modificar-servicio"><b><u>Recepción</u></b></label>
                        </div>

                        <div class="stl-div-fecha-devolucion-servicio stl-div-titulo-campo-listado-servicios card-body">
                            <label class="stl-label-campos-modificar-servicio"><b><u>Devolución</u></b></label>
                        </div>

                        <div class="stl-div-tipo-servicio stl-div-titulo-campo-listado-servicios card-body">
                            <label class="stl-label-campos-modificar-servicio"><b><u>Tipo Servicio</u></b></label>
                        </div>

                        <div class="stl-div-tipo-equipo-servicio stl-div-titulo-campo-listado-servicios card-body">
                            <label class="stl-label-campos-modificar-servicio"><b><u>Tipo Equipo</u></b></label>
                        </div>

                        <div class="stl-div-subtotal-servicio stl-div-titulo-campo-listado-servicios card-body">
                            <label class="stl-label-campos-modificar-servicio"><b><u>Subtotal</u></b></label>
                        </div>

                        <div class="stl-div-btn-modificar-eliminar-servicio">
                            <label class="stl-label-campos-modificar-servicio stl-modificar-servicio-lbl-titulo-listado"><%--<b><u>Acciones</u></b>--%></label>
                        </div>
                    </div>
                </section>

                <asp:Repeater ID="RepeaterListarServicios" runat="server">
                    <ItemTemplate>
                        <div class="stl-div-contenedor-listado-servicios card">
                            <div class="stl-div-num-servicio card-body">
                                <label class="stl-label-campos-modificar-servicio"><%# Eval("ID") %></label>
                            </div>

                            <div class="stl-div-cliente-servicio card-body">
                                <label class="stl-label-campos-modificar-servicio stl-lbl-cliente-listar-servicios"><%# Eval("Cliente.Apenom") %></label>
                            </div>

                            <div class="stl-div-fecha-recepcion-servicio card-body">
                                <label class="stl-label-campos-modificar-servicio"><%# Eval("FechaRecepcion") %></label>
                            </div>

                            <div class="stl-div-fecha-devolucion-servicio card-body">
                                <label class="stl-label-campos-modificar-servicio"><%# Eval("FechaDevolucion") %></label>
                            </div>

                            <div class="stl-div-tipo-servicio card-body">
                                <label class="stl-label-campos-modificar-servicio"><%# Eval("TipoServicio.Descripcion") %></label>
                            </div>

                            <div class="stl-div-tipo-equipo-servicio card-body">
                                <label class="stl-label-campos-modificar-servicio"><%# Eval("Equipo.Tipo.Descripcion") %></label>
                            </div>

                            <div class="stl-div-subtotal-servicio card-body">
                                <label class="stl-label-campos-modificar-servicio">$ <%# Eval("CostoTotal") %></label>
                            </div>

                            <div class="stl-div-btn-modificar-eliminar-servicio">
                                <a href="Servicios.aspx?IdServicioSeleccionado=<%# Eval("ID") %>&AccionServicio=CargarCamposModificar" style="text-decoration: none;">
                                    <span class="btn btn-primary btn-sm stl-btn-modificar-servicio">Modificar</span>
                                </a>
                                <a href="Servicios.aspx?IdServicioSeleccionado=<%# Eval("ID") %>&AccionServicio=ConfirmarEliminar" style="text-decoration: none;">
                                    <span class="btn btn-danger btn-sm stl-btn-eliminar-servicio">Eliminar</span>
                                </a>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </section>

            <asp:Panel DefaultButton="BtnConfirmarModificar" runat="server">
                <section id="section_listar_modificar" runat="server" class="stl-section-listar-modificar">
                    <section id="section_listar_modificar_campos" runat="server">
                        <div class="stl-div-campos">
                            <asp:HiddenField ID="HfIdModificarEliminarServicio" runat="server" />
                            <div class="stl-div-contenido-campo">
                                <asp:Label ID="LblListarModificarTiposEquipo" Text="Tipo de Equipo" runat="server" CssClass="stl-label-campo" />
                                <asp:DropDownList ID="DdlListarModificarTiposEquipo" AppendDataBoundItems="true" AutoPostBack="true" runat="server" CssClass="stl-texto-campo form-control stl-ddl" OnSelectedIndexChanged="DdlListarModificarTiposEquipo_SelectedIndexChanged">
                                </asp:DropDownList>
                                <b style="color: red;">*</b>
                            </div>
                            <div class="stl-div-contenido-campo">
                                <asp:Label ID="LblListarModificarTiposServicio" Text="Tipo de Servicio" runat="server" CssClass="stl-label-campo" />
                                <asp:DropDownList ID="DdlListarModificarTiposServicio" AppendDataBoundItems="true" AutoPostBack="true" runat="server" CssClass="stl-texto-campo form-control stl-ddl" OnSelectedIndexChanged="DdlListarModificarTiposServicio_SelectedIndexChanged" >
                                </asp:DropDownList>
                                <b style="color: red;">*</b>
                            </div>
                            <div class="stl-div-contenido-campo">
                                <asp:LinkButton ID="LblListarModificarCliente" Text="Cliente" ToolTip="Haga click aquí para elegir un cliente" runat="server" CssClass="stl-label-campo stl-link-button-cliente" OnClick="LblListarModificarCliente_Click" />
                                <asp:TextBox ID="TxtListarModificarCliente" Text="" ReadOnly="true" PlaceHolder="No seleccionado..." ToolTip="Cliente seleccionado" runat="server" CssClass="stl-texto-campo form-control"/>
                                <b style="color: red;">*</b>
                            </div>
                            <div class="stl-div-contenido-campo">
                                <asp:Label ID="LblListarModificarFechaRecepcion" Text="Fecha Recepción" runat="server" CssClass="stl-label-campo" />
                                <asp:TextBox ID="TxtListarModificarFechaRecepcion" Text="" PlaceHolder="" ToolTip="" Type="date" runat="server" CssClass="stl-texto-campo form-control" />
                                <b style="color: red;">*</b>
                            </div>
                            <div class="stl-div-contenido-campo">
                                <asp:Label ID="LblListarModificarFechaDevolucion" Text="Fecha Devolución" runat="server" CssClass="stl-label-campo" />
                                <asp:TextBox ID="TxtListarModificarFechaDevolucion" Text="" PlaceHolder="" ToolTip="" Type="date" Enable="false" Visible="false" runat="server" CssClass="stl-texto-campo form-control" />
                                <asp:CheckBox ID="CbListarModificarFechaDevolucion1" Text="" runat="server" Checked="false" AutoPostBack="true" CssClass="stl-checkbox-fecha-devolucion stl-checkbox-1-fecha-devolucion" OnCheckedChanged="CbListarModificarFechaDevolucion_CheckedChanged" />
                                <asp:CheckBox ID="CbListarModificarFechaDevolucion2" Text="" runat="server" Visible="false" Checked="true" AutoPostBack="true" OnCheckedChanged="CbListarModificarFechaDevolucion_CheckedChanged" />
                            </div>
                            <div class="stl-div-contenido-campo">
                                <asp:Label ID="LblListarModificarMarcaModelo" Text="Marca y Modelo" runat="server" CssClass="stl-label-campo" />
                                <asp:TextBox ID="TxtListarModificarMarcaModelo" Text="" PlaceHolder="Marca y Modelo" ToolTip="Marca y Modelo del Equipo" runat="server" CssClass="stl-texto-campo form-control" />
                                <b style="color: red;">*</b>
                            </div>
                            <div class="stl-div-contenido-campo">
                                <asp:Label ID="LblListarModificarRam" Text="Memoria RAM" runat="server" CssClass="stl-label-campo" />
                                <asp:TextBox ID="TxtListarModificarRam" Text="" PlaceHolder="Memoria RAM" ToolTip="Marca, Modelo, Frecuencia y Velocidad de la memoria RAM del Equipo" runat="server" CssClass="stl-texto-campo form-control" />
                            </div>
                            <div class="stl-div-contenido-campo">
                                <asp:Label ID="LblListarModificarMicroprocesador" Text="Microprocesador" runat="server" CssClass="stl-label-campo" />
                                <asp:TextBox ID="TxtListarModificarMicroprocesador" Text="" PlaceHolder="Microprocesador" ToolTip="Marca, Modelo y Frecuencia del microprocesador del Equipo" runat="server" CssClass="stl-texto-campo form-control" />
                            </div>
                            <div class="stl-div-contenido-campo">
                                <asp:Label ID="LblListarModificarAlmacenamiento" Text="Almacenamiento" runat="server" CssClass="stl-label-campo" />
                                <asp:TextBox ID="TxtListarModificarAlmacenamiento" Text="" PlaceHolder="Almacenamiento" ToolTip="Marca, Modelo y Capacidad del almacenamiento del Equipo" runat="server" CssClass="stl-texto-campo form-control" />
                            </div>
                            <div class="stl-div-contenido-campo">
                                <asp:Label ID="LblListarModificarPlacaMadre" Text="Placa Madre" runat="server" CssClass="stl-label-campo" />
                                <asp:TextBox ID="TxtListarModificarPlacaMadre" Text="" PlaceHolder="Placa Madre" ToolTip="Marca y modelo de la Placa Madre" runat="server" CssClass="stl-texto-campo form-control" />
                            </div>
                            <div class="stl-div-contenido-campo">
                                <asp:Label ID="LblListarModificarNumSerie" Text="N° de Serie" runat="server" CssClass="stl-label-campo" />
                                <asp:TextBox ID="TxtListarModificarNumSerie" Text="" PlaceHolder="Número de Serie" ToolTip="Número de serie del Equipo" runat="server" CssClass="stl-texto-campo form-control" />
                            </div>
                            <div class="stl-div-contenido-campo">
                                <asp:Label ID="LblListarModificarAdicionales" Text="Adicionales" runat="server" CssClass="stl-label-campo" />
                                <asp:TextBox ID="TxtListarModificarAdicionales" Text="" PlaceHolder="Adicionales" ToolTip="Adicionales recibidos (Cargador, Cables, etc.)" runat="server" CssClass="stl-texto-campo form-control" />
                            </div>
                            <div class="stl-div-contenido-campo">
                                <asp:Label ID="LblListarModificarAlimentacion" Text="Alimentación" runat="server" CssClass="stl-label-campo" />
                                <asp:TextBox ID="TxtListarModificarAlimentacion" Text="" PlaceHolder="Alimentación" ToolTip="Tipo de alimentación del Equipo (Batería, Fuente, etc.)" runat="server" CssClass="stl-texto-campo form-control" />
                            </div>
                            <div class="stl-div-contenido-campo">
                                <asp:Label ID="LblListarModificarUnidadOptica" Text="Unidad Óptica" runat="server" CssClass="stl-label-campo" />
                                <asp:DropDownList ID="DdlListarModificarUnidadOptica" AppendDataBoundItems="true" runat="server" CssClass="stl-texto-campo form-control stl-ddl">
                                </asp:DropDownList>
                            </div>
                            <div class="stl-div-contenido-campo">
                                <asp:Label ID="LblListarModificarDescripcion" Text="Descripción" runat="server" CssClass="stl-label-campo stl-label-descripcion" />
                                <asp:TextBox ID="TxtListarModificarDescripcion" Text="" TextMode="MultiLine" onpaste="return false" PlaceHolder="Descripción" ToolTip="Breve descripción del Servicio realizado" onkeypress="javascript:return validarDescripcion(event)" runat="server" CssClass="stl-texto-campo form-control" />
                                <b style="display: inline-block; color: red; vertical-align: top; margin-top: 15px;">*</b>
                            </div>
                            <div class="stl-div-contenido-campo">
                                <asp:Label ID="LblListarModificarCostoRepuestos" Text="$ Repuestos" runat="server" CssClass="stl-label-campo" />
                                <asp:TextBox ID="TxtListarModificarCostoRepuestos" Text="" PlaceHolder="$ Repuestos" onpaste="return false" ToolTip="Costo de repuestos" onkeypress="javascript:return soloNumerosCostosModificarServicio3(event)" runat="server" CssClass="stl-texto-campo form-control" />
                            </div>
                                <div class="stl-div-contenido-campo">
                                <asp:Label ID="LblListarModificarHonorarios" Text="$ Mano de Obra" runat="server" CssClass="stl-label-campo" />
                                <asp:TextBox ID="TxtListarModificarHonorarios" Text="" PlaceHolder="$ Mano de Obra" onpaste="return false" ToolTip="Costo de mano de obra propio" onkeypress="javascript:return soloNumerosCostosModificarServicio2(event)" runat="server" CssClass="stl-texto-campo form-control" />
                                <b style="color: red;">*</b>
                            </div>
                            <div class="stl-div-contenido-campo">
                                <asp:Label ID="LblListarModificarCostoTerceros" Text="$ Terceros" runat="server" CssClass="stl-label-campo" />
                                <asp:TextBox ID="TxtListarModificarCostoTerceros" Text="" PlaceHolder="$ Terceros" onpaste="return false" ToolTip="Costo de Servicios terciarizados" onkeypress="javascript:return soloNumerosCostosModificarServicio1(event)" runat="server" CssClass="stl-texto-campo form-control" />
                            </div>
                        </div>
                    </section>

                    <br />
                    
                    <section id="section_listar_modificar_botones" runat="server">
                        <div class="stl-div-boton">
                            <asp:Button id="BtnConfirmarModificar" Text="Confirmar cambios" runat="server" CssClass="btn btn-dark stl-btn-confirmar" OnClick="BtnConfirmarModificar_Click" />
                            <asp:Button id="BtnCancelarModificar" Text="Cancelar" runat="server" CssClass="btn btn-dark stl-btn-cancelar" OnClick="BtnCancelarModificar_Click" />
                        </div>
                    </section>
                </section>
            </asp:Panel>

            <br />

            <asp:Panel DefaultButton="BtnCancelarEliminar" runat="server">
                <section id="section_listar_eliminar" runat="server" class="stl-section-listar-eliminar">
                    <div class="stl-div-lbl-confirmar-eliminar">
                        <h3><asp:Label ID="LblConfirmarEliminar" Text="" runat="server" /></h3>
                    </div>

                    <br />

                    <div class="stl-div-boton">
                        <asp:Button id="BtnConfirmarEliminar" Text="Confirmar" runat="server" CssClass="btn btn-danger stl-btn-confirmar" OnClick="BtnConfirmarEliminar_Click" />
                        <asp:Button id="BtnCancelarEliminar" Text="Cancelar" runat="server" CssClass="btn btn-dark stl-btn-cancelar" OnClick="BtnCancelarEliminar_Click" />
                    </div>
                </section>
            </asp:Panel>
        </section>

    </div>

    <asp:HiddenField ID="hfMessage" runat="server" />
    <asp:HiddenField ID="hfError" runat="server" />

</asp:Content>
