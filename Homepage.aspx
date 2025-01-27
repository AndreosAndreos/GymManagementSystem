<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="Dlutto_management.Homepage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <center>
            <h2>Welcome to our page!</h2>
            <img src="imgs/gym%20home.jpg" class="img-fluid" />
        </center>
    </section>
    <section>
        <center>
            <h1>Our locations:</h1>
        </center>
        <div class="container">
            <div class="row">
                <div class="col-md-6">
                    <center>
                        <img src="imgs/lokacja.png" width="90" height="90"/>
                        <h2>OSP Saperska</h2>
                    </center>
                </div>
                <div class="col-md-6">
                    <center>
                        <img src="imgs/lokacja.png" width="90" height="90"/>
                        <h2>Home gym</h2>
                    </center>
                </div>
            </div>
        </div>
    </section>
     <br />
</asp:Content>
