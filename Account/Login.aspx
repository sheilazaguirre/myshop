<%@ Page Title="" Language="C#" MasterPageFile="~/Customer.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Account_Login" %>

<%@ Register Assembly="Recaptcha" Namespace="Recaptcha" TagPrefix="recaptcha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
    Account Login
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
    <form runat="server" class="form-horizontal">
        <div class="col-lg-6">
            <div id="error" runat="server" class="alert alert-danger"
                visible="false">
                Email or Password don't match!
            </div>
            <div class="form-group">
                <label class="control-label col-lg-4">Email Address</label>
                <div class="col-lg-8">
                    <asp:TextBox ID="txtEmail" runat="server"
                        class="form-control" type="email" MaxLength="100"
                        required />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-4">Password</label>
                <div class="col-lg-8">
                    <asp:TextBox ID="txtPassword" runat="server"
                        class="form-control" type="password" MaxLength="50"
                        required />
                </div>
            </div>
            
            <div class="form-group">
                <div class="col-lg-offset-4 col-lg-8">
                    <asp:Button ID="btnLogin" runat="server"
                        CssClass="btn btn-success" Text="Login"
                        OnClick="btnLogin_Click" />
                </div>
            </div>
        </div>
    </form>
</asp:Content>
