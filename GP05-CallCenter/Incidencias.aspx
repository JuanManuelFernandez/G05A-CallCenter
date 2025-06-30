<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Incidencias.aspx.cs" Inherits="CallCenter.Incidencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">
        <div class="row justify-content-center mb-4">
            <div class="col-md-4 text-center">
                <asp:Label ID="lblFyH" runat="server" CssClass="fw-bold">Fecha y hora: 
                <asp:Label ID="lblFechaYHora" runat="server" Text=""></asp:Label>
                </asp:Label>
            </div>
        </div>
        <div class="row justify-content-center">
            <div class="col-md-3">
                <asp:Label ID="lblNombre" runat="server" Text="Nombre Cliente" CssClass="form-label fw-bold"></asp:Label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control w-100" Enabled="false"></asp:TextBox>

            </div>
            <div class="col-md-3">
                <asp:Label ID="lblMail" runat="server" Text="Email Cliente" CssClass="form-label fw-bold"></asp:Label>
                <asp:TextBox ID="txtMail" runat="server" CssClass="form-control w-100" Enabled="false"></asp:TextBox>
            </div>
        </div>

        <div class="row justify-content-center">
            <div class="col-md-3">
                <asp:Label ID="lblDni" runat="server" Text="DNI Cliente" CssClass="form-label fw-bold"></asp:Label>
                <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control w-100" Enabled="false"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <asp:Label ID="lblTelefono" runat="server" Text="Telefono Cliente" CssClass="form-label fw-bold"></asp:Label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control w-100" Enabled="false"></asp:TextBox>
            </div>

            <div class="text-center m-2">
                <asp:Label ID="lblTipo" runat="server" Text="Tipo de Incidente" CssClass="form-label fw-bold text-center"></asp:Label>
                <br />
                <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-select w-50 mx-auto"></asp:DropDownList>
            </div>

            <asp:Label ID="lblResumenProblema" runat="server" Text="Resumen del problema" CssClass="form-label fw-bold text-center"> </asp:Label>
            <asp:TextBox ID="txtResumenProblema" runat="server" Placeholder="No anda internet, el router no enciende..." CssClass="form-control w-50 mx-auto mb-2"></asp:TextBox>


            <asp:Label ID="lblDescripcion" runat="server" Text="Descripcion" CssClass="form-label fw-bold text-center"> </asp:Label>
            <br />
            <asp:TextBox ID="txtDescripcion" runat="server" Placeholder="Descripcion detallada del inconveniente o la consulta a realizar..." TextMode="MultiLine" CssClass="form-control w-50 mx-auto mb-2" Style="resize: none; height: 175px"></asp:TextBox>
        </div>

        <div class="row justify-content-center">
            <div class="col-md-3">
                <asp:Label ID="lblEstadoActual" runat="server" Text="Estado Actual" CssClass="form-label fw-bold"></asp:Label>
                <br />
                <asp:TextBox ID="txtEstadoActual" runat="server" CssClass="form-control w-100"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <asp:Label ID="lblPrioridad" runat="server" Text="Prioridad" CssClass="form-label fw-bold"></asp:Label>
                <br />
                <asp:DropDownList ID="ddlPrioridad" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>
            <asp:Label ID="lblResolucion" runat="server" Text="Resolucion" CssClass="form-label fw-bold text-center mt-2"></asp:Label>
            <asp:TextBox ID="txtResolucion" runat="server" TextMode="MultiLine" Style="width: 500px; height: 175px; resize: none;" CssClass="form-control w-50 mx-auto" OnTextChanged="TxtResolucion_TextChanged" AutoPostBack="true"></asp:TextBox>
        </div>
    </div>
    <div class="row justify-content-center mt-3">
        <section class="col-md-3 text-center" aria-labelledby="hostingTitle">
            <asp:Button ID="btnCargar" runat="server" Text="Cargar" CssClass="btn btn-success btn-lg mx-3" OnClick="BtnCargar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary btn-lg mx-3" OnClick="BtnCancelar_Click" />
        </section>
    </div>
</asp:Content>
