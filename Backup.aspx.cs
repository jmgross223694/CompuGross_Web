using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Negocio;
using Dominio;
using Microsoft.Graph;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using Workbook = Aspose.Cells.Workbook;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using System.Globalization;

namespace CompuGross_Web
{
    public partial class Backup : System.Web.UI.Page
    {
        private int registrosImportados = 0;
        private int iRow = 1, iCol = 1, cantFilas = 0, cantColumnas = 0;
        private string[] columnasCliente = {
            "Apellido y Nombre / Razón Social",
            "CUIT_DNI",
            "Dirección",
            "Localidad",
            "Teléfono",
            "Mail"
        };
        private string[] columnasServicio = {
            "Cliente",
            "Recepción",
            "TipoEquipo",
            "RAM",
            "PlacaMadre",
            "MarcaModelo",
            "Microprocesador",
            "Almacenamiento",
            "UnidadÓptica",
            "Alimentación",
            "Adicionales",
            "NumSerie",
            "TipoServicio",
            "Descripción",
            "CostoRepuestos",
            "CostoTerceros",
            "Honorarios",
            "CostoTotal",
            "Devolución"
        };
        private string[] columnasLocalidad = {
            "Descripción_Localidad"
        };
        private string[] columnasPrecio = {
            "Código",
            "Descripción",
            "Aclaraciones",
            "PrecioDolares"
        };
        private string[] columnasUsuario = {
            "TipoUsuario",
            "Apellido",
            "Nombre",
            "Username",
            "Mail",
            "Clave"
        };
        private string[] columnasTipoEquipo = {
            "Descripción_TipoEquipo"
        };
        private string[] columnasTipoServicio = {
            "Descripción_TipoServicio"
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario_Logueado"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
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
                    if (!IsPostBack)
                    {
                        CargarDdlTablasDB();
                    }
                }
            }
        }

        //Metodos para realizar Backup
        private List<string> CrearListaTablasDB()
        {
            List<string> listaTablas = new List<string>();

            listaTablas.Add("Clientes");
            listaTablas.Add("ListaPrecios");
            listaTablas.Add("Localidades");
            listaTablas.Add("OrdenesTrabajo");
            listaTablas.Add("TiposEquipo");
            listaTablas.Add("TiposServicio");
            listaTablas.Add("Usuarios");

            return listaTablas;
        }

        private StreamWriter CrearArchivoCsv(string nombreTabla, DataTable dt)
        {
            string Path = @"" + Server.MapPath("/Exports/Backup") + "\\" + nombreTabla + ".csv";
            StreamWriter sw = new StreamWriter(Path, false, Encoding.UTF8);
            long cantidadColumnas = dt.Columns.Count;

            for (int ncolumna = 0; ncolumna < cantidadColumnas; ncolumna++)
            {
                sw.Write(dt.Columns[ncolumna]);
                if (ncolumna < cantidadColumnas - 1)
                {
                    sw.Write(";");
                }
            }
            sw.Write(sw.NewLine);

            foreach (DataRow renglon in dt.Rows)
            {
                for (int ncolumna = 0; ncolumna < cantidadColumnas; ncolumna++)
                {
                    if (!Convert.IsDBNull(renglon[ncolumna]))
                    {
                        sw.Write(renglon[ncolumna]);
                    }
                    if (ncolumna < cantidadColumnas)
                    {
                        sw.Write(";");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();

            return sw;
        }

        private bool CrearArchivosBackup(List<string> listaTablas, ConexionDB conDB)
        {
            bool bandera = false;

            foreach (var nombreTabla in listaTablas)
            {
                try
                {
                    DataTable dt = conDB.CrearDataTable("select * from " + nombreTabla);
                    StreamWriter sw = CrearArchivoCsv(nombreTabla, dt);
                    bandera = true;
                }
                catch
                {
                    bandera = false;
                }
                finally
                {
                    conDB.CerrarConexion();
                }
            }

            return bandera;
        }

        protected void BtnHacerBackup_Click(object sender, EventArgs e)
        {
            if (CrearArchivosBackup(CrearListaTablasDB(), new ConexionDB()))
            {
                hfMessage.Value = "Backup realizado correctamente";
            }
            else
            {
                hfError.Value = "No se realizó el Backup debido a un error";
            }
        }

        //Metodos para restaurar Backup
        private void CargarDdlTablasDB()
        {
            DdlTablasDB.Items.Clear();
            DdlTablasDB.Items.Add("Seleccione...");
            DdlTablasDB.DataSource = CrearListaDdlTablasDB();
            DdlTablasDB.DataMember = "datos";
            DdlTablasDB.DataTextField = "Descripcion";
            DdlTablasDB.DataValueField = "ID";
            DdlTablasDB.DataBind();
        }

        private List<TablaDB> CrearListaDdlTablasDB()
        {
            List<TablaDB> listaTablas = new List<TablaDB>();

            int i = 0;
            foreach (string tabla in CrearListaTablasDB())
            {
                TablaDB tablaAux = new TablaDB();
                tablaAux.ID = i + 1;
                tablaAux.Descripcion = tabla;

                if (tabla == "OrdenesTrabajo")
                {
                    tablaAux.Descripcion = "Servicios";
                }
                if (tabla == "ListaPrecios")
                {
                    tablaAux.Descripcion = "Lista de Precios";
                }
                if (tabla == "TiposEquipo")
                {
                    tablaAux.Descripcion = "Tipos de Equipo";
                }
                if (tabla == "TiposServicio")
                {
                    tablaAux.Descripcion = "Tipos de Servicio";
                }

                i++;
                listaTablas.Add(tablaAux);
            }

            return listaTablas;
        }

        private string ValidarTablaSeleccionadaConDB(string tablaSeleccionada)
        {
            if (tablaSeleccionada == "Servicios")
            {
                return "OrdenesTrabajo";
            }
            else if (tablaSeleccionada == "Tipos de Servicio")
            {
                return "TiposServicio";
            }
            else if (tablaSeleccionada == "Lista de Precios")
            {
                return "ListaPrecios";
            }
            else if (tablaSeleccionada == "Tipos de Equipo")
            {
                return "TiposEquipo";
            }

            return tablaSeleccionada;
        }

        private string ValidarArchivo(string path, string extension, int tam)
        {
            if (FuRestaurarBackup.HasFile)
            {
                if (extension == ".xlsx" || extension == ".xls")
                {
                    if (tam <= 1048576)
                    {
                        FuRestaurarBackup.SaveAs(Server.MapPath(path));
                        path = Server.MapPath(path);

                        if (extension == ".xls")
                        {
                            var workbook = new Workbook(path);
                            path = path.Replace(".xls", ".xlsx");
                            workbook.Save(path);
                        }

                        return "1";
                    }
                    else
                    {
                        return "-1";
                    }
                }
                else
                {
                    return "-2";
                }
            }
            else
            {
                return "-3";
            }
        }

        private bool ValidarColumnasArchivo(SLDocument archivo, string tablaSeleccionada)
        {
            bool resultado = false;

            if (tablaSeleccionada == "OrdenesTrabajo")
            {
                for (this.iCol = 1; this.iCol <= this.cantColumnas; this.iCol++)
                {
                    string valor = archivo.GetCellValueAsString(1, this.iCol); //Recorremos el archivo celda por celda y obtenemos su respectivo valor

                    if (valor == columnasServicio[this.iCol - 1])
                    {
                        resultado = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else if (tablaSeleccionada == "Clientes")
            {
                for (this.iCol = 1; this.iCol <= this.cantColumnas; this.iCol++)
                {
                    string valor = archivo.GetCellValueAsString(1, this.iCol); //Recorremos el archivo celda por celda y obtenemos su respectivo valor

                    if (valor == columnasCliente[this.iCol - 1])
                    {
                        resultado = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else if (tablaSeleccionada == "ListaPrecios")
            {
                for (this.iCol = 1; this.iCol <= this.cantColumnas; this.iCol++)
                {
                    string valor = archivo.GetCellValueAsString(1, this.iCol); //Recorremos el archivo celda por celda y obtenemos su respectivo valor

                    if (valor == columnasPrecio[this.iCol - 1])
                    {
                        resultado = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else if (tablaSeleccionada == "TiposEquipo")
            {
                for (this.iCol = 1; this.iCol <= this.cantColumnas; this.iCol++)
                {
                    string valor = archivo.GetCellValueAsString(1, this.iCol); //Recorremos el archivo celda por celda y obtenemos su respectivo valor

                    if (valor == columnasTipoEquipo[this.iCol - 1])
                    {
                        resultado = true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            else if (tablaSeleccionada == "TiposServicio")
            {
                for (this.iCol = 1; this.iCol <= this.cantColumnas; this.iCol++)
                {
                    string valor = archivo.GetCellValueAsString(1, this.iCol); //Recorremos el archivo celda por celda y obtenemos su respectivo valor

                    if (valor == columnasTipoServicio[this.iCol - 1])
                    {
                        resultado = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else if (tablaSeleccionada == "Usuarios")
            {
                for (this.iCol = 1; this.iCol <= this.cantColumnas; this.iCol++)
                {
                    string valor = archivo.GetCellValueAsString(1, this.iCol); //Recorremos el archivo celda por celda y obtenemos su respectivo valor

                    if (valor == columnasUsuario[this.iCol - 1])
                    {
                        resultado = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else if (tablaSeleccionada == "Localidades")
            {
                for (this.iCol = 1; this.iCol <= this.cantColumnas; this.iCol++)
                {
                    string valor = archivo.GetCellValueAsString(1, this.iCol); //Recorremos el archivo celda por celda y obtenemos su respectivo valor

                    if (valor == columnasLocalidad[this.iCol - 1])
                    {
                        resultado = true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return resultado;
        }

        private string AbrirArchivo(string path, string tablaSeleccionada)
        {
            try
            {
                SLDocument archivo = new SLDocument(@"" + path);
                archivo.SelectWorksheet(archivo.GetSheetNames()[0].ToString());
                ContarFilasColumnasArchivo(archivo);

                if (ValidarColumnasArchivo(archivo, tablaSeleccionada))
                {
                    if (RecorrerArchivo(archivo, tablaSeleccionada))
                    {
                        return "1";
                    }
                    return "-3";
                }
                else
                {
                    return "-1";
                }
            }
            catch
            {
                return "-2";
            }
        }

        private bool RecorrerArchivo(SLDocument archivo, string tablaSeleccionada)
        {
            if (tablaSeleccionada == "Clientes")
            {
                return ImportarClientes(archivo);
            }
            else if (tablaSeleccionada == "OrdenesTrabajo")
            {
                return ImportarServicios(archivo);
            }
            else if (tablaSeleccionada == "ListaPrecios")
            {
                return ImportarPrecios(archivo);
            }
            else if (tablaSeleccionada == "TiposEquipo")
            {
                return ImportarTiposEquipo(archivo);
            }
            else if (tablaSeleccionada == "TiposServicio")
            {
                return ImportarTiposServicio(archivo);
            }
            else if (tablaSeleccionada == "Usuarios")
            {
                return ImportarUsuarios(archivo);
            }
            else if (tablaSeleccionada == "Localidades")
            {
                return ImportarLocalidades(archivo);
            }
            return false;
        }

        private bool ImportarClientes(SLDocument archivo)
        {
            List<Cliente> lista = new List<Cliente>();

            for (this.iRow = 2; this.iRow <= this.cantFilas; this.iRow++)
            {
                lista.Add(CargarRegistroCliente(archivo, this.iRow));
            }

            if (lista.Count > 0)
            {
                ClienteDB clienteDB = new ClienteDB();

                foreach (Cliente cliente in lista)
                {
                    if (!clienteDB.VerificarExistenciaCliente_RestaurarBackup(cliente))
                    {
                        if (clienteDB.AgregarCliente(cliente))
                        {
                            this.registrosImportados++;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                this.registrosImportados = 0;
                return false;
            }
        }

        private bool ImportarServicios(SLDocument archivo)
        {
            List<Servicio> lista = new List<Servicio>();

            for (this.iRow = 2; this.iRow <= this.cantFilas; this.iRow++)
            {
                lista.Add(CargarRegistroServicio(archivo, this.iRow));
            }

            if (lista.Count > 0)
            {
                ServicioDB servicioDB = new ServicioDB();

                foreach (Servicio servicio in lista)
                {
                    if (!servicioDB.VerificarExistenciaServicio_RestaurarBackup(servicio))
                    {
                        if (servicioDB.Agregar(servicio))
                        {
                            this.registrosImportados++;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                this.registrosImportados = 0;
                return false;
            }
        }

        private bool ImportarPrecios(SLDocument archivo)
        {
            List<Precio> lista = new List<Precio>();

            for (this.iRow = 2; this.iRow <= this.cantFilas; this.iRow++)
            {
                lista.Add(CargarRegistroPrecio(archivo, this.iRow));
            }

            if (lista.Count > 0)
            {
                PrecioDB precioDB = new PrecioDB();

                foreach (Precio precio in lista)
                {
                    if (!precioDB.VerificarExistenciaPrecio_RestaurarBackup(precio))
                    {
                        if (precioDB.Agregar(precio))
                        {
                            this.registrosImportados++;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                this.registrosImportados = 0;
                return false;
            }
        }

        private bool ImportarTiposEquipo(SLDocument archivo)
        {
            List<TipoEquipo> lista = new List<TipoEquipo>();

            for (this.iRow = 2; this.iRow <= this.cantFilas; this.iRow++)
            {
                lista.Add(CargarRegistroTipoEquipo(archivo, this.iRow));
            }

            if (lista.Count > 0)
            {
                TipoEquipoDB tipoEquipoDB = new TipoEquipoDB();

                foreach (TipoEquipo tipoEquipo in lista)
                {
                    if (!tipoEquipoDB.VerificarExistenciaTipoEquipo_RestaurarBackup(tipoEquipo))
                    {
                        if (tipoEquipoDB.Agregar(tipoEquipo))
                        {
                            this.registrosImportados++;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                this.registrosImportados = 0;
                return false;
            }
        }

        private bool ImportarTiposServicio(SLDocument archivo)
        {
            List<TipoServicio> lista = new List<TipoServicio>();

            for (this.iRow = 2; this.iRow <= this.cantFilas; this.iRow++)
            {
                lista.Add(CargarRegistroTiposServicio(archivo, this.iRow));
            }

            if (lista.Count > 0)
            {
                TipoServicioDB tipoServicioDB = new TipoServicioDB();

                foreach (TipoServicio tipoServicio in lista)
                {
                    if (!tipoServicioDB.VerificarExistenciaTipoServicio_RestaurarBackup(tipoServicio))
                    {
                        if (tipoServicioDB.Agregar(tipoServicio))
                        {
                            this.registrosImportados++;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                this.registrosImportados = 0;
                return false;
            }
        }

        private bool ImportarUsuarios(SLDocument archivo)
        {
            List<Usuario> lista = new List<Usuario>();

            for (this.iRow = 2; this.iRow <= this.cantFilas; this.iRow++)
            {
                lista.Add(CargarRegistroUsuario(archivo, this.iRow));
            }

            if (lista.Count > 0)
            {
                UsuarioDB usuarioDB = new UsuarioDB();

                foreach (Usuario usuario in lista)
                {
                    if (!usuarioDB.VerificarExistenciaUsuario_RestaurarBackup(usuario))
                    {
                        if (usuarioDB.Agregar(usuario))
                        {
                            this.registrosImportados++;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                this.registrosImportados = 0;
                return false;
            }
        }

        private bool ImportarLocalidades(SLDocument archivo)
        {
            List<Localidad> lista = new List<Localidad>();

            for (this.iRow = 2; this.iRow <= this.cantFilas; this.iRow++)
            {
                lista.Add(CargarRegistroLocalidad(archivo, this.iRow));
            }

            if (lista.Count > 0)
            {
                LocalidadDB localidadDB = new LocalidadDB();

                foreach (Localidad localidad in lista)
                {
                    if (!localidadDB.VerificarExistenciaLocalidad_RestaurarBackup(localidad))
                    {
                        if (localidadDB.Agregar(localidad))
                        {
                            this.registrosImportados++;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                this.registrosImportados = 0;
                return false;
            }
        }

        private Cliente CargarRegistroCliente(SLDocument archivo, int iRow)
        {
            Localidad loc = new Localidad();
            Cliente c = new Cliente();

            c.Apenom = archivo.GetCellValueAsString(iRow, 1);
            c.CuitDni = archivo.GetCellValueAsString(iRow, 2);
            c.Direccion = archivo.GetCellValueAsString(iRow, 3);
            loc.Descripcion = archivo.GetCellValueAsString(iRow, 4);
            c.Localidad = loc;
            c.Telefono = archivo.GetCellValueAsString(iRow, 5);
            c.Direccion = archivo.GetCellValueAsString(iRow, 6);

            return c;
        }

        private Servicio CargarRegistroServicio(SLDocument archivo, int iRow)
        {
            Cliente cliente = new Cliente();
            Equipo equipo = new Equipo();
            UnidadOptica unidadOptica = new UnidadOptica();
            TipoEquipo tipoEquipo = new TipoEquipo();
            TipoServicio tipoServicio = new TipoServicio();
            Servicio servicio = new Servicio();

            cliente.Apenom = archivo.GetCellValueAsString(iRow, 1);
            servicio.Cliente = cliente;
            DateTime aux = Convert.ToDateTime(archivo.GetCellValueAsString(iRow, 2));
            servicio.FechaRecepcion = aux.Day + "/" + aux.Month + "/" + aux.Year;
            tipoEquipo.Descripcion = archivo.GetCellValueAsString(iRow, 3);
            equipo.Tipo = tipoEquipo;
            equipo.RAM = archivo.GetCellValueAsString(iRow, 4);
            equipo.PlacaMadre = archivo.GetCellValueAsString(iRow, 5);
            equipo.MarcaModelo = archivo.GetCellValueAsString(iRow, 6);
            equipo.Microprocesador = archivo.GetCellValueAsString(iRow, 7);
            equipo.Almacenamiento = archivo.GetCellValueAsString(iRow, 8);
            unidadOptica.Descripcion = archivo.GetCellValueAsString(iRow, 9);
            equipo.UnidadOptica = unidadOptica;
            equipo.Alimentacion = archivo.GetCellValueAsString(iRow, 10);
            equipo.Adicionales = archivo.GetCellValueAsString(iRow, 11);
            equipo.NumSerie = archivo.GetCellValueAsString(iRow, 12);
            servicio.Equipo = equipo;
            tipoServicio.Descripcion = archivo.GetCellValueAsString(iRow, 13);
            servicio.TipoServicio = tipoServicio;
            servicio.Descripcion = archivo.GetCellValueAsString(iRow, 14);
            servicio.CostoRepuestos = archivo.GetCellValueAsString(iRow, 15);
            servicio.CostoTerceros = archivo.GetCellValueAsString(iRow, 16);
            servicio.Honorarios = archivo.GetCellValueAsString(iRow, 17);
            servicio.CostoTotal = archivo.GetCellValueAsString(iRow, 18);
            aux = Convert.ToDateTime(archivo.GetCellValueAsString(iRow, 19));
            servicio.FechaDevolucion = aux.Day + "/" + aux.Month + "/" + aux.Year;

            return servicio;
        }

        private Precio CargarRegistroPrecio(SLDocument archivo, int iRow)
        {
            Precio p = new Precio();

            p.Codigo = archivo.GetCellValueAsString(iRow, 1);
            p.Descripcion = archivo.GetCellValueAsString(iRow, 2);
            p.Aclaraciones = archivo.GetCellValueAsString(iRow, 3);
            p.Dolares = Convert.ToDecimal(archivo.GetCellValueAsString(iRow, 4).Replace(".", ","));

            return p;
        }

        private TipoEquipo CargarRegistroTipoEquipo(SLDocument archivo, int iRow)
        {
            TipoEquipo tipoEquipo = new TipoEquipo();

            tipoEquipo.Descripcion = archivo.GetCellValueAsString(iRow, 1);

            return tipoEquipo;
        }

        private TipoServicio CargarRegistroTiposServicio(SLDocument archivo, int iRow)
        {
            TipoServicio tipoServicio = new TipoServicio();

            tipoServicio.Descripcion = archivo.GetCellValueAsString(iRow, 1);

            return tipoServicio;
        }

        private Usuario CargarRegistroUsuario(SLDocument archivo, int iRow)
        {
            Usuario u = new Usuario();

            u.TipoUsuario.Descripcion = archivo.GetCellValueAsString(iRow, 1);
            u.Apellido = archivo.GetCellValueAsString(iRow, 2);
            u.Nombre = archivo.GetCellValueAsString(iRow, 3);
            u.Username = archivo.GetCellValueAsString(iRow, 4);
            u.Mail = archivo.GetCellValueAsString(iRow, 5);
            u.Clave = archivo.GetCellValueAsString(iRow, 6);

            return u;
        }

        private Localidad CargarRegistroLocalidad(SLDocument archivo, int iRow)
        {
            Localidad loc = new Localidad();

            loc.Descripcion = archivo.GetCellValueAsString(iRow, 1);

            return loc;
        }

        private void ContarFilasColumnasArchivo(SLDocument archivo)
        {
            while (!string.IsNullOrEmpty(archivo.GetCellValueAsString(this.iRow, 1)))
            {
                this.iRow++;
                this.cantFilas++;
            }

            while (!string.IsNullOrEmpty(archivo.GetCellValueAsString(1, this.iCol)))
            {
                this.iCol++;
                this.cantColumnas++;
            }
        }

        protected void BtnRestaurarBackup_Click(object sender, EventArgs e)
        {
            string tablaSeleccionada = DdlTablasDB.SelectedItem.ToString();

            if (tablaSeleccionada != "Seleccione...")
            {
                tablaSeleccionada = ValidarTablaSeleccionadaConDB(tablaSeleccionada);
                string extension = Path.GetExtension(FuRestaurarBackup.FileName).ToLower();
                int tam = FuRestaurarBackup.PostedFile.ContentLength;
                string path = "~/Imports/Libro1" + extension;
                string resultadoValidarArchivo = ValidarArchivo(path, extension, tam);
                path = Server.MapPath(path);

                if (resultadoValidarArchivo == "1")
                {
                    string resultadoAbrirArchivo = AbrirArchivo(path, tablaSeleccionada);
                    if (resultadoAbrirArchivo == "1" && this.registrosImportados > 0)
                    {
                        if (tablaSeleccionada == "OrdenesTrabajo")
                        {
                            tablaSeleccionada = "Servicios";
                        }
                        if (tablaSeleccionada == "ListaPrecios")
                        {
                            tablaSeleccionada = "Precios";
                        }
                        if (tablaSeleccionada == "TiposEquipo")
                        {
                            tablaSeleccionada = "Tipos de Equipo";
                        }
                        if (tablaSeleccionada == "TiposServicio")
                        {
                            tablaSeleccionada = "Tipos de Servicio";
                        }

                        try
                        {
                            System.IO.File.Delete(path); //BorrarArchivoGenerado
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        hfMessage.Value = "Se han insertado " + this.registrosImportados + " " + tablaSeleccionada + " nuevos, con éxito";
                    }
                    else
                    {
                        if (resultadoAbrirArchivo != "1")
                        {
                            if (resultadoAbrirArchivo == "-1")
                            {
                                hfError.Value = "Las Columnas del Archivo no coinciden con la Tabla seleccionada";
                            }
                            if (resultadoAbrirArchivo == "-2")
                            {
                                hfError.Value = "Se produjo un error al intentar abrir el Archivo";
                            }
                            if (resultadoAbrirArchivo == "-3")
                            {
                                hfError.Value = "Se produjo un error al recorrer el Archivo";
                            }
                        }
                        else if (registrosImportados == 0)
                        {
                            hfError.Value = "El Archivo seleccionado no contiene registros";
                        }
                        else
                        {
                            hfError.Value = "Se produjo un error y no se importó el archivo";
                        }
                    }
                }
                else
                {
                    if (resultadoValidarArchivo == "-1")
                    {
                        hfError.Value = "El archivo seleccionado supera el límite soportado";
                    }
                    if (resultadoValidarArchivo == "-2")
                    {
                        hfError.Value = "Sólo se permiten archivo con extensión '.xls' y '.xlsx'";
                    }
                    if (resultadoValidarArchivo == "-3")
                    {
                        hfError.Value = "No se seleccionó ningún archivo";
                    }
                }
            }
            else
            {
                hfError.Value = "Tabla no seleccionada";
            }
        }
    }
}