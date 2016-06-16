<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Site.master" AutoEventWireup="true" CodeFile="Details.aspx.cs" Inherits="Admin_Products_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
    <i class="fa fa-plus"></i> View Product #<asp:Literal ID="ltID" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
    <form runat="server" class="form-horizontal">
        <div class="col-lg-6">
            <div class="form-group">
                <label class="control-label col-lg-4">Name</label>
                <div class="col-lg-8">
                    <asp:TextBox ID="txtName" runat="server" class="form-control" MaxLength="50" required />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-4">Category</label>
                <div class="col-lg-8">
                    <asp:DropDownList ID="ddlCategories" runat="server" class="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-4">Code</label>
                <div class="col-lg-8">
                    <asp:TextBox ID="txtCode" runat="server" class="form-control" MaxLength="10" required />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-4">Description</label>
                <div class="col-lg-8">
                    <asp:TextBox ID="txtDesc" runat="server" class="form-control" TextMode="MultiLine" MaxLength="300" required />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-4">Image</label>
                <div class="col-lg-8">
                    <asp:Image ID="imgProduct" runat="server" class="img-responsive" Width="200" /><br />
                    <asp:FileUpload ID="fuImage" runat="server" class="form-control" />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-4">Price</label>
                <div class="col-lg-8">
                    <asp:TextBox ID="txtPrice" runat="server" class="form-control" type="number"
                        min="1.0" max="100000.00" step="0.01" required />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-4">Is Featured?</label>
                <div class="col-lg-8">
                    <asp:DropDownList ID="ddlFeatured" runat="server" class="form-control">
                        <asp:ListItem>Yes</asp:ListItem>
                        <asp:ListItem>No</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="col-lg-6">
            <div class="form-group">
                <label class="control-label col-lg-4">Critical Level</label>
                <div class="col-lg-8">
                    <asp:TextBox ID="txtCritical" runat="server" CssClass="form-control" AutoCompleteType="number"
                        min="1" max="99" required />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-4">Maximum</label>
                <div class="col-lg-8">
                    <asp:TextBox ID ="txtMaximum" runat="server"
                        class="form-control" type="number"
                        min="1" max="99" required />
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <span class="pull-right">
                <asp:Button ID="btnCancel" runat="server" class="btn btn-default" Text="Cancel"
                    PostBackUrl="~/Admin/Products/Default.aspx" formnovalidate />
                <asp:Button ID="btnUpdate" runat="server" class="btn btn-success" Text="Update" OnClick="btnUpdate_Click"/>
            </span>
        </div>
    </form>
</asp:Content>

