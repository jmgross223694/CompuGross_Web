<%@ Page Title="Reportes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="CompuGross_Web.Reportes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="Styles/Style_Reportes.css?v=2.0" rel="stylesheet" />
    <script defer src="JavaScript/Script_Reportes.js" ></script>

    <section id="section_titulo" runat="server">
        <div class="stl-div-titulo">
            <h1>
                <asp:Label ID="LblTitulo" Text="Reportes" runat="server" CssClass="stl-lbl-titulo" />
            </h1>
        </div>
    </section>

    <section id="section_botones_principales" runat="server">
        <div class="stl-div-btn-principales">
            <asp:Button ID="BtnIngresosGenerales" Text="Ingresos Generales" runat="server" CssClass="stl-btn-ingresos-generales btn btn-dark" OnClick="BtnIngresosGenerales_Click" />
            <asp:Button ID="BtnServiciosPorCliente" Text="Servicios Por Cliente" runat="server" CssClass="stl-btn-servicios-por-cliente btn btn-dark" OnClick="BtnServiciosPorCliente_Click" />
        </div>
    </section>

    <section id="section_ingresos_generales" runat="server" class="stl-section-ingresos-generales">
        <div class="stl-div-contenedor-ingresos-generales">
            <div class="stl-div-contenedor-ganancias-por-tipo-de-servicio">
                <div class="stl-div-titulo-ganancias-por-tipo-de-servicio">
                    <h3 class="stl-titulo">Totales por Tipo de Servicio</h3>
                </div>
                <div class="stl-div-ingresos-por-servicio">
                        <div class="stl-div-contenedor-titulos card">
                            <div class="stl-div-cantidad card-body">
                                <label class="stl-label-titulo stl-label-titulo-cantidad"><u>Cantidad</u></label>
                                <label class="stl-label-titulo stl-label-titulo-cantidad-abreviado"><u>Cant.</u></label>
                            </div>

                            <div class="stl-div-tipo-servicio card-body">
                                <label class="stl-label-titulo"><u>Tipo de Servicio</u></label>
                            </div>

                            <div class="stl-div-ganancia card-body">
                                <label class="stl-label-titulo"><u>Ganancia</u></label>
                            </div>
                        </div>
                    <asp:Repeater ID="RepeaterIngresosPorServicio" runat="server">
                        <ItemTemplate>
                            <div class="stl-div-contenedor-campos card">
                                <div class="stl-div-cantidad card-body">
                                    <label class="stl-label-campo"><%# Eval("Cantidad") %></label>
                                </div>

                                <div class="stl-div-tipo-servicio card-body">
                                    <label class="stl-label-campo"><%# Eval("TipoServicio.Descripcion") %></label>
                                </div>

                                <div class="stl-div-ganancia card-body">
                                    <label class="stl-label-campo">$ <%# Eval("Ganancia") %></label>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <div class="stl-div-total-ganancia card card-body">
                    <asp:Label ID="LblGananciaTotal" Text="" runat="server" />
                </div>

            </div>

            <div class="stl-div-contenedor-promedios-generales">

                <div class="stl-div-promedios-generales">
                    <div class="stl-div-titulo">
                        <h3 class="stl-titulo">Promedios Generales</h3>
                    </div>
                    <div class="stl-div-campos">
                        <asp:Label ID="LblPromedioGananciaGeneral" Text="" runat="server" CssClass="stl-label-promedio stl-lbl-promedio-ganancia card card-body" />
                        <asp:Label ID="LblPromedioGananciaMensual" Text="" runat="server" CssClass="stl-label-promedio stl-lbl-promedio-ganancia card card-body" />
                        <asp:Label ID="LblDiasEntreServicios" Text="" runat="server" CssClass="stl-label-promedio stl-lbl-promedio-dias card card-body" />
                    </div>
                </div>

            </div>

        </div>

        <div class="stl-div-ganancias-por-año">
            <div class="stl-div-desplegable-años">
                <div class="stl-div-select-año">
                    <h3>Seleccione un año: </h3>
                    <asp:DropDownList ID="DdlAños" runat="server" size="1" CssClass="stl-ddl-años btn btn-dark" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="DdlAños_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>

            <section id="section_totales_ganancias_por_año" runat="server" class="stl-section-ganancias-por-año">
                <div class="stl-div-titulo-ganancias-por-año">
                    <h3 class="stl-titulo">Ganancias por Año y por Mes</h3>
                </div>
                <div class="stl-div-totales-anuales card">
                    <asp:Label ID="LblTotalAnual" Text="" runat="server" CssClass="stl-lbl-total-anual card card-body" />
                    <asp:Label ID="LblPromedioMensual" Text="" runat="server" CssClass="stl-lbl-promedio-mensual card card-body" />
                    <asp:Label ID="LblCantidadServiciosAnual" Text="" runat="server" CssClass="stl-lbl-cantidad-servicios-anual card card-body" />
                </div>
                <div class="stl-div-titulo-ganancias-por-mes">
                    <h3 class="stl-titulo">Ganancias por Mes (Cant. Servicios realizados)</h3>
                </div>
                <div class="stl-div-totales-por-mes card">
                    <div class="stl-div-trimestre card card-body">
                        <div class="stl-div-total-mes">
                            <asp:Label ID="LblTotalMes1" Text="" runat="server" CssClass="stl-lbl-total-mes" />
                        </div>
                        <div class="stl-div-total-mes">
                            <asp:Label ID="LblTotalMes2" Text="" runat="server" CssClass="stl-lbl-total-mes" />
                        </div>
                        <div class="stl-div-total-mes">
                            <asp:Label ID="LblTotalMes3" Text="" runat="server" CssClass="stl-lbl-total-mes" />
                        </div>
                    </div>
                    <div class="stl-div-trimestre card card-body">
                        <div class="stl-div-total-mes">
                            <asp:Label ID="LblTotalMes4" Text="" runat="server" CssClass="stl-lbl-total-mes" />
                        </div>
                        <div class="stl-div-total-mes">
                            <asp:Label ID="LblTotalMes5" Text="" runat="server" CssClass="stl-lbl-total-mes" />
                        </div>
                        <div class="stl-div-total-mes">
                            <asp:Label ID="LblTotalMes6" Text="" runat="server" CssClass="stl-lbl-total-mes" />
                        </div>
                    </div>
                    <div class="stl-div-trimestre card card-body">
                        <div class="stl-div-total-mes">
                            <asp:Label ID="LblTotalMes7" Text="" runat="server" CssClass="stl-lbl-total-mes" />
                        </div>
                        <div class="stl-div-total-mes">
                            <asp:Label ID="LblTotalMes8" Text="" runat="server" CssClass="stl-lbl-total-mes" />
                        </div>
                        <div class="stl-div-total-mes">
                            <asp:Label ID="LblTotalMes9" Text="" runat="server" CssClass="stl-lbl-total-mes" />
                        </div>
                    </div>
                    <div class="stl-div-trimestre card card-body">
                        <div class="stl-div-total-mes">
                            <asp:Label ID="LblTotalMes10" Text="" runat="server" CssClass="stl-lbl-total-mes" />
                        </div>
                        <div class="stl-div-total-mes">
                            <asp:Label ID="LblTotalMes11" Text="" runat="server" CssClass="stl-lbl-total-mes" />
                        </div>
                        <div class="stl-div-total-mes">
                            <asp:Label ID="LblTotalMes12" Text="" runat="server" CssClass="stl-lbl-total-mes" />
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </section>

    <section id="section_servicios_por_cliente" class="stl-section-servicios-por-cliente" runat="server">
        <div class="stl-div-contenedor-servicios-por-cliente">
                <div class="stl-div-btn-exportar">
                    <asp:Button ID="BtnExportarExcel" Text="Exportar a Excel" runat="server" CssClass="stl-btn-exportar btn btn-success" OnClick="BtnExportarExcel_Click" />
                </div>
            
            <div class="stl-div-listado-servicios-por-cliente">
                <asp:Panel DefaultButton="BtnBuscar" runat="server">
                    <div class="stl-div-contenedor-titulos stl-titulos-servicios-por-cliente card">
                        <div class="stl-div-num-cliente card-body">
                            <asp:TextBox ID="TxtNumCliente" runat="server" CssClass="stl-label-titulo btn btn-dark form-control stl-txt-num-cliente" ToolTip="Se busca coincidencias con N° Cliente" PlaceHolder="N° Cliente" onpaste="return false" />
                        </div>

                        <div class="stl-div-cliente card-body">
                            <asp:TextBox ID="TxtCliente" runat="server" CssClass="stl-label-titulo btn btn-dark form-control stl-txt-cliente" ToolTip="Se busca coincidencias con Clientes" PlaceHolder="Cliente" onpaste="return false" />
                        </div>

                        <div class="stl-div-servicios card-body">
                            <asp:DropDownList ID="DdlTipoServicio" AppendDataBoundItems="true" AutoPostBack="true" runat="server" CssClass="stl-label-titulo form-control btn btn-dark stl-ddl-tipo-servicio" OnSelectedIndexChanged="DdlTipoServicio_SelectedIndexChanged">
                                <asp:ListItem Text="Todos" />
                            </asp:DropDownList>
                        </div>
                    </div>

                    <asp:Button ID="BtnBuscar" CssClass="stl-btn-buscar" runat="server" OnClick="BtnBuscar_Click" />

                </asp:Panel>
                    <asp:Repeater ID="RepeaterServiciosPorCliente" runat="server">
                        <ItemTemplate>
                            <div class="stl-div-contenedor-campos stl-campos-servicios-por-cliente card">
                                <div class="stl-div-num-cliente card-body">
                                    <label class="stl-label-campo stl-lbl-campo-servicios-por-cliente"><%# Eval("Cliente.ID") %></label>
                                </div>

                                <div class="stl-div-cliente card-body">
                                    <label class="stl-label-campo stl-lbl-campo-servicios-por-cliente"><%# Eval("Cliente.Apenom") %></label>
                                </div>

                                <div class="stl-div-servicios card-body">
                                    <label class="stl-label-campo stl-lbl-campo-servicios-por-cliente"><%# Eval("TotalServiciosRealizados") %> ($ <%# Eval("GananciaTotal") %>)</label>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
            </div>
        </div>
    </section>

    <asp:HiddenField ID="hfMessage" runat="server" />
    <asp:HiddenField ID="hfError" runat="server" />

</asp:Content>
