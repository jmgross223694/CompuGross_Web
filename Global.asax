<%@ Application Codebehind="Global.asax.cs" Inherits="CompuGross_Web.Global" Language="C#" %>

<script runat="server">
    void Application_BeginRequest(object sender, EventArgs e)
    {
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type");
        HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "86400");
    }
</script>
