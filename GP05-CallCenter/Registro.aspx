<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="CallCenter.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="text-center">
            <h1 class="py-3 mb-4 bg-primary text-white display-3" style="display: inline-block;"><%= TituloH1 %></h1>
        </div>

        <p class="text-center"><%= ParrafoP %></p>

        <div class="row justify-content-center">
            <div class="col-md-2">
                <div>
                    <div class="mb-3">
                        <asp:TextBox ID="dni" runat="server" Placeholder="DNI" TextMode="Number" required="required" CssClass="form-control" min=1/>
                    </div>
                    <div class="mb-3">
                        <asp:TextBox ID="nombre" runat="server" Placeholder="Nombre" required="required" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <asp:TextBox ID="apellido" runat="server" Placeholder="Apellido" required="required" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <asp:TextBox ID="telefono" runat="server" Placeholder="Teléfono" TextMode="Number" required="required" CssClass="form-control" min=1/>
                    </div>
                    <div class="mb-3">
                        <asp:TextBox ID="email" runat="server" Placeholder="Email" TextMode="Email" required="required" CssClass="form-control" />
                    </div>
                    <div class="mb-2">
                        <asp:TextBox ID="clave" runat="server" Placeholder="Contraseña" TextMode="Password" required="required" CssClass="form-control" />
                    </div>
                </div>
            </div>
        </div>

        <div class="row justify-content-center mt-2">
            <section class="text-center col-md-6" aria-labelledby="hostingTitle">
                <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-primary btn-lg w-100" OnClick="btnRegistrar_Click" />
            </section>
        </div>
        <div class="text-center mt-1">
            <asp:Label ID="lblRegistro" runat="server" Text="Registro realizado con éxito" Visible="false" ForeColor="Green" />
        </div>

    </main>
</asp:Content>
