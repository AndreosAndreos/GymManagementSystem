<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="GroupSessionsManagement.aspx.cs" Inherits="Dlutto_management.GroupSessionsManagement" %>
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
                                    <img src="imgs/group.jpeg" height="100" width="150px"/>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>Group session</h3>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <label>Group session ID</label>
                                    <div class="form-group">
                                        <div class="input-group">
                                            <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="ID" ></asp:TextBox>
                                            <asp:Button ID="Button4" class="btn btn-primary" runat="server" Text="Go" OnClick="Button4_Click" AutoPostBack="true"/>
                                        </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label>Name</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" placeholder="Name" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-7">
                                <label>Description</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Description" ></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label>Start date</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Start date" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>Number of seats</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" placeholder="Number of seats"></asp:TextBox>
                                </div>
                            </div>
                                <div class="col-md-4">
                                <label>Trainer ID</label><asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DluttoConnectionString %>" SelectCommand="Select ID_trenera  FROM [Dlutto].[dbo].[Trenerzy]"></asp:SqlDataSource>
                                    &nbsp;<div class="form-group">
                                    <asp:DropDownList CssClass="form-control" ID="DropDownList1" runat="server" DataSourceID="SqlDataSource2" DataTextField="ID_trenera" DataValueField="ID_trenera">
                                        <asp:ListItem Text="Select Trainer" Value="" />
                                    </asp:DropDownList>
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
                                    <img src="imgs/group.jpeg" width="150px"/>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>List of group sessions</h3>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DluttoConnectionString %>" SelectCommand="SELECT * FROM [Zajecia_grupowe]"></asp:SqlDataSource>
                                <hr>
                            </div>
                        </div>
                       <div class="row">
                            <div class="col">
                                <asp:GridView Class="table table-striped table-bordered" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID_zajec" DataSourceID="SqlDataSource1" ShowHeaderWhenEmpty="True">
                                    <Columns>
                                        <asp:BoundField DataField="ID_zajec" HeaderText="Session ID" InsertVisible="False" ReadOnly="True" SortExpression="ID_zajec" />
                                        <asp:BoundField DataField="Nazwa" HeaderText="Name" SortExpression="Nazwa" />
                                        <asp:BoundField DataField="Opis" HeaderText="Description" SortExpression="Opis" />
                                        <asp:BoundField DataField="Data_rozpoczecia" HeaderText="Start date" SortExpression="Data_rozpoczecia" />
                                        <asp:BoundField DataField="Liczba_miejsc" HeaderText="Number of seats" SortExpression="Liczba_miejsc" />
                                        <asp:BoundField DataField="ID_trenera_prowadzacego" HeaderText="Leading trainer ID" SortExpression="ID_trenera_prowadzacego" />
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
