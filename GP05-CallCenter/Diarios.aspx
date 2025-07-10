<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Diarios.aspx.cs" Inherits="GP05_CallCenter.Diarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <h1>Reportes</h1>
        <h2>Selecciona los empleados que recibiran reportes mensuales sobre sus casos:</h2>
        <asp:GridView ID="dgvEmpleados" runat="server" class="table table-bordered" AutoGenerateColumns="false" OnSelectedIndexChanged="DgvEmpleados_SelectedIndexChanged" DataKeyNames="IdUsuario">
            <Columns>
                <asp:BoundField HeaderText="Email" DataField="Email" />
                <asp:BoundField HeaderText="Eliminado" DataField="Eliminado" />
                <asp:CommandField ShowSelectButton="true" SelectText="Editar" />
            </Columns>
        </asp:GridView>
    </main>

    <section>
        <div class="text-center mt-3">
            <asp:LinkButton ID="btnActualizar" runat="server" CssClass="btn btn-primary btn-lg mx-3" OnClick="BtnActualizar_Click" Visible="true" Style="display: inline-flex; align-items: center;">
            <img src="Assets/edit.png" style="width: 30px; height: 30px; margin-right: 5px;" />
            Actualizar
            </asp:LinkButton>
        </div>
    </section>

</asp:Content>
