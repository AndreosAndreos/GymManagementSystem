<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="usersingup.aspx.cs" Inherits="Dlutto_management.usersingup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container">
        <div class="row">
            <div class="col-md-8 mx-auto">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>

                                    <img src="imgs/newuser.png" width="100px"/>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>User sign up</h3>
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
                                <label>First name</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="First name" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Surname</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" placeholder="Surrname" ></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Email</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" placeholder="Email" TextMode="Email"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Date of birth</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" placeholder="Date of birth" TextMode="Date" ></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Password</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox7" runat="server" placeholder="Password" Textmode="Password"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>User role</label>
                                     <div class="form-group">
                                     <asp:DropDownList CssClass="form-control" ID="DropDownList2" runat="server">
                                     <asp:ListItem Text="1" Value="1" />
                                     
                                     
                                 </asp:DropDownList>
                                 </div>
                            </div>
                        </div>
                       <div class="row">
                            <div class="col-md-6">
                                <label>Role ID</label>
                                    <div class="form-group">
                                        <asp:DropDownList class="form-control" ID="DropDownList1" runat="server">                                        
                                        <asp:ListItem Text="Client" Value="Klient" />
                                        </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label>Phone number</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox10" runat="server" placeholder="Phone number" Textmode="Number"></asp:TextBox>
                                </div>
                            </div>
                           <div class="col-md-6">
                             <label>Date of beggining</label>
                                 <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox11" runat="server" placeholder="Data rozpoczecia czlonkostwa" TextMode="Date" ></asp:TextBox>
                                 </div>
                            </div> 
                        </div>
                        <div class="row">
                            <div class="col">
                                <div class="form-group d-grid gap-2">
                                    <asp:Button class="btn btn-success btn-lg" ID="Button1" runat="server" Text="Sign up" OnClick="Button1_Click" />
                                </div>
                            </div>
                                                         
                        </div>
                    </div>
                </div>
                <a href="Homepage.aspx"><< Back to home page</a><br><br>
                 </div>
            </div>
        </div>
</asp:Content>
