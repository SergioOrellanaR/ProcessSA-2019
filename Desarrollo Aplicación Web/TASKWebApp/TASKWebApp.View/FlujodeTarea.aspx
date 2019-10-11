﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FlujodeTarea.aspx.cs" Inherits="TASKWebApp.View.FlujodeTarea" %>

<asp:Content ID="Content1" ContentPlaceHolderID="tittle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">
    <section class="section">
        <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="upTablaTareas" runat="server">
            <ContentTemplate>
                <div class="container text-center">
                    <asp:Label ID="Label7" runat="server"><h2>Crear Flujo de Tareas - Tareas Unicas</h2></asp:Label>
                    <br />
                </div>

                <table class="table table-hover">
                <asp:Repeater ID="repTabla" runat="server">
                    <HeaderTemplate>
                            <thead>
                                <tr>
                                    <th scope="col">Nombre Tarea</th>
                                    <th scope="col">Descripcion</th>
                                    <th scope="col">Fecha Inicio</th>
                                    <th scope="col">Fecha Fin</th>
                                    <th scope="col">Dependendia de</th>
                                    <th scope="col"><asp:Label ID="hdlblAgregar" runat="server" Text="Agregar"></asp:Label></th>
                                    <th scope="col"><asp:Label ID="hdlblEliminar" runat="server" Text="Eliminar"></asp:Label></th>
                                    <th scope="col"><asp:Label ID="hdlblEditar" runat="server" Text="Editar"></asp:Label></th>
                                </tr>
                            </thead>
                        <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        
                            <tr>
                                <td><asp:Label ID="lblSubSeparator" runat="server" Text=""></asp:Label><asp:Label ID="lblSubNombre" runat="server" Text=""></asp:Label></td>
                                <td><asp:Label ID="lblSubDescripcion" runat="server" Text=""></asp:Label></td>
                                <td><asp:Label ID="lblSubFechaInicio" runat="server" Text=""></asp:Label></td>
                                <td><asp:Label ID="lblSubFechaFin" runat="server" Text=""></asp:Label></td>
                                <td><asp:Label ID="lblSubDependencia" runat="server" Text=""></asp:Label></td>
                                <td><asp:Button ID="btnSubAdd" runat="server" Text="+" OnClick="btnSubAdd_Click" UseSubmitBehavior="false" /></td>
                                <td><asp:Button ID="btnSubDelete" runat="server" Text="-" OnClick="btnSubDelete_Click" UseSubmitBehavior="false"/></td>
                                <td><asp:Button ID="btnSubEdit" runat="server" Text="Editar" OnClick="btnSubEdit_Click" UseSubmitBehavior="false"/></td>
                            </tr>
                         
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                    </FooterTemplate>
                </asp:Repeater>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="upTarea" runat="server">
            <ContentTemplate>
<asp:Repeater ID="repSubTask" runat="server">
    <HeaderTemplate>
        Contenido de repeater
    </HeaderTemplate>
    <ItemTemplate>
        <div id="divTarea" runat="server">

                    <div id="InformacionTarea" runat="server">
                        <div class="row">
                            <asp:Label ID="Label1" runat="server" CssClass="col-sm-2 " Text="Nombre Tarea"></asp:Label>
                            <div class="col-md-4 form-group">
                                <asp:TextBox class="form-control" ID="txtNombre" type="text" runat="server" placeholder="Nombre tarea"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <asp:Label ID="Label3" CssClass="col-sm-2 " runat="server" Text="Descripcion"></asp:Label>
                            <div class="col-md-4 form-group">
                                <asp:TextBox ID="txtDescription" TextMode="multiline" Columns="50" Rows="5" runat="server" MaxLength="500" Style="margin: 0px; width: 539px; height: 190px;" />
                            </div>
                        </div>
                    </div>
                    <div id="divTareaUnica" runat="server" class="container">
                        <div class="row">
                            <asp:Label ID="Label2" CssClass="col-sm-2 " runat="server" Text="Inicio:"></asp:Label>
                            <div class="col-md-2 form-group">
                                <asp:TextBox ID="txtFechaInicio" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
                            </div>
                            <asp:Label ID="Label4" CssClass="col-sm-2 " runat="server" Text="Fin:"></asp:Label>
                            <div class="col-md-2 form-group">
                                <asp:TextBox ID="txtFechaFin" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div id="divTareaRepetitiva" runat="server" class="container">
                        <div class="col-md-12 mb-5 element-animate">
                            <div class="row">
                                <asp:Label ID="Label12" CssClass="col-sm-2 " runat="server" Text="Hora Inicio"></asp:Label>
                                <div class="col-md-2 form-group">
                                    <asp:TextBox class="form-control" ID="txtHoraInicio" TextMode="Time" runat="server" placeholder="Hora Inicio" Text="00:01"></asp:TextBox>
                                </div>
                                <asp:Label ID="Label13" CssClass="col-sm-2 " runat="server" Text="Hora Fin"></asp:Label>
                                <div class="col-md-2 form-group">
                                    <asp:TextBox class="form-control" ID="txtHoraFin" TextMode="Time" runat="server" placeholder="Hora Fin" Text="23:59"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div id="divDiaSemana" runat="server">
                                    <div class="col-sm-2">
                                        <asp:Label ID="Label9" runat="server" Text="Repetir en días: "></asp:Label>
                                    </div>
                                    <div class="col-sm-10">
                                        <asp:CheckBoxList ID="cbxDiaSemana" runat="server" RepeatDirection="Horizontal" AppendDataBoundItems="true">
                                            <asp:ListItem Value="1" Text="Lunes">&nbsp Lunes &nbsp&nbsp</asp:ListItem>
                                            <asp:ListItem Value="2" Text="Martes">&nbsp Martes &nbsp&nbsp</asp:ListItem>
                                            <asp:ListItem Value="3" Text="Miercoles">&nbsp Miercoles &nbsp&nbsp</asp:ListItem>
                                            <asp:ListItem Value="4" Text="Jueves">&nbsp Jueves &nbsp&nbsp</asp:ListItem>
                                            <asp:ListItem Value="5" Text="Viernes">&nbsp Viernes &nbsp&nbsp</asp:ListItem>
                                            <asp:ListItem Value="6" Text="Sábado">&nbsp Sábado &nbsp&nbsp</asp:ListItem>
                                            <asp:ListItem Value="7" Text="Domingo">&nbsp Domingo &nbsp&nbsp</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div id="divDiaMes" runat="server">
                                    <div class="col-sm-2">
                                        <asp:Label ID="Label21" runat="server" Text="Repetir los días: "></asp:Label>
                                    </div>
                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="ddlDiaDelMes" runat="server" CssClass="btn btn-info dropdown-toggle">
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
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div id="divNumeroSemana" runat="server" style="width: 100%;">
                                    <div class="col-sm-2">
                                        <asp:Label ID="Label22" runat="server" Text="Durante las semanas: "></asp:Label>
                                    </div>
                                    <div class="col-sm-10">
                                        <asp:CheckBoxList ID="cbxNumeroSemana" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Text="Semana 1">&nbsp Semana 1 &nbsp&nbsp</asp:ListItem>
                                            <asp:ListItem Value="2" Text="Semana 2">&nbsp Semana 2 &nbsp&nbsp</asp:ListItem>
                                            <asp:ListItem Value="3" Text="Semana 3">&nbsp Semana 3 &nbsp&nbsp</asp:ListItem>
                                            <asp:ListItem Value="4" Text="Semana 4">&nbsp Semana 4 &nbsp&nbsp</asp:ListItem>
                                            <asp:ListItem Value="5" Text="Semana 5">&nbsp Semana 5 &nbsp&nbsp</asp:ListItem>
                                            <asp:ListItem Value="6" Text="Semana 6">&nbsp Semana 6 &nbsp&nbsp</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div id="divMes" runat="server" style="width: 100%;">
                                    <div class="col-sm-2">
                                        <asp:Label ID="Label23" runat="server" Text="De: "></asp:Label>
                                    </div>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="ddlMeses" runat="server" CssClass="btn btn-info dropdown-toggle">
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
                            <br />
                        </div>
                    </div>
                    <div id="divDependencia" runat="server">
                        <div class="row">
                            <div class="col-sm-3">
                                <asp:Label ID="Label10" runat="server" Text="¿Esta tarea tiene dependencia?"></asp:Label>
                            </div>
                            <div class="col-sm-2">
                                <div class="form-check">
                                    <asp:RadioButtonList ID="rbtDependencia" runat="server" AutoPostBack="true">
                                        <asp:ListItem Value="No" Text="No" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="Si" Text="Si"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3">
                                <asp:Label ID="Label11" runat="server" Text="Seleccionar tarea dependiente"></asp:Label>
                            </div>
                            <div class="col-sm-1">
                                <div class="form-check">
                                    <asp:DropDownList CssClass="btn btn-info dropdown-toggle" ID="ddlTareaDependiente" runat="server">
                                        <asp:ListItem Value="">¿Cual es el nombre de tu primera mascota?</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-5 form-group">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="btn btn-info btn-lg btn-block" />
                        </div>
                    </div>
                </div>
    </ItemTemplate>
    <SeparatorTemplate>
        <br />
        <br />
        <br />
    </SeparatorTemplate>
</asp:Repeater>
               
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row">
            <div class="col-md-3 form-group">
                <asp:Button ID="btnVolver" runat="server" Text="Volver" class="btn btn-info btn-lg btn-block" />
            </div>
            <div class="col-md-3 form-group">
                <asp:Button ID="btnCrearFlujo" runat="server" Text="Crear Flujo de Tareas" class="btn btn-info btn-lg btn-block" />
            </div>
        </div>
    </section>

    <asp:Label ID="lblMeme" runat="server" Text=""></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
