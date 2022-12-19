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
                <asp:Button ID="BtnBotonPrincipalNuevoCliente" Text="Nuevo Cliente" runat="server" CssClass="btn btn-dark stl-boton-principal agregar-cliente" OnClick="BtnBotonPrincipalNuevoCliente_Click" />
                <asp:Button ID="BtnBotonPrincipalModificarCliente" Text="Modificar Cliente" runat="server" CssClass="btn btn-dark stl-boton-principal modificar-cliente" onclick="BtnBotonPrincipalModificarCliente_Click" />
                <asp:Button ID="BtnBotonPrincipalLocalidades" Text="ABM Localidades" runat="server" CssClass="btn btn-dark stl-boton-principal abm-localidades" OnClick="BtnBotonPrincipalLocalidades_Click" />
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
                <section id="section_listado_modificar_cliente" runat="server">
                    <asp:Panel DefaultButton="BtnModificarClienteBusqueda" runat="server">
                        <section id="section-modificar-cliente-busqueda">
                            <div class="stl-modificar-cliente-div-busqueda">
                                <asp:TextBox ID="TxtModificarClienteBusqueda" Tooltip="Se busca coincidencias con Nombres, Apellidos, Telefono, CUIT/DNI, Mail, Dirección y/o Localidad" PlaceHolder="Búsqueda..." runat="server" CssClass="stl-texto-campo form-control stl-modificar-cliente-txt-buscar" />
                                <asp:Button ID="BtnModificarClienteBusqueda" Text="Buscar" runat="server" CssClass="btn btn-dark" OnClick="BtnModificarClienteBusqueda_Click" />
                                <asp:Button ID="BtnModificarClienteCancelar" Text="Cancelar" runat="server" CssClass="btn btn-dark stl-btn-cancelar-modificar" OnClick="BtnModificarClienteCancelar_Click" />
                            </div>
                        </section>

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
                    </asp:Panel>
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
                                    <asp:TextBox ID="TxtModificarClienteCuitDni" placeholder="CUIT/DNI" onkeypress="javascript:return soloNumerosModificarCliente(event)" runat="server" CssClass="stl-texto-campo form-control" MaxLength="11" />
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

                        <div class="stl-div-lbl-confirmar-eliminar-cliente">
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
                <h1>ABM Localidades</h1>
            </div>
            <div class="stl-div-campos">

            </div>
            <div class="stl-div-boton">
                <asp:Button ID="BtnLocalidadesCancelar" Text="Cancelar" runat="server" CssClass="btn btn-dark stl-btn-cancelar-localidades" OnClick="BtnLocalidadesCancelar_Click" />
            </div>
        </section>

    </div>

    <asp:HiddenField ID="hfMessage" runat="server" />
    <asp:HiddenField ID="hfError" runat="server" />

</asp:Content>
