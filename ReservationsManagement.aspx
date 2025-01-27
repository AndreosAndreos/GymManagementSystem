﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ReservationsManagement.aspx.cs" Inherits="Dlutto_management.ReservationsManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Stylizacja tabeli */
        .table {
            width: 100%;
            border-collapse: collapse;
        }
        .table th, .table td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }
        .table th {
            background-color: #f2f2f2;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container-fluid">
        <div class="row">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img src="imgs/reservations.png" width="100px"/>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>Resevations</h3>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label>Reservation ID</label>
                                    <div class="form-group">
                                        <div class="input-group">
                                            <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="Reservation ID" ></asp:TextBox>
                                            <asp:Button ID="Button4" class="btn btn-primary" runat="server" Text="Go" OnClick="Button4_Click" AutoPostBack="true"/>
                                        </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label>Date of reservation</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" placeholder="Date" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label>Client ID</label>
                                <div class="form-group">
                                    <asp:DropDownList CssClass="form-control" ID="DropDownListClientID" runat="server" DataSourceID="SqlDataSourceClient" DataTextField="ID_uzytkownika" DataValueField="ID_uzytkownika" AppendDataBoundItems="true">
                                        <asp:ListItem Text="Select Client" Value="" />
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSourceClient" runat="server" ConnectionString="<%$ ConnectionStrings:DluttoConnectionString %>" SelectCommand="SELECT ID_uzytkownika FROM dbo.Uzytkownicy where Rola_uzytkownika = 'Klient'"></asp:SqlDataSource>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label>Group session ID</label>
                                <div class="form-group">
                                    <asp:DropDownList CssClass="form-control" ID="DropDownListGroupSessionID" runat="server" DataSourceID="SqlDataSourceGroupSession" DataTextField="ID_zajec" DataValueField="ID_zajec" AppendDataBoundItems="true">
                                        <asp:ListItem Text="Select Session" Value="" />
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSourceGroupSession" runat="server" ConnectionString="<%$ ConnectionStrings:DluttoConnectionString %>" SelectCommand="SELECT ID_zajec FROM Zajecia_grupowe"></asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Button ID="Button1" class="btn btn-success btn-block btn-lg" runat="server" Text="Add" OnClick="Button1_Click" AutoPostBack="true"/>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Button ID="Button2" class="btn btn-warning btn-block btn-lg" runat="server" Text="Update" OnClick="Button2_Click" AutoPostBack="true"/>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Button ID="Button3" class="btn btn-danger btn-block btn-lg" runat="server" Text="Dalete" OnClick="Button3_Click" AutoPostBack="true"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <a href="Homepage.aspx"><< Back to home page</a><br><br>
                 </div>
            <div class="col-md-6">
                 <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img src="imgs/reservations.png" width="100px"/>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>List of reservations</h3>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DluttoConnectionString %>" SelectCommand="SELECT * FROM [Rezerwacje]"></asp:SqlDataSource>
                                <hr>
                            </div>
                        </div>
                       <div class="row">
                            <div class="col">
                                <asp:GridView Class="table table-striped table-bordered" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID_rezerwacji" DataSourceID="SqlDataSource1" ShowHeaderWhenEmpty="True">
                                    <Columns>
                                        <asp:BoundField DataField="ID_rezerwacji" HeaderText="Reservation ID" InsertVisible="False" ReadOnly="True" SortExpression="ID_rezerwacji" />
                                        <asp:BoundField DataField="Data_rezerwacji" HeaderText="Reservation date" SortExpression="Data_rezerwacji" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False" />
                                        <asp:BoundField DataField="ID_klienta" HeaderText="Client ID" SortExpression="ID_klienta" />
                                        <asp:BoundField DataField="ID_zajec_grupowych" HeaderText="Group session ID" SortExpression="ID_zajec_grupowych" />
                                    </Columns>
                                </asp:GridView>
                        </div>
                        
                    </div>
                </div>
            </div>
             </div>
            </div>
</asp:Content>
