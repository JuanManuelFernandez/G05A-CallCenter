<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="diarios.aspx.cs" Inherits="CallCenter.Diarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <h1>Reportes</h1>
        <h2>Selecciona los empleados que recibiran los reportes mensuales:</h2>
        <asp:GridView ID="dgvEmpleados" runat="server" class="table table-bordered" AutoGenerateColumns="false" DataKeyNames="IdUsuario">
            <Columns>
                <asp:BoundField HeaderText="Email" DataField="Email" />
                <asp:BoundField HeaderText="Eliminado" DataField="Eliminado" />

                <asp:TemplateField HeaderText="Enviar">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkEnviar" runat="server"
                            Checked='<%# Convert.ToBoolean(Eval("Reporte")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>

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
