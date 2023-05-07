<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="true" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="CompuGross_Web.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <link href="Styles/Style_Clientes.css?v=2.0" rel="stylesheet" />
    <script defer src="JavaScript/Script_Clientes.js" ></script>

    <div class="stl-div-content">

        <section id="section_botones_principales" class="stl-section-botones-principales" runat="server">

            <center>
                <h1>Clientes</h1>
            </center>

            <div class="stl-div-botones-principales">
                <asp:Button ID="BtnBotonPrincipalNuevoCliente" Text="Agregar Nuevo" runat="server" CssClass="btn btn-dark stl-boton-principal agregar-cliente" OnClick="BtnBotonPrincipalNuevoCliente_Click" />
                <asp:Button ID="BtnBotonPrincipalModificarCliente" Text="Listar Todos" runat="server" CssClass="btn btn-dark stl-boton-principal modificar-cliente" onclick="BtnBotonPrincipalModificarCliente_Click" />
                <asp:Button ID="BtnBotonPrincipalLocalidades" Text="Localidades" runat="server" CssClass="btn btn-dark stl-boton-principal abm-localidades" OnClick="BtnBotonPrincipalLocalidades_Click" />
            </div>
        </section>

        <asp:Panel DefaultButton="BtnNuevoClienteConfirmar" runat="server">
        
        <section id="section_agregar_cliente" runat="server" class="stl-section-agregar-cliente">
            <div class="stl-div-contenedor-campos">
                <div class="stl-div-titulo">
                    <h1>Nuevo Cliente</h1>
                </div>
                <br />
                <div class="stl-div-campos">
                    <div class="stl-div-cuit-dni">
                        <asp:Label ID="LblNuevoClienteCuitDni" Text="CUIT/DNI" runat="server" CssClass="stl-label-campo" />
                        <asp:TextBox ID="TxtNuevoClienteCuitDni" placeholder="CUIT/DNI" onkeypress="javascript:return soloNumeros(event)" runat="server" CssClass="stl-texto-campo form-control" MaxLength="11" />
                    </div>
                    <div class="stl-div-apenom">
                        <asp:Label ID="LblNuevoClienteApenom" Text="Apellido y Nombre" runat="server" CssClass="stl-label-campo" />
                        <asp:TextBox ID="TxtNuevoClienteApenom" placeholder="Apellido y Nombre" onkeypress="javascript:return soloLetrasNuevoCliente(event)" runat="server" CssClass="stl-texto-campo form-control" MaxLength="50" />
                        <b style="color: red;">*</b>
                    </div>
                    <div class="stl-div-direccion">
                        <asp:Label ID="LblNuevoClienteDireccion" Text="Dirección" runat="server" CssClass="stl-label-campo" />
                        <asp:TextBox ID="TxtNuevoClienteDireccion" placeholder="Dirección" runat="server" onkeypress="javascript:return soloEspacioLetrasNumerosNuevoCliente(event)" CssClass="stl-texto-campo form-control" MaxLength="50" />
                    </div>
                    <div class="stl-div-localidad">
                        <asp:Label ID="LblNuevoClienteLocalidad" Text="Localidad" runat="server" CssClass="stl-label-campo" />
                        <asp:DropDownList ID="DdlNuevoClienteLocalidad" runat="server" AppendDataBoundItems="true" CssClass="stl-texto-campo form-control stl-ddl-localidad">
                        </asp:DropDownList>
                    </div>
                    <div class="stl-div-telefono">
                        <asp:Label ID="LblNuevoClienteTelefono" Text="Teléfono" runat="server" CssClass="stl-label-campo" />
                        <asp:TextBox ID="TxtNuevoClienteTelefono" placeholder="Teléfono" runat="server" onkeypress="javascript:return soloNumerosNuevoCliente(event)" CssClass="stl-texto-campo form-control" MaxLength="15" />
                        <b style="color: red;">*</b>
                    </div>
                    <div class="stl-div-mail">
                        <asp:Label ID="LblNuevoClienteMail" Text="Mail" runat="server" CssClass="stl-label-campo" />
                        <asp:TextBox ID="TxtNuevoClienteMail" placeholder="Mail" runat="server" CssClass="stl-texto-campo form-control" MaxLength="50" />
                    </div>
                </div>
                <br /><br />
                <div class="stl-div-boton">
                    <asp:Button ID="BtnNuevoClienteConfirmar" Text="Confirmar Cliente" class="btn btn-dark stl-btn" runat="server" OnClick="BtnNuevoClienteConfirmar_Click" />
                    <asp:Button ID="BtnNuevoClienteCancelar" Text="Cancelar" runat="server" CssClass="btn btn-dark stl-btn-cancelar-agregar stl-btn" OnClick="BtnNuevoClienteCancelar_Click" />
                </div>
            </div>
        </section>

        </asp:panel>

        <section id="section_modificar_cliente" runat="server" class="stl-section-modificar-cliente">
            <div class="stl-div-titulo">
                <h1><asp:Label ID="LblModificarClienteTitulo" Text="" runat="server" /></h1>
            </div>

            <br />

            <div class="stl-div-campos-modificar-cliente">
                <section id="section_listado_modificar_cliente" class="stl-section-listado-modificar-cliente" runat="server">
                    <asp:Panel DefaultButton="BtnModificarClienteBusqueda" runat="server">
                        <section id="section-modificar-cliente-busqueda">
                            <div class="stl-modificar-cliente-div-busqueda">
                                <asp:TextBox ID="TxtModificarClienteBusqueda" Tooltip="Se busca coincidencias con Nombres, Apellidos, Telefono, CUIT/DNI, Mail, Dirección y/o Localidad" PlaceHolder="Búsqueda..." runat="server" CssClass="stl-texto-campo form-control stl-modificar-cliente-txt-buscar" />
                                <asp:Button ID="BtnModificarClienteBusqueda" Text="Buscar" runat="server" CssClass="btn btn-dark stl-btn-modificar-cliente-busqueda" OnClick="BtnModificarClienteBusqueda_Click" />
                                <asp:Button ID="BtnModificarClienteCancelar" Text="Volver" runat="server" CssClass="btn btn-dark stl-btn-cancelar-modificar" OnClick="BtnModificarClienteCancelar_Click" />
                            </div>
                        </section>
                    </asp:Panel>

                        <br />

                        <section id="section-modificar-cliente-titulo-listado">
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
                    
                    <asp:Repeater ID="RepeaterListadoClientes" runat="server">
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
                                    <a href="Clientes.aspx?IdCliente=<%# Eval("ID") %>&AccionCliente=CargarCamposModificar" style="text-decoration: none;">

                                        <span class="btn btn-primary btn-sm stl-btn-modificar-cliente">Modificar</span>

                                    </a>

                                <a href="Clientes.aspx?IdCliente=<%# Eval("ID") %>&AccionCliente=ConfirmarEliminar" style="text-decoration: none;">
                                        
                                        <span class="btn btn-danger btn-sm stl-btn-eliminar-cliente">Eliminar</span>

                                    </a>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </section>

                <asp:Panel DefaultButton="BtnModificarClienteConfirmar" runat="server">

                    <section id="section_campos_modificar_cliente" style="display:none;" runat="server">
                        <div class="stl-div-contenedor-campos">
                            <div class="stl-div-campos">
                                <asp:HiddenField ID="hfIdCliente" Value="0" runat="server" />
                                <div class="stl-div-cuit-dni">
                                    <asp:Label ID="LblModificarClienteCuitDni" Text="CUIT/DNI" runat="server" CssClass="stl-label-campo" />
                                    <asp:TextBox ID="TxtModificarClienteCuitDni" placeholder="CUIT/DNI" onkeypress="javascript:return soloNumeros(event)" runat="server" CssClass="stl-texto-campo form-control" MaxLength="11" />
                                </div>
                                <div class="stl-div-apenom">
                                    <asp:Label ID="LblModificarClienteApenom" Text="Apellido y Nombre" runat="server" CssClass="stl-label-campo" />
                                    <asp:TextBox ID="TxtModificarClienteApenom" placeholder="Apellido y Nombre" onkeypress="javascript:return soloLetrasModificarCliente(event)" runat="server" CssClass="stl-texto-campo form-control" MaxLength="50" />
                                    <b style="color: red;">*</b>
                                </div>
                                <div class="stl-div-direccion">
                                    <asp:Label ID="LblModificarClienteDireccion" Text="Dirección" runat="server" CssClass="stl-label-campo" />
                                    <asp:TextBox ID="TxtModificarClienteDireccion" placeholder="Dirección" runat="server" onkeypress="javascript:return soloEspacioLetrasNumerosModificarCliente(event)" CssClass="stl-texto-campo form-control" MaxLength="50" />
                                </div>
                                <div class="stl-div-localidad">
                                    <asp:Label ID="LblModificarClienteLocalidad" Text="Localidad" runat="server" CssClass="stl-label-campo" />
                                    <asp:DropDownList ID="DdlModificarClienteLocalidad" runat="server" AppendDataBoundItems="true" CssClass="stl-texto-campo form-control stl-ddl-localidad dropdown-toggle">
                                    </asp:DropDownList>
                                </div>
                                <div class="stl-div-telefono">
                                    <asp:Label ID="LblModificarClienteTelefono" Text="Teléfono" runat="server" CssClass="stl-label-campo" />
                                    <asp:TextBox ID="TxtModificarClienteTelefono" placeholder="Teléfono" runat="server" onkeypress="javascript:return soloNumerosModificarCliente(event)" CssClass="stl-texto-campo form-control" MaxLength="15" />
                                    <b style="color: red;">*</b>
                                </div>
                                <div class="stl-div-mail">
                                    <asp:Label ID="LblModificarClienteMail" Text="Mail" runat="server" CssClass="stl-label-campo" />
                                    <asp:TextBox ID="TxtModificarClienteMail" placeholder="Mail" runat="server" CssClass="stl-texto-campo form-control" MaxLength="50" />
                                </div>
                                <div class="stl-div-estado">
                                    <asp:Label ID="LblModificarClienteEstado" Text="Estado" runat="server" CssClass="stl-label-campo" />
                                    <asp:DropDownList ID="DdlModificarClienteEstado" runat="server" AppendDataBoundItems="true" CssClass="stl-texto-campo form-control stl-modificar-cliente-estado">
                                        <asp:ListItem Value="1" Text="Activo" />
                                        <asp:ListItem Value="0" Text="Inactivo" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <br /><br />
                            <div class="stl-div-boton">
                                <asp:Button ID="BtnModificarClienteConfirmar" Text="Confirmar Cambios" class="btn btn-dark stl-btn" runat="server" OnClick="BtnModificarClienteConfirmar_Click" />
                                <asp:Button ID="BtnModificarClienteCancelarEdicion" Text="Cancelar Edición" CssClass="btn btn-dark stl-btn" runat="server" OnClick="BtnModificarClienteCancelarEdicion_Click" />
                            </div>
                        </div>
                    </section>

                </asp:Panel>

                <asp:Panel DefaultButton="BtnCancelarEliminarCliente" runat="server">

                    <section id="section_confirmar_eliminar_cliente" runat="server" class="stl-section-confirmar-eliminar-cliente">

                        <div class="stl-div-lbl-confirmar-eliminar">
                            <h3><asp:Label ID="LblConfirmarEliminarCliente" Text="" runat="server" /></h3>
                        </div>

                        <br />
                        
                        <div class="stl-div-boton">
                            <asp:Button ID="BtnConfirmarEliminarCliente" Text="Confirmar" runat="server" CssClass="btn btn-danger stl-btn" onclick="BtnConfirmarEliminarCliente_Click" />
                            <asp:Button ID="BtnCancelarEliminarCliente" Text="Cancelar" runat="server" CssClass="btn btn-dark stl-btn" OnClick="BtnCancelarEliminarCliente_Click" />
                        </div>

                    </section>

                </asp:Panel>

            </div>
        </section>
        
        <section id="section_localidades" runat="server" class="stl-section-localidades">
            <div class="stl-div-titulo">
                <h1>
                    <asp:Label ID="LblLocalidadesTitulo" Text="Localidades" runat="server" CssClass="stl-localidades-lbl-titulo" />
                </h1>
            </div>

            <section id="section_localidades_principal" runat="server" class="stl-section-localidades-principal">
                <div class="stl-div-principal-localidades">
                    <asp:Button ID="BtnLocalidadesAgregar" Text="Agregar Nueva" runat="server" CssClass="btn btn-dark stl-boton-principal" OnClick="BtnLocalidadesAgregar_Click" />
                    <asp:Button ID="BtnLocalidadesListar" Text="Listar Todas" runat="server" CssClass="btn btn-dark stl-boton-principal" OnClick="BtnLocalidadesListar_Click" />
                    <asp:Button ID="BtnLocalidadesCancelar" Text="Volver" runat="server" CssClass="btn btn-dark stl-btn-cancelar-localidades stl-boton-principal" onclick="BtnLocalidadesCancelar_Click" />
                </div>
            </section>

            <asp:Panel DefaultButton="BtnLocalidadesConfirmarAgregar" runat="server">

                <section id="section_agregar_localidad" runat="server" class="stl-section-agregar-localidad">
                    <div class="stl-div-contenedor-campos">

                        <br />

                        <div class="stl-div-campos stl-div-campos-localidades">
                            <div class="stl-div-descripcion">
                                <asp:Label ID="LblLocalidadesAgregarDescripcion" Text="Descripción" runat="server" CssClass="stl-label-campo" />
                                <asp:TextBox ID="TxtLocalidadesAgregarDescripcion" Tooltip="Ingresar la Descripción de la Nueva Localidad" PlaceHolder="Descripción..." runat="server" CssClass="stl-texto-campo form-control stl-localidades-txt-agregar" />
                                <b style="color: red;">*</b>
                            </div>
                        </div>

                        <br />

                        <div class="stl-div-boton">
                            <asp:Button ID="BtnLocalidadesConfirmarAgregar" Text="Confirmar y Agregar" runat="server" CssClass="btn btn-dark stl-btn" onclick="BtnLocalidadesConfirmarAgregar_Click" />
                            <asp:Button ID="BtnLocalidadesCancelarAgregar" Text="Cancelar" runat="server" CssClass="btn btn-dark stl-btn stl-btn-cancelar-localidades" onclick="BtnLocalidadesCancelarAgregar_Click" />
                        </div>
                    </div>
                </section>
            
            </asp:Panel>

            <asp:Panel DefaultButton="BtnLocalidadesBuscar" runat="server">

                <section id="section_localidades_busqueda" runat="server" class="stl-section-localidades-busqueda">
                    <div class="stl-localidades-div-busqueda">
                        <asp:TextBox ID="TxtLocalidadesBuscar" Tooltip="Se buscan coincidencias con Descripción de Localidades" PlaceHolder="Búsqueda..." runat="server" CssClass="stl-texto-campo form-control stl-localidades-txt-buscar" />
                        <asp:Button ID="BtnLocalidadesBuscar" Text="Buscar" runat="server" CssClass="btn btn-dark" OnClick="BtnLocalidadesBuscar_Click" />
                        <asp:Button ID="BtnLocalidadesListarCancelar" Text="Volver" runat="server" CssClass="btn btn-dark stl-btn-cancelar-localidades" onclick="BtnLocalidadesListarCancelar_Click" />
                    </div>
                </section>

            </asp:Panel>

            <section id="section_localidades_listado" class="stl-section-localidades-listado" runat="server">

                <div class="stl-div-titulos-listado-localidades card">
                    <div class="stl-div-descripcion-localidad card-body">
                        <label class="stl-label-campos-listar-localidad"><b><u>Descripción</u></b></label>
                    </div>

                    <div class="stl-div-btn-modificar-eliminar-localidad">
                        <label class="stl-label-campos-modificar-localidad stl-modificar-localidad-lbl-titulo-listado"><%--<b><u>Acciones</u></b>--%></label>
                    </div>
                </div>

                <div class="stl-localidades-div-listado">
                    <asp:Repeater ID="RepeaterLocalidades" runat="server">
                        <ItemTemplate>
                            <div class="stl-div-contenedor-listado-localidades card">
                                <div class="stl-div-descripcion-localidad card-body">
                                    <label class="stl-label-campos-listar-localidad"><%# Eval("Descripcion") %></label>
                                </div>

                                <div class="stl-div-btn-modificar-eliminar-localidad">
                                    <a href="Clientes.aspx?IdLocalidad=<%# Eval("ID") %>&AccionLocalidad=CargarCamposModificar" style="text-decoration: none;">

                                        <span class="btn btn-primary btn-sm stl-btn-modificar-localidad">Modificar</span>
                                    </a>

                                    <a href="Clientes.aspx?IdLocalidad=<%# Eval("ID") %>&AccionLocalidad=ConfirmarEliminar" style="text-decoration: none;">
                                        
                                        <span class="btn btn-danger btn-sm stl-btn-eliminar-localidad">Eliminar</span>

                                    </a>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </section>

            <asp:Panel DefaultButton="BtnLocalidadesConfirmarModificar" runat="server">

                <section id="section_modificar_localidad" runat="server" class="stl-section-modificar-localidad">
                    <div class="stl-div-contenedor-campos">
                        <div class="stl-div-campos stl-div-campos-localidades">
                            <asp:HiddenField ID="HfIdLocalidadModificar" Value="0" runat="server" />
                            <div class="stl-div-descripcion">
                                <asp:Label ID="LblLocalidadesModificarDescripcion" Text="Descripción" runat="server" CssClass="stl-label-campo" />
                                <asp:TextBox ID="TxtLocalidadesModificarDescripcion" PlaceHolder="Descripción..." runat="server" CssClass="stl-texto-campo form-control stl-localidades-txt-modificar" />
                                <b style="color: red;">*</b>
                            </div>
                            <div>
                                <asp:Label ID="LblLocalidadesModificarEstado" Text="Estado" runat="server" CssClass="stl-label-campo" />
                                <asp:DropDownList ID="DdlLocalidadesModificarEstado" runat="server" AppendDataBoundItems="true" CssClass="stl-texto-campo form-control stl-modificar-localidad-estado">
                                    <asp:ListItem Value="1" Text="Activo" />
                                    <asp:ListItem Value="0" Text="Inactivo" />
                                </asp:DropDownList>
                            </div>
                        </div>

                        <br />

                        <div class="stl-div-boton">
                            <asp:Button ID="BtnLocalidadesConfirmarModificar" Text="Confirmar cambios" runat="server" CssClass="btn btn-dark stl-btn" onclick="BtnLocalidadesConfirmarModificar_Click" />
                            <asp:Button ID="BtnLocalidadesCancelarModificar" Text="Cancelar cambios" runat="server" CssClass="btn btn-dark stl-btn stl-btn-cancelar-localidades" onclick="BtnLocalidadesCancelarModificar_Click" />
                        </div>
                    </div>
                </section>
            
            </asp:Panel>

            <asp:Panel DefaultButton="BtnLocalidadesCancelarEliminar" runat="server">

                <section id="section_eliminar_localidad" runat="server" class="stl-section-eliminar-localidad">

                        <div class="stl-div-lbl-confirmar-eliminar">
                            <h3><asp:Label ID="LblLocalidadesConfirmarEliminar" Text="" runat="server" /></h3>
                        </div>

                        <br />
                        
                        <div class="stl-div-boton">
                            <asp:Button ID="BtnLocalidadesConfirmarEliminar" Text="Confirmar" runat="server" CssClass="btn btn-danger stl-btn" onclick="BtnLocalidadesConfirmarEliminar_Click" />
                            <asp:Button ID="BtnLocalidadesCancelarEliminar" Text="Cancelar" runat="server" CssClass="btn btn-dark stl-btn stl-btn-cancelar-localidades" OnClick="BtnLocalidadesCancelarEliminar_Click" />
                        </div>

                    </section>
            
            </asp:Panel>

        </section>

    </div>

    <asp:HiddenField ID="hfMessage" runat="server" />
    <asp:HiddenField ID="hfError" runat="server" />

</asp:Content>
