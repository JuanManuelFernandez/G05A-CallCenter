<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Formularios.aspx.cs" Inherits="CallCenter.Formularios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h1>Incidencias Actuales:</h1>

        <div class="row mb-4">
            <div class="col">
                <div class="d-flex align-items-center gap-2 flex-wrap">
                    <asp:Label runat="server" CssClass="form-label fw-bold me-2">Criterio:</asp:Label>

                    <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-select form-select-lg"
                        OnSelectedIndexChanged="DdlTipo_SelectedIndexChanged" AutoPostBack="true" />

                    <asp:DropDownList ID="ddlPrioridad" runat="server" CssClass="form-select form-select-lg"
                        Visible="false" OnSelectedIndexChanged="DdlPrioridad_SelectedIndexChanged" AutoPostBack="true" />

                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control form-control-lg"
                        Visible="false" placeholder="Escriba aquí..." />

                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary btn-lg"
                        Enabled="false" OnClick="BtnBuscar_Click" />
                </div>
            </div>
        </div>

        <asp:GridView ID="dgvIncidencias" OnSelectedIndexChanged="DgvIncidencias_SelectedIndexChanged" DataKeyNames="IdIncidencia" runat="server" class="table table-bordered" AutoGenerateColumns="false" OnRowCommand="DgvIncidencias_RowCommand" OnRowDataBound="DgvIncidencias_RowDataBound">
            <Columns>
                <asp:BoundField HeaderText="Prioridad" DataField="prioridad.Nombre" />
                <asp:BoundField HeaderText="Estado Actual" DataField="EstadoActual" />
                <asp:BoundField HeaderText="Fecha/Hora Creacion" DataField="FechaYHoraCreacion" />
                <asp:TemplateField HeaderText="Historial">
                    <ItemTemplate>
                        <div style="text-align: center;">
                            <asp:Button ID="btnHistorial" runat="server" CommandName="Abrir" CommandArgument='<%# Container.DataItemIndex %>' Text="Abrir" CssClass="btn btn-primary btn-lg" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Asignar Empleado" Visible="false">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlEmpleados" runat="server" CssClass="form-control form-control-rounded" OnSelectedIndexChanged="DdlEmpleados_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </main>

</asp:Content>
