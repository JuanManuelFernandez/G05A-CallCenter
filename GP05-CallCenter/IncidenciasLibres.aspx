<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IncidenciasLibres.aspx.cs" Inherits="GP05_CallCenter.IndicenciasLibres" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <h1>Incidencias libres</h1>
        </div>

        <asp:GridView ID="dgvIncidenciasLibres" runat="server" class="table table-bordered" AutoGenerateColumns="False" OnRowCommand="dgvIncidenciasLibres_RowCommand" DataKeyNames="IDIncidencia">
            <Columns>
                <asp:BoundField DataField="IDIncidencia" HeaderText="ID" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                <asp:BoundField DataField="IDEmpleado" HeaderText="Empleado asignado" />
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <div style="text-align: center;">
                            <asp:Button ID="btnTomar" runat="server" CommandName="Tomar" CommandArgument='<%# Container.DataItemIndex %>' Text="Tomar" CssClass="btn btn-success btn-lg"/>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </main>
</asp:Content>
