<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EdicionDatos.aspx.cs" Inherits="GP05_CallCenter.EdicionDatos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="text-center my-4">
        <h1>Se podrán administrar los diferentes tipos de datos desde aquí:
        </h1>
    </div>

    <div class="row justify-content-center mb-4">
        <div class="col-auto">
            <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-select form-select-lg"></asp:DropDownList>
        </div>
        <div class="col-auto">
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control form-control-lg" placeholder="Escriba el nombre..."></asp:TextBox>
        </div>
        <div class="col-auto">
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control form-control-lg" placeholder="Escriba la descripcion..." TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="col-auto">
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary btn-lg" OnClick="btnAgregar_Click" />
        </div>
    </div>
    <div class="row justify-content-center mt-1">
        <asp:Label ID="lblRegistro" runat="server" Text="Ya existe el dato ingresado..." Visible="false" ForeColor="Red" />
    </div>
</asp:Content>
