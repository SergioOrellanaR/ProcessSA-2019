﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CrearTarea.aspx.cs" Inherits="TASKWebApp.View.CrearTarea" %>

<asp:Content ID="Content1" ContentPlaceHolderID="tittle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .disabledStyle {
            border: 1px solid gray;
            color: Gray;
        }

        .enabledStyle {
            border: 1px solid Blue;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">
    <section class="section container">
        <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
        <asp:Panel ID="Panel1" runat="server">
            <div style="text-align: center">
                <h1>Crear Tarea</h1>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="message" class="text-center">
                        <strong><asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red" Font-Size="Medium"></asp:Label></strong>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>



        <asp:Panel ID="Panel4" runat="server">
            <div class="row col-md-offset-4">
                <div class="col-sm-6">
                    <strong>
                        <asp:Label ID="Label16" runat="server" Text="Responsable:"></asp:Label></strong>
                    <asp:DropDownList CssClass="form-control" ID="ddlResponsable" runat="server" AppendDataBoundItems="true"></asp:DropDownList><br />
                </div>
            </div>


            <asp:UpdatePanel ID="udpTipoCargaTarea" runat="server">
                <ContentTemplate>
                    <div class="col-sm-6">
                        <asp:RadioButtonList ID="rbtlTipoCargaTarea" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbtlTipoCargaTarea_SelectedIndexChanged">
                            <asp:ListItem Value="TareaPropia" Text="Tarea propia" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="TareaPredeterminada" Text="Tarea predeterminada"></asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:DropDownList CssClass="form-control" ID="ddlTareasPredeterminadas" runat="server" OnSelectedIndexChanged="ddlTareasPredeterminadas_SelectedIndexChanged" AutoPostBack="True" Enabled="false" Visible="false" AppendDataBoundItems="true">
                        </asp:DropDownList>
                        <br />



                        <asp:Label ID="Label6" runat="server" Text="Nombre Tarea"></asp:Label>
                        <div class="form-group">
                            <asp:TextBox CssClass="form-control" ID="txtNombreTarea" runat="server" type="text" Font-Size="Smaller" placeholder="Nombre Tarea"></asp:TextBox>
                        </div>

                        <asp:Label ID="Label7" runat="server" Text="Descripcion"></asp:Label>
                        <div class="form-group">
                            <asp:TextBox CssClass="form-control" ID="txtDescripcion" TextMode="multiline" Columns="50" Rows="5" runat="server" MaxLength="500" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel ID="udpTipoTarea" runat="server">
                <ContentTemplate>
                    <div class="col-sm-6">
                        <asp:RadioButtonList ID="rbtlTipoTarea" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbtlTipoTarea_SelectedIndexChanged">
                            <asp:ListItem Value="TareaUnica" Text="Tarea unica" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="TareaRepetitiva" Text="Tarea repetitiva"></asp:ListItem>
                        </asp:RadioButtonList>

                        <div id="divTareaUnica" runat="server">
                            <div class="col-md-5 form-group">
                                <asp:Label ID="Label14" runat="server" Text="Inicio:"></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtFechaInicio" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
                            </div>
                            <div class="col-md-5 form-group">
                                <asp:Label ID="Label15" runat="server" Text="Fin:"></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtFechaFin" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
                            </div>
                        </div>

                        <div id="divTareaRepetitiva" runat="server" visible="false">
                            <h4>Opciones de repetición</h4>
                            <div class="col-md-5 form-group">
                                <asp:Label ID="Label9" runat="server" Text="Hora inicio"></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtHoraInicio" runat="server" TextMode="Time" Text="00:01"></asp:TextBox>
                            </div>
                            <div class="col-md-5 form-group">
                                <asp:Label ID="Label12" runat="server" Text="Hora fin"></asp:Label>
                                <asp:TextBox CssClass="form-control" ID="txtHoraFin" runat="server" TextMode="Time" Text="23:59"></asp:TextBox>
                            </div>

                            <div class="col-md-3 form-group">
                                <asp:Label ID="Label3" runat="server" Text="Repetir por: "></asp:Label>
                            </div>

                            <div class="col-md-5 form-group">
                                <asp:RadioButtonList ID="rbtlTipoRepeticion" runat="server" RepeatDirection="Horizontal" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="rbtlTipoRepeticion_SelectedIndexChanged">
                                    <asp:ListItem Text="Dia semana" Selected="True" Value="diaSemana"></asp:ListItem>
                                    <asp:ListItem Text="Dia mes" Value="diaMes"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                            <div id="divDiaSemana" runat="server">
                                <div class="col-md-5 form-group">
                                    <asp:Label ID="Label1" runat="server" Text="Repetir en días: "></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:CheckBoxList ID="cbxDiaSemana" runat="server" RepeatDirection="Horizontal" AppendDataBoundItems="true">
                                        <asp:ListItem Value="1" Text="Lunes"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Martes"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Miercoles"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Jueves"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="Viernes"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="Sábado"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="Domingo"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </div>
                            </div>

                            <div id="divDiaMes" runat="server">
                                <div class="col-md-5 form-group">
                                    <asp:Label ID="Label5" runat="server" Text="Repetir los días: "></asp:Label>
                                </div>
                                <asp:DropDownList CssClass="form-control" ID="ddlDiaDelMes" runat="server">
                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                    <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                    <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                    <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                    <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                    <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                    <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                    <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                    <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                    <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                    <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                    <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                    <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                    <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                    <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                    <asp:ListItem Text="Último día de cada mes" Value="32"></asp:ListItem>
                                </asp:DropDownList>
                                <br />
                            </div>

                            <div id="divNumeroSemana" runat="server">
                                <div class="col-md-5 form-group">
                                    <asp:Label ID="Label2" runat="server" Text="Durante las semanas: "></asp:Label>
                                </div>
                                <asp:CheckBoxList ID="cbxNumeroSemana" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Text="Semana 1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Semana 2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Semana 3"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Semana 4"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="Semana 5"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="Semana 6"></asp:ListItem>
                                </asp:CheckBoxList>
                            </div>

                            <div id="divMes" runat="server">
                                <div class="col-md-5 form-group">
                                    <asp:Label ID="Label4" runat="server" Text="De: "></asp:Label>
                                </div>
                                <asp:DropDownList CssClass="form-control" ID="ddlMeses" runat="server">
                                    <asp:ListItem Text="Todos los meses" Value="13"></asp:ListItem>
                                    <asp:ListItem Text="Enero" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Febrero" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Marzo" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Abril" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Mayo" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Junio" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="Julio" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="Agosto" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="Septiembre" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="Octubre" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="Noviembre" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="Diciembre" Value="12"></asp:ListItem>
                                </asp:DropDownList>

                            </div>
                    </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <%--
        <div style="width: 50%; ">

            <br />
            <br />
            <asp:UpdatePanel ID="udpDependencia" runat="server">
                <ContentTemplate>
                    <div id="divDependencia" runat="server">
                        <asp:Label ID="Label17" runat="server" Text="¿Esta tarea tiene dependencia?" RepeatLayout="Flow"></asp:Label>
                        <asp:RadioButtonList ID="rbtDependencia" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtDependencia_SelectedIndexChanged" >
                            <asp:ListItem Value="No" Text="No" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="Si" Text="Si"></asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:DropDownList ID="ddlTareaDependiente" runat="server">
                        </asp:DropDownList>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
            --%>
        </asp:Panel>
        <asp:UpdatePanel ID="updButtons" runat="server">
            <ContentTemplate>
                    <div class="col-md-3 form-group" style="width:80%;">
                        <div class="col-md-4 form-group">
                            <asp:Button class="btn btn-info btn-lg btn-block" ID="btnCrearTarea" runat="server" Text="Crear Tarea" OnClick="btnCrearTarea_Click" />
                        </div>
                        <div class="col-md-3 form-group" style="width:30%;">
                            <asp:Button class="btn btn-info btn-lg btn-block" ID="btnCrearFlujoTarea" runat="server" Text="Crear flujo de tareas" OnClick="btnCrearFlujoTarea_Click" />
                        </div>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
