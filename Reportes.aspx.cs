using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Office.Interop.Excel;
using System.IO;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Drawing;
using System.Web.UI.WebControls.WebParts;
using System.EnterpriseServices;
using DocumentFormat.OpenXml.Wordprocessing;

namespace CompuGross_Web
{
    public partial class Reportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["Usuario_Logueado"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Session["listadoItems"] = null;

                Usuario usuarioLogueado = new Usuario();
                usuarioLogueado = (Usuario)Session["Usuario_Logueado"];
                UsuarioDB uDB = new UsuarioDB();
                usuarioLogueado = uDB.BuscarUsuario(usuarioLogueado);
                if (usuarioLogueado.TipoUsuario.Descripcion != "admin")
                {
                    Session["ErrorTipoUsuario"] = "ERROR\n\nUsted no tiene permiso para ingresar a este sitio";
                    Response.Redirect("Error.aspx");
                }
                else
                {
                    if (BtnExportarExcel.Visible)
                    {
                        DdlOrdenarServiciosPorCliente.Visible = true;
                    }
                    else
                    {
                        DdlOrdenarServiciosPorCliente.Visible = false;
                    }
                    
                    if (!IsPostBack)
                    {
                        CargarDesplegableAños();
                        CargarDesplegableTipoServicio();
                    }
                }
            }
        }

        private void CargarDesplegableTipoServicio()
        {
            TipoServicioDB tipoServicioDB = new TipoServicioDB();
            List<TipoServicio> listaTiposServicio = new List<TipoServicio>();
            listaTiposServicio = tipoServicioDB.Listar();

            DdlTipoServicio.Items.Clear();
            DdlTipoServicio.Items.Add("Todos");
            DdlTipoServicio.DataSource = listaTiposServicio;
            DdlTipoServicio.DataMember = "datos";
            DdlTipoServicio.DataTextField = "Descripcion";
            DdlTipoServicio.DataValueField = "ID";
            DdlTipoServicio.DataBind();
        }

        private void CargarDesplegableAños()
        {
            List<int> listaAños = new List<int>();

            int añoDesde = 2017;
            int añoHasta = DateTime.Today.Year;

            for (int i = añoDesde; i <= añoHasta; i++)
            {
                listaAños.Add(i);
            }

            int tamaño = añoHasta - añoDesde;

            for (int i = 0; i <= tamaño; i++)
            {
                DdlAños.Items.Add(listaAños[i].ToString());
            }

            DdlAños.SelectedValue = DateTime.Now.Year.ToString();
            CargarDatosAñoSeleccionado();
        }

        private void ListarIngresosPorServicio()
        {
            IngresoPorServicioDB ingresoPorServicioDB = new IngresoPorServicioDB();
            RepeaterIngresosPorServicio.DataSource = ingresoPorServicioDB.Listar();
            RepeaterIngresosPorServicio.DataBind();
        }

        private List<Servicio> ListarServicios()
        {
            ServicioDB servicioDB = new ServicioDB();
            return servicioDB.ListarTodos();
        }

        private void CargarPromediosGenerales()
        {
            try
            {
                double cantAños = DateTime.Now.Year - 2017 - 0.5;
                int totalGananciaGeneral = 0, totalServiciosRealizados = 0,
                totalDiasServicio = Convert.ToInt32(DateTime.Now.Subtract(Convert.ToDateTime("28/06/2017")).TotalDays);

                foreach (Servicio servicio in ListarServicios())
                {
                    totalGananciaGeneral += Convert.ToInt32(servicio.Honorarios);
                    totalServiciosRealizados++;
                }
                
                LblGananciaTotal.Text = "Ganancia Total: $ " + totalGananciaGeneral.ToString("N", new CultureInfo("es-AR")) + " (" + totalServiciosRealizados + " Servicios)";
                LblPromedioGananciaGeneral.Text = "Promedio de ganancia Anual: $ " + (totalGananciaGeneral / cantAños).ToString("N", new CultureInfo("es-AR"));
                LblPromedioGananciaMensual.Text = "Promedio de ganancia Mensual: $ " + ((totalGananciaGeneral / cantAños) / 12).ToString("N", new CultureInfo("es-AR"));
                LblDiasEntreServicios.Text = "Se realiza un servicio cada " + Convert.ToString(Convert.ToInt32(totalDiasServicio / totalServiciosRealizados)) + " Días.";
            }
            catch
            {
                hfError.Value = "Se produjo un error al calcular los promedios Generales";
                LblGananciaTotal.Text = "";
                LblPromedioGananciaGeneral.Text = "Error";
                LblPromedioGananciaMensual.Text = "Error";
                LblDiasEntreServicios.Text = "Error";
            }
        }

        protected void BtnIngresosGenerales_Click(object sender, EventArgs e)
        {
            ListarIngresosPorServicio();
            CargarPromediosGenerales();
            section_ingresos_generales.Style.Add("display", "block");
            section_servicios_por_cliente.Style.Add("display", "none");
        }

        protected void BtnServiciosPorCliente_Click(object sender, EventArgs e)
        {
            TxtNumCliente.Text = "";
            TxtCliente.Text = "";
            DdlTipoServicio.SelectedValue = "Todos";
            section_ingresos_generales.Style.Add("display", "none");
            section_servicios_por_cliente.Style.Add("display", "block");
            DdlOrdenarServiciosPorCliente.Visible = true;
            ListarServiciosPorCliente("Todos");
        }

        private int CalcularGananciaTotalAnual(Ganancia_Anual_y_Mensual aux)
        {
            return aux.GananciaTotalMes1 + aux.GananciaTotalMes2 + aux.GananciaTotalMes3
                 + aux.GananciaTotalMes4 + aux.GananciaTotalMes5 + aux.GananciaTotalMes6
                 + aux.GananciaTotalMes7 + aux.GananciaTotalMes8 + aux.GananciaTotalMes9
                 + aux.GananciaTotalMes10 + aux.GananciaTotalMes11 + aux.GananciaTotalMes12;
        }

        private void CompletarTextoLabels(Ganancia_Anual_y_Mensual aux, int añoSeleccionado)
        {
            LblTotalAnual.Text = "- Total Anual: $ " + CalcularGananciaTotalAnual(aux).ToString();
            if (añoSeleccionado == 2017)
            {
                LblPromedioMensual.Text = "- Promedio Mensual: $ " + (CalcularGananciaTotalAnual(aux) / 6).ToString();
            }
            else if (añoSeleccionado == DateTime.Now.Year)
            {
                LblPromedioMensual.Text = "- Promedio Mensual: $ " + (CalcularGananciaTotalAnual(aux) / DateTime.Now.Month).ToString();
            }
            else
            {
                LblPromedioMensual.Text = "- Promedio Mensual: $ " + (CalcularGananciaTotalAnual(aux) / 12).ToString();
            }
            LblCantidadServiciosAnual.Text = "- Cantidad de Servicios: " + aux.CantidadTotalServicios.ToString();
            LblTotalMes1.Text = "1-Enero:  $ " + aux.GananciaTotalMes1 + " (" + aux.CantitdadServiciosTotalMes1 + ")";
            LblTotalMes2.Text = "2-Febrero:  $ " + aux.GananciaTotalMes2 + " (" + aux.CantitdadServiciosTotalMes2 + ")";
            LblTotalMes3.Text = "3-Marzo:  $ " + aux.GananciaTotalMes3 + " (" + aux.CantitdadServiciosTotalMes3 + ")";
            LblTotalMes4.Text = "4-Abril:  $ " + aux.GananciaTotalMes4 + " (" + aux.CantitdadServiciosTotalMes4 + ")";
            LblTotalMes5.Text = "5-Mayo:  $ " + aux.GananciaTotalMes5 + " (" + aux.CantitdadServiciosTotalMes5 + ")";
            LblTotalMes6.Text = "6-Junio:  $ " + aux.GananciaTotalMes6 + " (" + aux.CantitdadServiciosTotalMes6 + ")";
            LblTotalMes7.Text = "7-Julio:  $ " + aux.GananciaTotalMes7 + " (" + aux.CantitdadServiciosTotalMes7 + ")";
            LblTotalMes8.Text = "8-Agosto:  $ " + aux.GananciaTotalMes8 + " (" + aux.CantitdadServiciosTotalMes8 + ")";
            LblTotalMes9.Text = "9-Septiembre:  $ " + aux.GananciaTotalMes9 + " (" + aux.CantitdadServiciosTotalMes9 + ")";
            LblTotalMes10.Text = "10-Octubre:  $ " + aux.GananciaTotalMes10 + " (" + aux.CantitdadServiciosTotalMes10 + ")";
            LblTotalMes11.Text = "11-Noviembre:  $ " + aux.GananciaTotalMes11 + " (" + aux.CantitdadServiciosTotalMes11 + ")";
            LblTotalMes12.Text = "12-Diciembre:  $ " + aux.GananciaTotalMes12 + " (" + aux.CantitdadServiciosTotalMes12 + ")";
        }

        private void CargarDatosAñoSeleccionado()
        {
            section_totales_ganancias_por_año.Style.Add("display", "block");
            Ganancia_Anual_y_Mensual aux = new Ganancia_Anual_y_Mensual();
            int añoSeleccionado = Convert.ToInt32(DdlAños.SelectedItem.ToString());

            foreach (Servicio servicio in ListarServicios())
            {
                if (servicio.FechaDevolucion != null)
                {
                    DateTime fechaDevolucion = Convert.ToDateTime(servicio.FechaDevolucion);
                    int añoFechaDevolucion = fechaDevolucion.Year, mesFechaDevolucion = fechaDevolucion.Month;

                    if (añoFechaDevolucion == añoSeleccionado)
                    {
                        aux.CantidadTotalServicios++;
                        switch (mesFechaDevolucion)
                        {
                            case 1:
                                aux.GananciaTotalMes1 += Convert.ToInt32(servicio.Honorarios);
                                aux.CantitdadServiciosTotalMes1++;
                                break;
                            case 2:
                                aux.GananciaTotalMes2 += Convert.ToInt32(servicio.Honorarios);
                                aux.CantitdadServiciosTotalMes2++;
                                break;
                            case 3:
                                aux.GananciaTotalMes3 += Convert.ToInt32(servicio.Honorarios);
                                aux.CantitdadServiciosTotalMes3++;
                                break;
                            case 4:
                                aux.GananciaTotalMes4 += Convert.ToInt32(servicio.Honorarios);
                                aux.CantitdadServiciosTotalMes4++;
                                break;
                            case 5:
                                aux.GananciaTotalMes5 += Convert.ToInt32(servicio.Honorarios);
                                aux.CantitdadServiciosTotalMes5++;
                                break;
                            case 6:
                                aux.GananciaTotalMes6 += Convert.ToInt32(servicio.Honorarios);
                                aux.CantitdadServiciosTotalMes6++;
                                break;
                            case 7:
                                aux.GananciaTotalMes7 += Convert.ToInt32(servicio.Honorarios);
                                aux.CantitdadServiciosTotalMes7++;
                                break;
                            case 8:
                                aux.GananciaTotalMes8 += Convert.ToInt32(servicio.Honorarios);
                                aux.CantitdadServiciosTotalMes8++;
                                break;
                            case 9:
                                aux.GananciaTotalMes9 += Convert.ToInt32(servicio.Honorarios);
                                aux.CantitdadServiciosTotalMes9++;
                                break;
                            case 10:
                                aux.GananciaTotalMes10 += Convert.ToInt32(servicio.Honorarios);
                                aux.CantitdadServiciosTotalMes10++;
                                break;
                            case 11:
                                aux.GananciaTotalMes11 += Convert.ToInt32(servicio.Honorarios);
                                aux.CantitdadServiciosTotalMes11++;
                                break;
                            case 12:
                                aux.GananciaTotalMes12 += Convert.ToInt32(servicio.Honorarios);
                                aux.CantitdadServiciosTotalMes12++;
                                break;
                        }
                    }
                }
            }

            CompletarTextoLabels(aux, añoSeleccionado);
        }

        protected void DdlAños_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDatosAñoSeleccionado();
        }

        private void ExportExcel(List<ServicioPorCliente> lista)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Application.Workbooks.Add(true);
                excel.Cells[1, 1] = "N° Cliente";
                excel.Cells[1, 2] = "Cliente";
                excel.Cells[1, 3] = "Servicios realizados";
                excel.Cells[1, 4] = "Ganancia total";
                int indiceFila = 0;

                foreach (ServicioPorCliente servicioPorCliente in lista)
                {
                    indiceFila++;
                    excel.Cells[indiceFila + 1, 1] = servicioPorCliente.Cliente.ID;
                    excel.Cells[indiceFila + 1, 2] = servicioPorCliente.Cliente.Apenom;
                    excel.Cells[indiceFila + 1, 3] = servicioPorCliente.TotalServiciosRealizados;
                    excel.Cells[indiceFila + 1, 4] = "$ " + servicioPorCliente.GananciaTotal;

                    excel.Cells[indiceFila + 1, 1].Interior.Color = ColorTranslator.ToOle(System.Drawing.Color.FromArgb(217, 225, 242));
                    excel.Cells[indiceFila + 1, 2].Interior.Color = ColorTranslator.ToOle(System.Drawing.Color.FromArgb(217, 225, 242));
                    excel.Cells[indiceFila + 1, 3].Interior.Color = ColorTranslator.ToOle(System.Drawing.Color.FromArgb(217, 225, 242));
                    excel.Cells[indiceFila + 1, 4].Interior.Color = ColorTranslator.ToOle(System.Drawing.Color.FromArgb(217, 225, 242));

                    excel.Cells[indiceFila + 1, 1].Font.Name = "Calibri";
                    excel.Cells[indiceFila + 1, 2].Font.Name = "Calibri";
                    excel.Cells[indiceFila + 1, 3].Font.Name = "Calibri";
                    excel.Cells[indiceFila + 1, 4].Font.Name = "Calibri";

                    excel.Cells[indiceFila + 1, 1].Font.Size = 11;
                    excel.Cells[indiceFila + 1, 2].Font.Size = 11;
                    excel.Cells[indiceFila + 1, 3].Font.Size = 11;
                    excel.Cells[indiceFila + 1, 4].Font.Size = 11;

                    excel.Cells[indiceFila + 1, 1].Font.Bold = true;
                    excel.Cells[indiceFila + 1, 2].Font.Bold = true;
                    excel.Cells[indiceFila + 1, 3].Font.Bold = true;
                    excel.Cells[indiceFila + 1, 4].Font.Bold = true;

                    excel.Cells[indiceFila + 1, 1].Interior.Pattern = XlPattern.xlPatternSolid;
                    excel.Cells[indiceFila + 1, 2].Interior.Pattern = XlPattern.xlPatternSolid;
                    excel.Cells[indiceFila + 1, 3].Interior.Pattern = XlPattern.xlPatternSolid;
                    excel.Cells[indiceFila + 1, 4].Interior.Pattern = XlPattern.xlPatternSolid;

                    excel.Cells[indiceFila + 1, 1].Borders.Weight = 1;
                    excel.Cells[indiceFila + 1, 2].Borders.Weight = 1;
                    excel.Cells[indiceFila + 1, 3].Borders.Weight = 1;
                    excel.Cells[indiceFila + 1, 4].Borders.Weight = 1;

                    excel.Cells[indiceFila + 1, 1].Borders.Color = ColorTranslator.ToOle(System.Drawing.Color.Black);
                    excel.Cells[indiceFila + 1, 2].Borders.Color = ColorTranslator.ToOle(System.Drawing.Color.Black);
                    excel.Cells[indiceFila + 1, 3].Borders.Color = ColorTranslator.ToOle(System.Drawing.Color.Black);
                    excel.Cells[indiceFila + 1, 4].Borders.Color = ColorTranslator.ToOle(System.Drawing.Color.Black);
                }

                Range titleRng = excel.Application.get_Range("A1:D1");
                Range contentRng = excel.Application.get_Range("A:D");
                contentRng.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                titleRng.Font.Name = "Calibri";
                titleRng.Font.Size = 13;
                titleRng.Font.Bold = true;
                titleRng.Interior.Pattern = XlPattern.xlPatternSolid;
                titleRng.Borders.Weight = 1;
                titleRng.Borders.Color = ColorTranslator.ToOle(System.Drawing.Color.Black);
                titleRng.Font.Color = ColorTranslator.ToOle(System.Drawing.Color.White);
                titleRng.Interior.Color = ColorTranslator.ToOle(System.Drawing.Color.FromArgb(49, 80, 90));
                excel.Columns[1].AutoFit();
                excel.Columns[2].AutoFit();
                excel.Columns[3].AutoFit();
                excel.Columns[4].AutoFit();
                excel.Visible = true;
            }
            catch
            {
                hfError.Value = "No se pudo generar el archivo Excel. Compruebe que el paquete Office está instalado en el equipo.";
            }
        }

        protected void BtnExportarExcel_Click(object sender, EventArgs e)
        {
            ExportExcel((List<ServicioPorCliente>)Session["ListaServiciosPorCliente"]);
        }

        private void ListarServiciosPorCliente(string seleccion)
        {
            List<ServicioPorCliente> listaServiciosPorCliente = new List<ServicioPorCliente>();
            ClienteDB clienteDB = new ClienteDB();
            ServicioDB servicioDB = new ServicioDB();

            foreach (Cliente cliente in clienteDB.ListarTodos())
            {
                ServicioPorCliente servicioPorCliente = new ServicioPorCliente();

                foreach (Servicio servicio in servicioDB.ListarTodos())
                {
                    servicioPorCliente.Cliente = cliente;
                    servicioPorCliente.TipoServicio = servicio.TipoServicio;

                    if (seleccion == "Todos")
                    {
                        if (servicio.Cliente.Apenom == cliente.Apenom)
                        {
                            servicioPorCliente.TotalServiciosRealizados++;
                            servicioPorCliente.GananciaTotal += Convert.ToDecimal(Convert.ToInt32(servicio.Honorarios).ToString("N", new CultureInfo("es-AR")));
                        }
                    }
                    else
                    {
                        if (servicio.Cliente.Apenom == cliente.Apenom && servicio.TipoServicio.Descripcion == seleccion)
                        {
                            servicioPorCliente.TotalServiciosRealizados++;
                            servicioPorCliente.GananciaTotal += Convert.ToDecimal(Convert.ToInt32(servicio.Honorarios).ToString("N", new CultureInfo("es-AR")));
                        }
                    }
                }

                if (servicioPorCliente.TotalServiciosRealizados > 0)
                {
                    listaServiciosPorCliente.Add(servicioPorCliente);
                }
            }

            if (DdlOrdenarServiciosPorCliente.SelectedItem.Text == "Ordenar Listado ↑")
            {
                listaServiciosPorCliente = listaServiciosPorCliente.OrderBy(x => x.GananciaTotal).ToList();
            }
            if (DdlOrdenarServiciosPorCliente.SelectedItem.Text == "Ordenar Listado ↓")
            {
                listaServiciosPorCliente = listaServiciosPorCliente.OrderByDescending(x => x.GananciaTotal).ToList();
            }

            Session["ListaServiciosPorCliente"] = listaServiciosPorCliente;

            RepeaterServiciosPorCliente.DataSource = listaServiciosPorCliente;
            RepeaterServiciosPorCliente.DataBind();
        }

        protected void DdlTipoServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TxtNumCliente.Text != "" || TxtCliente.Text != "")
            {
                ListarServiciosPorClienteFiltrado();
            }
            else
            {
                string seleccion = DdlTipoServicio.SelectedItem.ToString();
                ListarServiciosPorCliente(seleccion);
            }
        }

        private void ListarServiciosPorClienteFiltrado()
        {
            section_ingresos_generales.Style.Add("display", "none");

            ListarServiciosPorCliente("Todos");

            Cliente cliente = new Cliente();
            cliente.ID = 0;
            if (TxtNumCliente.Text != "")
            {
                cliente.ID = Convert.ToInt64(TxtNumCliente.Text);
            }
            cliente.Apenom = TxtCliente.Text;
            TipoServicio tipoServicioSeleccionado = new TipoServicio();
            tipoServicioSeleccionado.Descripcion = DdlTipoServicio.SelectedItem.ToString();
            List<ServicioPorCliente> listaServiciosPorClienteNueva = new List<ServicioPorCliente>();

            foreach (ServicioPorCliente servicioPorCliente in (List<ServicioPorCliente>)Session["ListaServiciosPorCliente"])
            {
                if (tipoServicioSeleccionado.Descripcion == "Todos")
                {
                    if (cliente.ID > 0 && cliente.Apenom != "")
                    {
                        if (servicioPorCliente.Cliente.Apenom.ToUpper().Contains(cliente.Apenom.ToUpper())
                            && servicioPorCliente.Cliente.ID.ToString().Contains(cliente.ID.ToString()))
                        {
                            listaServiciosPorClienteNueva.Add(servicioPorCliente);
                        }
                    }
                    else if (cliente.ID > 0 && cliente.Apenom == "")
                    {
                        if (servicioPorCliente.Cliente.ID.ToString().Contains(cliente.ID.ToString()))
                        {
                            listaServiciosPorClienteNueva.Add(servicioPorCliente);
                        }
                    }
                    else if (cliente.ID == 0 && cliente.Apenom != "")
                    {
                        if (servicioPorCliente.Cliente.Apenom.ToUpper().Contains(cliente.Apenom.ToUpper()))
                        {
                            listaServiciosPorClienteNueva.Add(servicioPorCliente);
                        }
                    }
                    else if (cliente.ID == 0 && cliente.Apenom == "")
                    {
                        listaServiciosPorClienteNueva.Add(servicioPorCliente);
                    }
                }
                else
                {
                    if (cliente.ID > 0 && cliente.Apenom != "")
                    {
                        if (servicioPorCliente.Cliente.Apenom.ToUpper().Contains(cliente.Apenom.ToUpper())
                            && servicioPorCliente.Cliente.ID.ToString().Contains(cliente.ID.ToString())
                            && servicioPorCliente.TipoServicio.Descripcion == tipoServicioSeleccionado.Descripcion)
                        {
                            listaServiciosPorClienteNueva.Add(servicioPorCliente);
                        }
                    }
                    else if (cliente.ID > 0 && cliente.Apenom == "")
                    {
                        if (servicioPorCliente.Cliente.ID.ToString().Contains(cliente.ID.ToString())
                            && servicioPorCliente.TipoServicio.Descripcion == tipoServicioSeleccionado.Descripcion)
                        {
                            listaServiciosPorClienteNueva.Add(servicioPorCliente);
                        }
                    }
                    else if (cliente.ID == 0 && cliente.Apenom != "")
                    {
                        if (servicioPorCliente.Cliente.Apenom.ToUpper().Contains(cliente.Apenom.ToUpper())
                            && servicioPorCliente.TipoServicio.Descripcion == tipoServicioSeleccionado.Descripcion)
                        {
                            listaServiciosPorClienteNueva.Add(servicioPorCliente);
                        }
                    }
                    else if (cliente.ID == 0 && cliente.Apenom == "")
                    {
                        if (servicioPorCliente.TipoServicio.Descripcion == tipoServicioSeleccionado.Descripcion)
                        {
                            listaServiciosPorClienteNueva.Add(servicioPorCliente);
                        }
                    }
                }
            }

            if (DdlOrdenarServiciosPorCliente.SelectedItem.Text == "Ascendente")
            {
                listaServiciosPorClienteNueva = listaServiciosPorClienteNueva.OrderBy(x => x.GananciaTotal).ToList();
            }
            if (DdlOrdenarServiciosPorCliente.SelectedItem.Text == "Descendente")
            {
                listaServiciosPorClienteNueva = listaServiciosPorClienteNueva.OrderByDescending(x => x.GananciaTotal).ToList();
            }

            Session["ListaServiciosPorCliente"] = listaServiciosPorClienteNueva;

            RepeaterServiciosPorCliente.DataSource = listaServiciosPorClienteNueva;
            RepeaterServiciosPorCliente.DataBind();
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            ListarServiciosPorClienteFiltrado();
        }

        protected void DdlOrdenarServiciosPorCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListarServiciosPorCliente("Todos");
        }
    }
}