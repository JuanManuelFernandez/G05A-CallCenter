﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="incidencia.aspx.cs" Inherits="CallCenter.Incidencia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">
        <div class="row justify-content-center mb-4">
            <div class="col-md-4 text-center">
                <asp:Label ID="lblFyH" runat="server" CssClass="fw-bold">Fecha y hora: 
                <asp:Label ID="lblFechaYHora" runat="server" Text=""></asp:Label>
                </asp:Label>
            </div>
        </div>

        <div class="row justify-content-center">
            <div class="col-md-3">
                <asp:Label ID="lblDni" runat="server" Text="DNI Cliente" CssClass="form-label fw-bold"></asp:Label>
                <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control w-100" Enabled="false" required="required" OnTextChanged="TxtDNI_TextChanged" AutoPostBack="true" TextMode="Number" min="1"></asp:TextBox>
            </div>

            <div class="col-md-3">
                <asp:Label ID="lblMail" runat="server" Text="Email Cliente" CssClass="form-label fw-bold"></asp:Label>
                <asp:TextBox ID="txtMail" runat="server" CssClass="form-control w-100" Enabled="false" required="required" OnTextChanged="TxtMail_TextChanged" AutoPostBack="true" TextMode="Email" MaxLength="100"></asp:TextBox>
            </div>
        </div>

        <div class="row justify-content-center">
            <div class="col-md-3">
                <asp:Label ID="lblNombre" runat="server" Text="Nombre Cliente" CssClass="form-label fw-bold"></asp:Label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control w-100" Enabled="false" required="required" MaxLength="50"></asp:TextBox>
            </div>

            <div class="col-md-3">
                <asp:Label ID="lblApellido" runat="server" Text="Apellido Cliente" CssClass="form-label fw-bold"></asp:Label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control w-100" required="required" MaxLength="50"></asp:TextBox>
            </div>
        </div>

        <div class="row justify-content-center">
            <div class="col-md-3">
                <asp:Label ID="lblTelefono" runat="server" Text="Telefono Cliente" CssClass="form-label fw-bold"></asp:Label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control w-100" Enabled="false" required="required" OnTextChanged="TxtTelefono_TextChanged" AutoPostBack="true" TextMode="Number" min="1" MaxLength="50"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <asp:Label ID="lblCategoria" runat="server" Text="Categoria Cliente" CssClass="form-label fw-bold"></asp:Label>
                <br />
                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>

            <div class="text-center m-2">
                <asp:Label ID="lblTipo" runat="server" Text="Tipo de Incidente" CssClass="form-label fw-bold text-center"></asp:Label>
                <br />
                <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-select w-50 mx-auto"></asp:DropDownList>
            </div>
            <asp:Label ID="lblDescripcion" runat="server" Text="Descripcion" CssClass="form-label fw-bold text-center"> </asp:Label>
            <br />
            <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" CssClass="form-control w-50 mx-auto mb-2" Style="resize: none; height: 135px" MaxLength="500"></asp:TextBox>

            <div class="row justify-content-center">
                <div class="col-md-6">
                    <asp:Label ID="lblPlantilla" runat="server" Text="Seleccionar plantilla:" CssClass="form-label fw-bold d-block mb-2" />
                    <div class="d-flex align-items-center gap-2">
                        <asp:DropDownList ID="ddlPlantillas" runat="server" CssClass="form-select w-75"></asp:DropDownList>
                        <asp:Button ID="btnAplicarPlantilla" runat="server" Text="Aplicar" CssClass="btn btn-primary" OnClick="BtnAplicarPlantilla_Click" />
                    </div>
                </div>
            </div>

        </div>

        <div class="row justify-content-center">
            <div class="col-md-3">
                <asp:Label ID="lblEstadoActual" runat="server" Text="Estado Actual" CssClass="form-label fw-bold"></asp:Label>
                <br />
                <asp:TextBox ID="txtEstadoActual" runat="server" CssClass="form-control w-100" MaxLength="250"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <asp:Label ID="lblPrioridad" runat="server" Text="Prioridad" CssClass="form-label fw-bold"></asp:Label>
                <br />
                <asp:DropDownList ID="ddlPrioridad" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>
            <asp:Label ID="lblResolucion" runat="server" Text="Resolucion" CssClass="form-label fw-bold text-center mt-2"></asp:Label>
            <asp:TextBox ID="txtResolucion" runat="server" TextMode="MultiLine" Style="width: 500px; height: 135px; resize: none;" CssClass="form-control w-50 mx-auto" MaxLength="500"></asp:TextBox>
        </div>
    </div>
    <div class="row justify-content-center mt-3">
        <section class="col-md-3 text-center" aria-labelledby="hostingTitle">
            <div class="d-flex justify-content-center">
                <asp:LinkButton ID="btnCargar" runat="server" CssClass="btn btn-success btn-lg mx-3" OnClick="BtnCargar_Click" Style="display: inline-flex; align-items: center;">
                    <img src="Assets/check.png" style="width: 30px; height: 30px; margin-right: 5px;" />Cargar </asp:LinkButton>

                <asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-danger btn-lg mx-3" OnClick="BtnCancelar_Click" Style="display: inline-flex; align-items: center;">
                    <img src="Assets/cancel.png" style="width: 30px; height: 30px; margin-right: 5px;" />Cancelar </asp:LinkButton>
            </div>
        </section>
        <section>
            <div class="text-center mt-3">
                <asp:LinkButton ID="btnActualizar" runat="server" CssClass="btn btn-primary btn-lg mx-3" OnClick="BtnActualizar_Click" Visible="true" Style="display: inline-flex; align-items: center;">
                    <img src="Assets/edit.png" style="width: 30px; height: 30px; margin-right: 5px;" />Actualizar caso </asp:LinkButton>
            </div>
        </section>
        <section>
            <div class="text-center mt-3">
                <asp:LinkButton ID="btnActualizarCliente" runat="server" CssClass="btn btn-primary btn-lg mx-3" OnClick="BtnActualizarCliente_Click" Visible="true" Style="display: inline-flex; align-items: center;">
                    <img src="Assets/editUser.png" style="width: 30px; height: 30px; margin-right: 5px;" />Actualizar Cliente </asp:LinkButton>
            </div>
        </section>
    </div>

    <div class="text-center mt-1">
        <asp:Label ID="lblError" runat="server" Text="" Visible="false" ForeColor="Red" />
    </div>

</asp:Content>
