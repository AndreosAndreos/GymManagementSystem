<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EquipmentManagement.aspx.cs" Inherits="Dlutto_management.Toolsmanagement" %>
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
                                    <img src="imgs/gymtool.png" width="100px"/>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>Equipment</h3>
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
                                <label>Equipment ID</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="Equipment ID" ></asp:TextBox>
                                        <asp:Button ID="Button4" class="btn btn-primary" runat="server" Text="Go" OnClick="Button4_Click" AutoPostBack="true" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <label>Equipment name</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" placeholder="Equipment name" ></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-9">
                                <label>Description</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Description" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label>Purchase date</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" placeholder="Purchase date" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Button ID="Button1" class="btn btn-success btn-block btn-lg" runat="server" Text="Add" OnClick="Button1_Click" AutoPostBack="true" />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Button ID="Button2" class="btn btn-warning btn-block btn-lg" runat="server" Text="Update" OnClick="Button2_Click" AutoPostBack="true" />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Button ID="Button3" class="btn btn-danger btn-block btn-lg" runat="server" Text="Delete" OnClick="Button3_Click" AutoPostBack="true" />
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
                                    <img src="imgs/equipement.jpeg" width="100px"/>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>List of our equipment</h3>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DluttoConnectionString %>" SelectCommand="SELECT * FROM [Sprzet]"></asp:SqlDataSource>
                                <hr>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:GridView Class="table table-striped table-bordered" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID_sprzetu" DataSourceID="SqlDataSource1" ShowHeaderWhenEmpty="True">
                                    <Columns>
                                        <asp:BoundField DataField="ID_sprzetu" HeaderText="Equipement ID" InsertVisible="False" ReadOnly="True" SortExpression="ID_sprzetu" />
                                        <asp:BoundField DataField="Nazwa" HeaderText="Name" SortExpression="Nazwa" />
                                        <asp:BoundField DataField="Opis" HeaderText="Descryption" SortExpression="Opis" />
                                        <asp:BoundField DataField="Data_zakupu" HeaderText="Date of purchase" SortExpression="Data_zakupu" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False" />
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