<%@ Page Title="" Language="C#" MasterPageFile="~/Customer.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Account_Register" %>

<%--<%@ Register Assembly="Recaptcha" Namespace="Recaptcha" TagPrefix="recaptcha" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
    Account Registration
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
    <form runat="server" class="form-horizontal">
        <div class="col-lg-6">
            <div id="error" runat="server" class="alert alert-danger"
                visible="false">
                Email address already taken!
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
                <label class="control-label col-lg-4">Repeat Password</label>
                <div class="col-lg-8">
                    <asp:TextBox ID="txtRPassword" runat="server"
                        class="form-control" type="password" MaxLength="50"
                        required />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-4">First Name</label>
                <div class="col-lg-8">
                    <asp:TextBox ID="txtFN" runat="server"
                        class="form-control" MaxLength="100"
                        required />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-4">Last Name</label>
                <div class="col-lg-8">
                    <asp:TextBox ID="txtLN" runat="server"
                        class="form-control" MaxLength="50"
                        required />
                </div>
            </div>
<%--            <div class="form-group">
                <div class="col-lg-offset-4 col-lg-8">
                    <recaptcha:RecaptchaControl 
                        ID="rcRegister" runat="server"
                        PublicKey="6LeqviATAAAAAJZrhGNtUhYx6yO2JQxBY0q-3NT_"
                        PrivateKey="6LeqviATAAAAAGJemPntv5f2W8A9HL5yGZ6jUiBo"
                        Theme="clean" />
                </div>
            </div>--%>
            <div class="form-group">
                <div class="col-lg-offset-4 col-lg-8">
                    <asp:Button ID="btnRegister" runat="server"
                        CssClass="btn btn-success" Text="Register"
                        OnClick="btnRegister_Click" />
                </div>
            </div>
        </div>
    </form>
</asp:Content>