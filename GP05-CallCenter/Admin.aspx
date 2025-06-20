<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="GP05_CallCenter.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h1 class="text-center">Vista de Administrador de los Usuarios en el Sistema</h1>
        <asp:GridView ID="dgvUsuarios" runat="server" class="table table-bordered" AutoGenerateColumns="false" OnSelectedIndexChanged="dgvUsuarios_SelectedIndexChanged" DataKeyNames="IdUsuario">
            <Columns>
                <asp:BoundField HeaderText="Email" DataField="Email" />
                <asp:BoundField HeaderText="Tipo del Usuario" DataField="TipoUsuario" />
                <asp:BoundField HeaderText="Eliminado" DataField="Eliminado" />
                <asp:CommandField ShowSelectButton="true" SelectText="Editar" />
            </Columns>
        </asp:GridView>
    </main>
</asp:Content>
