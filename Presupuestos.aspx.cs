using Dominio;
using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows.Forms;
using System.Collections;
using iTextSharp.text.html.simpleparser;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Data;
using System.Data.Common;

namespace CompuGross_Web
{
    public partial class Presupuestos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario_Logueado"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                TxtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                if (Session["listadoItems"] == null)
                {
                    section_totales_listado.Style.Add("display", "none");
                }
                
                if (Request.QueryString["CodigoItem"] != null)
                {
                    string codigo = (string)Request.QueryString["CodigoItem"];
                    EliminarItem(codigo);
                }

                if (TxtCliente.Text != "")
                {
                    PrimeraMayusculaCliente();
                }
            }
        }

        private void PrimeraMayusculaCliente()
        {
            string cliente = TxtCliente.Text;
            int len = cliente.Length;
            string aux = "";

            for (int i = 0; i < len; i++)
            {
                if (i == 0)
                {
                    aux = cliente[0].ToString().ToUpper();
                }
                else
                {
                    aux = aux + cliente[i].ToString();
                }
            }

            TxtCliente.Text = aux;
        }

        private bool ValidarCamposItem()
        {
            if (TxtCodigo.Text != "" && TxtDescripcion.Text != "" 
                && TxtCantidad.Text != "" && TxtCantidad.Text != "0" 
                && TxtPrecioUnitario.Text != "" && TxtPrecioUnitario.Text != "0")
            {
                if (Convert.ToDecimal(TxtCantidad.Text) > 0 && Convert.ToDecimal(TxtPrecioUnitario.Text) > 0)
                {
                    return true;
                }
            }

            return false;
        }

        private ItemPresupuesto CargarCamposItem()
        {
            ItemPresupuesto item = new ItemPresupuesto();

            item.Codigo = TxtCodigo.Text;
            item.Cantidad = Convert.ToDecimal(TxtCantidad.Text);
            item.Precio = Convert.ToDecimal(TxtPrecioUnitario.Text);

            string aux = TxtDescripcion.Text;
            string aux2 = "";
            for (int i = 0; i < aux.Length; i++)
            {
                if (i == 0)
                {
                    aux2 = aux[i].ToString().ToUpper();
                }
                else
                {
                    aux2 = aux2 + aux[i];
                }
            }

            item.Descripcion = aux2;
            item.Subtotal = item.Precio*item.Cantidad;

            return item;
        }

        private void CargarRepeaterListado()
        {
            Presupuesto presupuesto = new Presupuesto();
            List<ItemPresupuesto> lista = new List<ItemPresupuesto>();
            presupuesto.ListaItems = lista;

            if (Session["listadoItems"] != null)
            {
                presupuesto.ListaItems = (List<ItemPresupuesto>)Session["listadoItems"];
            }

            presupuesto.TotalItems = 0;
            presupuesto.TotalMonto = 0;

            foreach (ItemPresupuesto item in presupuesto.ListaItems)
            {
                presupuesto.TotalItems += item.Cantidad;
                presupuesto.TotalMonto += item.Subtotal;
            }

            LblTotalItems.Text = "Cantidad items: " + presupuesto.TotalItems.ToString();
            LblTotalPrecio.Text = "Monto total: $ " + presupuesto.TotalMonto.ToString();
            section_totales_listado.Style.Add("display", "block");

            RepeaterListado.DataSource = presupuesto.ListaItems;
            RepeaterListado.DataBind();
            section_listado.Style.Add("display", "block");
            BtnExportar.Style.Add("display", "block");
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarCamposItem())
            {
                Presupuesto presupuesto = new Presupuesto();
                List<ItemPresupuesto> lista = new List<ItemPresupuesto>();
                presupuesto.ListaItems = lista;

                if (Session["listadoItems"] != null)
                {
                    presupuesto.ListaItems = (List<ItemPresupuesto>)Session["listadoItems"];
                }
                else
                {
                    Session["listadoItems"] = presupuesto.ListaItems;
                }

                bool itemAgregado = false;

                ItemPresupuesto itemNuevo = CargarCamposItem();

                foreach (ItemPresupuesto item in presupuesto.ListaItems)
                {
                    if (item.Codigo == itemNuevo.Codigo)
                    {
                        itemAgregado = true;
                    }
                }

                if (!itemAgregado)
                {
                    presupuesto.ListaItems.Add(itemNuevo);
                }
                CargarRepeaterListado();
                ResetearCampos();
            }
            else
            {
                hfError.Value = "Datos incompletos";
            }
        }

        private void EliminarItem(string codigo)
        {
            Presupuesto presupuestoActual = new Presupuesto();
            List<ItemPresupuesto> lista = new List<ItemPresupuesto>();
            presupuestoActual.ListaItems = lista;

            Presupuesto presupuestoNuevo = new Presupuesto();
            List<ItemPresupuesto> listaNueva = new List<ItemPresupuesto>();
            presupuestoNuevo.ListaItems = listaNueva;

            if (Session["listadoItems"] != null)
            {
                presupuestoActual.ListaItems = (List<ItemPresupuesto>)Session["listadoItems"];

                if (presupuestoActual.ListaItems.Count > 0)
                {
                    foreach (ItemPresupuesto item in presupuestoActual.ListaItems)
                    {
                        if (item.Codigo != codigo)
                        {
                            presupuestoNuevo.ListaItems.Add(item);
                        }
                    }
                    Session["listadoItems"] = presupuestoNuevo.ListaItems;
                    CargarRepeaterListado();
                }
            }
        }

        private void VaciarPresupuesto()
        {
            Session["listadoItems"] = null;
            RepeaterListado.DataSource = null;
            RepeaterListado.DataBind();
            section_listado.Style.Add("display", "none");
            BtnExportar.Style.Add("display", "none");
            LblTotalItems.Text = "";
            LblTotalPrecio.Text = "";
            section_totales_listado.Style.Add("display", "none");
        }

        private void ExportarPdf(Presupuesto presupuesto)
        {
            Paragraph saltoDeLinea = new Paragraph("                                                                                                                                                                                                                                                                                                                                                                                   ");

            BaseFont _letraRemito = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
            Font letraRemito = new Font(_letraRemito, 40f, Font.BOLD, new Color(0, 0, 0));
            BaseFont _titulo = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
            Font titulo = new Font(_titulo, 24f, Font.BOLD, new Color(0, 0, 0));
            BaseFont _subtitulo = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            Font subtitulo = new Font(_subtitulo, 10f, Font.BOLD, new Color(0, 0, 0));
            BaseFont _cliente = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            Font nombresCliente = new Font(_subtitulo, 12f, Font.BOLD, new Color(0, 0, 0));
            BaseFont _parrafo = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            Font parrafo = new Font(_parrafo, 10f, Font.NORMAL, new Color(0, 0, 0));
            BaseFont _tituloGrilla = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            Font tituloGrilla = new Font(_tituloGrilla, 10f, Font.NORMAL, new Color(255, 255, 255));
            BaseFont _contenidoGrilla = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            Font contenidoGrilla = new Font(_contenidoGrilla, 10f, Font.NORMAL, new Color(0, 0, 0));
            BaseFont _footer = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, true);
            Font footer = new Font(_footer, 6f, Font.BOLD, new Color(0, 0, 0));

            string NombreEmpresa = "COMPUGROSS";
            string Cliente = "Cliente:\n" + presupuesto.Cliente.Apenom;
            string Contacto = "WhatsApp: 11-5607-3553";
            string Mail = "compugross02.05.13@gmail.com";
            string FechaHoraPresupuesto = "Fecha: " + presupuesto.Fecha;
            string Footer = "* Los precios no incluyen IVA.\n\n" +
            "* Los precios serán válidos por 24 Hs.";

            string imageURL = Server.MapPath("/img/Presupuesto/letra2.jpg");
            Image imagenTipoDocumento = Image.GetInstance(imageURL);
            imagenTipoDocumento.ScaleToFit(40f, 40f);
            imagenTipoDocumento.Alignment = Element.ALIGN_CENTER;

            string imageURL2 = Server.MapPath("/img/Presupuesto/CG.png");
            Image imgLogo = Image.GetInstance(imageURL2);
            imgLogo.ScaleToFit(100f, 100f);
            imgLogo.Border = 0;
            imgLogo.Alignment = Element.ALIGN_RIGHT;

            PdfPTable tblLogoCG = new PdfPTable(new float[] { 100f })
            {
                WidthPercentage = 100,
                HorizontalAlignment = Element.ALIGN_CENTER,
            };
            tblLogoCG.AddCell(new PdfPCell(new Phrase(NombreEmpresa, titulo))
            {
                Border = 0,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            var tblCabecera = new PdfPTable(new float[] { 50f, 50f }) { WidthPercentage = 100 };

            tblCabecera.AddCell(new PdfPCell(new Phrase(Cliente, nombresCliente))
            {
                Border = 0,
                Rowspan = 3,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                HorizontalAlignment = Element.ALIGN_LEFT
            });
            tblCabecera.AddCell(new PdfPCell(new Phrase(Contacto, parrafo))
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_RIGHT
            });
            tblCabecera.AddCell(new PdfPCell(new Phrase(Mail, parrafo))
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_RIGHT
            });
            tblCabecera.AddCell(new PdfPCell(new Phrase(FechaHoraPresupuesto, parrafo))
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_RIGHT
            });

            PdfPTable tblContenido = new PdfPTable(new float[] { 7f, 15f, 58f, 10f, 10f })
            {
                WidthPercentage = 100,
                HorizontalAlignment = Element.ALIGN_CENTER
            };

            //TITULOS GRILLA
            tblContenido.AddCell(new PdfPCell(new Phrase(new Phrase("Cant.", tituloGrilla)))
            {
                BackgroundColor = Color.BLACK,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Border = 1,
                BorderColor = Color.WHITE
            });
            tblContenido.AddCell(new PdfPCell(new Phrase(new Phrase("Código", tituloGrilla)))
            {
                BackgroundColor = Color.BLACK,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Border = 1,
                BorderColor = Color.WHITE
            });
            tblContenido.AddCell(new PdfPCell(new Phrase(new Phrase("Descripción", tituloGrilla)))
            {
                BackgroundColor = Color.BLACK,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Border = 1,
                BorderColor = Color.WHITE
            });
            tblContenido.AddCell(new PdfPCell(new Phrase(new Phrase("Precio", tituloGrilla)))
            {
                BackgroundColor = Color.BLACK,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Border = 1,
                BorderColor = Color.WHITE
            });
            tblContenido.AddCell(new PdfPCell(new Phrase(new Phrase("Subtotal", tituloGrilla)))
            {
                BackgroundColor = Color.BLACK,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Border = 1,
                BorderColor = Color.WHITE
            });

            //CONTENIDO GRILLA
            foreach (ItemPresupuesto item in presupuesto.ListaItems)
            {
                tblContenido.AddCell(new PdfPCell(new Phrase(item.Cantidad.ToString(), contenidoGrilla))
                {
                    BorderWidthRight = 1,
                    BorderWidthLeft = 1,
                    BorderWidthBottom = 1,
                    BorderWidthTop = 1,
                    BorderColor = Color.BLACK,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                });
                tblContenido.AddCell(new PdfPCell(new Phrase(item.Codigo, contenidoGrilla))
                {
                    BorderWidthRight = 1,
                    BorderWidthBottom = 1,
                    BorderWidthTop = 1,
                    BorderColor = Color.BLACK,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                });
                tblContenido.AddCell(new PdfPCell(new Phrase(item.Descripcion, contenidoGrilla))
                {
                    BorderWidthRight = 1,
                    BorderWidthBottom = 1,
                    BorderWidthTop = 1,
                    BorderColor = Color.BLACK,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                });
                tblContenido.AddCell(new PdfPCell(new Phrase("$ "+item.Precio.ToString(), contenidoGrilla))
                {
                    BorderWidthRight = 1,
                    BorderWidthBottom = 1,
                    BorderWidthTop = 1,
                    BorderColor = Color.BLACK,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                });
                tblContenido.AddCell(new PdfPCell(new Phrase("$ "+item.Subtotal.ToString(), contenidoGrilla))
                {
                    BorderWidthRight = 1,
                    BorderWidthBottom = 1,
                    BorderWidthTop = 1,
                    BorderColor = Color.BLACK,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                });
            }

            PdfPTable tblTotal = new PdfPTable(new float[] { 100f })
            {
                WidthPercentage = 20,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            };
            tblTotal.AddCell(new PdfPCell(new Phrase("$ "+presupuesto.TotalMonto.ToString(), tituloGrilla))
            {
                BackgroundColor = Color.BLACK,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Border = 1,
                BorderColor = Color.BLACK
            });

            //TABLA_FOOTER
            var tblFooter = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100 };
            tblFooter.AddCell(new PdfPCell(new Phrase(Footer, footer))
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_BOTTOM
            });

            using (FileStream fs = new FileStream(Server.MapPath("/Exports/Presupuestos/Presupuesto (" + presupuesto.Fecha + ") " + presupuesto.Cliente.Apenom + ".pdf"), FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.LETTER, 40f, 40f, 40f, 40f);
                PdfWriter.GetInstance(pdfDoc, fs);
                pdfDoc.AddTitle("Presupuesto");
                pdfDoc.Open();
                pdfDoc.Add(imagenTipoDocumento);
                pdfDoc.Add(imgLogo);
                pdfDoc.Add(tblCabecera);
                pdfDoc.Add(saltoDeLinea);
                pdfDoc.Add(tblContenido);
                pdfDoc.Add(saltoDeLinea);
                pdfDoc.Add(tblTotal);
                pdfDoc.Add(saltoDeLinea);
                pdfDoc.Add(saltoDeLinea);
                pdfDoc.Add(tblFooter);
                pdfDoc.Close();
                fs.Close();
            }
        }

        private string ArmarFecha()
        {
            DateTime aux = Convert.ToDateTime(TxtFecha.Text);
            string dia = aux.Day.ToString();
            if (aux.Day < 10) { dia = "0" + aux.Day.ToString(); }
            string mes = aux.Month.ToString();
            if (aux.Month < 10) { mes = "0" + aux.Month.ToString(); }

            return dia + "." + mes + "." + aux.Year;
        }

        protected void BtnExportar_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();
            Presupuesto presupuesto = new Presupuesto();
            List<ItemPresupuesto> lista = new List<ItemPresupuesto>();
            presupuesto.Cliente = cliente;
            presupuesto.ListaItems = lista;

            if (Session["listadoItems"] != null)
            {
                presupuesto.ListaItems = (List<ItemPresupuesto>)Session["listadoItems"];

                if (presupuesto.ListaItems.Count > 0)
                {
                    if (TxtFecha.Text != "" && TxtCliente.Text != "")
                    {
                        presupuesto.Fecha = ArmarFecha();
                        presupuesto.Cliente.Apenom = TxtCliente.Text;
                        presupuesto.TotalItems = 0;
                        presupuesto.TotalMonto = 0;

                        foreach (ItemPresupuesto item in lista)
                        {
                            presupuesto.TotalItems += item.Cantidad;
                            presupuesto.TotalMonto += item.Subtotal;
                        }

                        try
                        {
                            ExportarPdf(presupuesto);
                            hfMessage.Value = "Presupuesto guardado correctamente";
                        }
                        catch
                        {
                            hfError.Value = "No se pudo exportar el presupuesto a PDF debido a un error";
                        }
                    }
                    else
                    {
                        hfError.Value = "Fecha no seleccionada o Cliente no ingresado";
                    }
                }
                else
                {
                    hfError.Value = "El Presupuesto está vacío";
                }
            }
        }

        protected void BtnVaciar_Click(object sender, EventArgs e)
        {
            VaciarPresupuesto();
            ResetearCampos();
            TxtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TxtCliente.Text = "";
        }

        private void ResetearCampos()
        {
            TxtCodigo.Text = "";
            TxtCantidad.Text = "";
            TxtPrecioUnitario.Text = "";
            TxtDescripcion.Text = "";
        }

        protected void LblCliente_Click(object sender, EventArgs e)
        {

        }
    }
}