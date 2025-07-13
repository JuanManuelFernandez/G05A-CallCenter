<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="editardatos.aspx.cs" Inherits="CallCenter.EditarDatos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="text-center my-4">
        <h1>administra los tipos de datos aquí:</h1>
    </div>

    <div class="row justify-content-center mb-4">
        <div class="col-auto">
            <asp:DropDownList ID="ddlElegir" runat="server" CssClass="form-select form-select-lg" AutoPostBack="true" OnSelectedIndexChanged="ddlElegir_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-auto">
            <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-select form-select-lg" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="col-auto">
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control form-control-lg" placeholder="Escriba el nombre..."></asp:TextBox>
        </div>
        <div class="col-auto">
            <asp:DropDownList ID="ddlDato" runat="server" CssClass="form-select form-select-lg" Visible ="false" AutoPostBack="true" OnSelectedIndexChanged="ddlDato_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-auto">
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control form-control-lg" placeholder="Escriba la descripcion..." TextMode="MultiLine" Style="resize: none"></asp:TextBox>
        </div>
        <div class="col-auto">
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary btn-lg" OnClick="btnAgregar_Click" />
        </div>
    </div>
    <div class="row text-center mt-1">
        <asp:Label ID="lblRegistro" runat="server" Text="Ya existe el dato ingresado..." Visible="false" ForeColor="Red" />
    </div>
</asp:Content>
