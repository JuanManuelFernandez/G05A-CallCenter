<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edicion.aspx.cs" Inherits="GP05_CallCenter.Edicion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="container mt-4">
        <div class="row g-3">
            <div class="col-md-3">
                <asp:Label ID="lblDni" runat="server" Text="DNI" CssClass="form-label" />
                <asp:TextBox ID="txtDNI" runat="server" TextMode="Number" CssClass="form-control form-control-rounded"/>
            </div>
            <div class="col-md-3">
                <asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="form-label" />
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control form-control-rounded"/>
            </div>
            <div class="col-md-3">
                <asp:Label ID="lblApellido" runat="server" Text="Apellido" CssClass="form-label" />
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control form-control-rounded"/>
            </div>
            <div class="col-md-3">
                <asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="form-label" />
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control form-control-rounded"/>
            </div>
            <div class="col-md-3">
                <asp:Label ID="lblTelefono" runat="server" Text="Teléfono" CssClass="form-label" />
                <asp:TextBox ID="txtTelefono" runat="server" TextMode="Number" CssClass="form-control form-control-rounded"/>
            </div>
            <div class="col-md-3">
                <asp:Label ID="lblCategoria" runat="server" Text="Categoría" CssClass="form-label" />
                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control form-control-rounded" AutoPostBack="false"></asp:DropDownList>
            </div>
            <div class="col-md-6 d-flex justify-content-start mt-4">
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-primary btn-lg me-3" OnClick="btnModificar_Click" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger btn-lg" onclick="btnEliminar_Click"/>
            </div>
        </div>
    </main>
</asp:Content>
