<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CoachEvaluations.aspx.cs" Inherits="Dlutto_management.CoachEvaluations" %>
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
                                    <img src="imgs/eval1.png" width="100px"/>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>Coach evaluation</h3>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Evaluation ID</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="Evaluation ID" ></asp:TextBox>
                                        <asp:Button ID="Button4" class="btn btn-primary" runat="server" Text="Go" OnClick="Button4_Click" AutoPostBack="true"/>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Score <1-5></label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" placeholder="Score" ></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Comment</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Comment" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label>Client ID</label>
                                <asp:SqlDataSource ID="SqlDataSourceClients" runat="server" ConnectionString="<%$ ConnectionStrings:DluttoConnectionString %>" SelectCommand="SELECT ID_uzytkownika
FROM dbo.Uzytkownicy
WHERE Rola_uzytkownika = 'Klient';"></asp:SqlDataSource>
                                <asp:DropDownList CssClass="form-control" ID="DropDownListClients" runat="server" AppendDataBoundItems="true" DataSourceID="SqlDataSourceClients" DataTextField="ID_uzytkownika" DataValueField="ID_uzytkownika">
                                    <asp:ListItem Text="-- Select Client --" Value="" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label>Trainer ID</label>
                                <asp:SqlDataSource ID="SqlDataSourceTrainers" runat="server" ConnectionString="<%$ ConnectionStrings:DluttoConnectionString %>" SelectCommand="SELECT [ID_trenera] FROM [Trenerzy]"></asp:SqlDataSource>
                                <asp:DropDownList CssClass="form-control" ID="DropDownListTrainers" runat="server" AppendDataBoundItems="true" DataSourceID="SqlDataSourceTrainers" DataTextField="ID_trenera" DataValueField="ID_trenera">
                                    <asp:ListItem Text="-- Select Trainer --" Value="" />
                                </asp:DropDownList>
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
                                    <asp:Button ID="Button2" class="btn btn-warning btn-block btn-lg" runat="server" Text="Update" OnClick="Button2_Click" AutoPostBack="true" />
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
                <a href="Homepage.aspx"><< Back to home page</a><br><br>
            </div>
            <div class="col-md-6">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img src="imgs/eval1.png" width="100px"/>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>List of evaluations</h3>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DluttoConnectionString %>" SelectCommand="SELECT * FROM [Oceny_trenerow]"></asp:SqlDataSource>
                                <hr>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:GridView CssClass="table table-striped table-bordered" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID_oceny" DataSourceID="SqlDataSource1" ShowHeaderWhenEmpty="True">
                                    <Columns>
                                        <asp:BoundField DataField="ID_oceny" HeaderText="Score ID" InsertVisible="False" ReadOnly="True" SortExpression="ID_oceny" />
                                        <asp:BoundField DataField="Ocena" HeaderText="Score" SortExpression="Ocena" />
                                        <asp:BoundField DataField="Komentarz" HeaderText="Comment" SortExpression="Komentarz" />
                                        <asp:BoundField DataField="ID_klienta" HeaderText="Client ID" SortExpression="ID_klienta" />
                                        <asp:BoundField DataField="ID_trenera" HeaderText="Trainer ID" SortExpression="ID_trenera" />
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
