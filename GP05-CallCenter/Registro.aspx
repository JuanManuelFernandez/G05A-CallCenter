<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="CallCenter.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h1 class="text-center">¡Bienvenido!</h1>

        <p class="text-center">¿Tu primera vez acá? Registrate y tomaremos tu pedido lo más pronto posible</p>

        <div class="container">
            <div class="row justify-content-center">
                <div class="col-md-6 text-center">
                    <div class="mb-3">
                        <asp:TextBox ID="dni" runat="server" Placeholder="DNI" TextMode="Number" required="required" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <asp:TextBox ID="nombre" runat="server" Placeholder="Nombre" required="required" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <asp:TextBox ID="apellido" runat="server" Placeholder="Apellido" required="required" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <asp:TextBox ID="telefono" runat="server" Placeholder="Teléfono" TextMode="Number" required="required" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <asp:TextBox ID="email" runat="server" Placeholder="Email" TextMode="Email" required="required" CssClass="form-control" />
                    </div>
                    <div class="mt-3">
                        <asp:TextBox ID="clave" runat="server" Placeholder="Contraseña" TextMode="Password" required="required" CssClass="form-control" />
                    </div>
                    <div class="row justify-content-center mt-1">
                        <asp:Label ID="lblRegistro" runat="server" Text="Registro realizado con éxito" Visible="false" ForeColor="Green" />
                    </div>
                    <div class="row justify-content-center mt-2">
                        <section class="col-md-6" aria-labelledby="hostingTitle">
                            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-primary btn-lg w-100" OnClick="btnRegistrar_Click" />
                        </section>
                    </div>
                </div>
            </div>
        </div>

    </main>
</asp:Content>
