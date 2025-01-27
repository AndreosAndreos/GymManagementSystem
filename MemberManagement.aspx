<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="MemberManagement.aspx.cs" Inherits="Dlutto_management.MemberManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
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
                                    <img src="imgs/member.png" width="100px"/>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>Member</h3>
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
                                <label>Member ID</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="Member ID"></asp:TextBox>
                                        <asp:Button ID="Button4" class="btn btn-primary" runat="server" Text="Go" OnClick="Button4_Click" AutoPostBack="true"/>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>First name</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" placeholder="First name"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>Name</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Name"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label>Email</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Email"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>Password</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>Role</label>
                                <div class="form-group">
                                    <asp:DropDownList CssClass="form-control" ID="DropDownList3" runat="server">
                                        <asp:ListItem Text="Select Role" Value="" />
                                        <asp:ListItem Text="Klient" Value="Klient" />
                                        <asp:ListItem Text="Trener" Value="Trener" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label>Date of birth</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox7" runat="server" placeholder="Date of birth" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>Permission ID</label>
                                <div class="form-group">
                                    <asp:DropDownList CssClass="form-control" ID="DropDownList1" runat="server">
                                    <asp:ListItem Text="Select Permission" Value="" />
                                    <asp:ListItem Text="1" Value="1" />
                                    <asp:ListItem Text="3" Value="3" />
                                    <asp:ListItem Text="5" Value="5" />
                                </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DluttoConnectionString %>" SelectCommand="SELECT ID_uprawnienia FROM Uprawnienia WHERE ID_uprawnienia!=2"></asp:SqlDataSource>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>Phone number</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox9" runat="server" placeholder="Phone number"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label>Membership start date</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox10" runat="server" placeholder="Membership start date" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <label>Membership end date</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox11" runat="server" placeholder="Membership end date" TextMode="Date"></asp:TextBox>
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
                <a href="Homepage.aspx"><< Back to home page</a><br><br>
            </div>
            <div class="col-md-6">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img src="imgs/members.png" width="100px"/>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>List of members</h3>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DluttoConnectionString %>" SelectCommand="SELECT [ID_uzytkownika], [Imie], [Nazwisko], [Email], [Rola_uzytkownika], [Data_urodzenia], [ID_uprawnienia], [Nr_telefonu], [Data_rozp_czlonkostwa], [Data_zak_czlonkostwa] FROM [Uzytkownicy] WHERE Rola_Uzytkownika != 'Admin'"></asp:SqlDataSource>
                                <hr>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <asp:GridView Class="table table-striped table-bordered" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID_uzytkownika" DataSourceID="SqlDataSource1" ShowHeaderWhenEmpty="True">
                                    <Columns>
                                        <asp:BoundField DataField="ID_uzytkownika" HeaderText="User ID" InsertVisible="False" ReadOnly="True" SortExpression="ID_uzytkownika" />
                                        <asp:BoundField DataField="Imie" HeaderText="Name" SortExpression="Imie" />
                                        <asp:BoundField DataField="Nazwisko" HeaderText="Surname" SortExpression="Nazwisko" />
                                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                        <asp:BoundField DataField="Rola_uzytkownika" HeaderText="User role" SortExpression="Rola_uzytkownika" />
                                        <asp:BoundField DataField="Data_urodzenia" HeaderText="Birth date" SortExpression="Data_urodzenia" />
                                        <asp:BoundField DataField="ID_uprawnienia" HeaderText="Authorization ID" SortExpression="ID_uprawnienia" />
                                        <asp:BoundField DataField="Nr_telefonu" HeaderText="Phone number" SortExpression="Nr_telefonu" />
                                        <asp:BoundField DataField="Data_rozp_czlonkostwa" HeaderText="Start date of membership" SortExpression="Data_rozp_czlonkostwa" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False" />
                                        <asp:BoundField DataField="Data_zak_czlonkostwa" HeaderText="End date of membership" SortExpression="Data_zak_czlonkostwa" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False" />
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
