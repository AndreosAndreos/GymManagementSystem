<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="TrainersManagement.aspx.cs" Inherits="Dlutto_management.TrainersManagement" %>

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
                                    <img src="imgs/trainer.jpeg" width="100px"/>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>Trainer</h3>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label>Trainer ID</label>
                                    <div class="form-group">
                                        <div class="input-group">
                                            <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="Trainer ID" ></asp:TextBox>
                                            <asp:Button ID="Button4" class="btn btn-primary" runat="server" Text="Go" OnClick="Button4_Click" AutoPostBack="true"/>
                                        </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-9">
                                <label>Specialization</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Specialization" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label>User ID</label>
                                <div class="form-group">
                                    <asp:DropDownList CssClass="form-control" ID="DropDownListUserID" runat="server" DataSourceID="SqlDataSourceUsers" DataTextField="ID_uzytkownika" DataValueField="ID_uzytkownika" AppendDataBoundItems="true">
                                        <asp:ListItem Text="Select User" Value="" />
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSourceUsers" runat="server" ConnectionString="<%$ ConnectionStrings:DluttoConnectionString %>" SelectCommand="SELECT ID_uzytkownika FROM Uzytkownicy Where Rola_uzytkownika = 'Trener'"></asp:SqlDataSource>
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
                                    <asp:Button ID="Button3" class="btn btn-danger btn-block btn-lg" runat="server" Text="Delete" OnClick="Button3_Click" AutoPostBack="true"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <a href="Homepage.aspx">&lt;&lt; Back to home page</a><br><br>
            </div>
            <div class="col-md-6">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img src="imgs/trainerlist.png" width="100px"/>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>List of trainers</h3>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr>
                            </div>
                        </div>
                        <div class="row">
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DluttoConnectionString %>" SelectCommand="SELECT * FROM [Trenerzy]"></asp:SqlDataSource>
                            <div class="col">
                                <asp:GridView Class="table table-striped table-bordered" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID_trenera" DataSourceID="SqlDataSource1" ShowHeaderWhenEmpty="True">
                                    <Columns>
                                        <asp:BoundField DataField="ID_trenera" HeaderText="Trainer ID" InsertVisible="False" ReadOnly="True" SortExpression="ID_trenera" />
                                        <asp:BoundField DataField="Specjalizacja" HeaderText="Specialization" SortExpression="Specjalizacja" />
                                        <asp:BoundField DataField="ID_uzytkownika" HeaderText="User ID" SortExpression="ID_uzytkownika" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
