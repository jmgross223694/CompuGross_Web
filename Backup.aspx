<%@ Page Title="Backup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Backup.aspx.cs" Inherits="CompuGross_Web.Backup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="Styles/Style_Backup.css?v=2.0" rel="stylesheet" />
    <script defer src="JavaScript/Script_Backup.js" ></script>

    <section id="section_titulo" runat="server" class="stl-section-titulo">
        <div class="stl-div-titulo">
            <h1>Backup</h1>
        </div>
    </section>

    <asp:Panel DefaultButton="BtnHacerBackup" runat="server">
        <section id="section_botones" runat="server" class="stl-section-botones">
            <div class="stl-div-contenedor">
                <div class="stl-div-hacer-backup">
                    <h3>
                        <asp:Label ID="LblTituloHacerBackup" Text="Exportar" runat="server" />
                    </h3>
                    <asp:Button ID="BtnHacerBackup" Text="Guardar Backup" CssClass="btn btn-dark stl-btn-hacer-backup" runat="server" OnClick="BtnHacerBackup_Click" />
                </div>
                <div class="stl-div-restaurar-backup">
                    <div class="stl-div-label-restaurar-backup">
                        <h3>
                            <asp:Label ID="LblRestaurarBackup" Text="Importar" runat="server" />
                        </h3>
                    </div>
                    <div class="stl-div-btn-restaurar-backup">
                        <asp:Button ID="BtnRestaurarBackup" Text="Importar Archivo" runat="server" CssClass="btn btn-dark stl-btn-restaurar-backup" onclick="BtnRestaurarBackup_Click" />
                        <asp:DropDownList ID="DdlTablasDB" AppendDataBoundItems="true" CssClass="btn btn-dark stl-ddl-tablas-db" runat="server">
                            <asp:ListItem Value="0" Text="Seleccione..." />
                        </asp:DropDownList>
                    </div>
                    <div class="file-select file-upload">
                        <asp:FileUpload ID="FuRestaurarBackup" runat="server" CssClass="stl-fu-restaurar-backup" />
                    </div>
                    
                </div>
            </div>
        </section>
    </asp:Panel>
    

    <asp:HiddenField ID="hfMessage" runat="server" />
    <asp:HiddenField ID="hfError" runat="server" />

</asp:Content>
