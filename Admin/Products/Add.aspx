<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Site.master" AutoEventWireup="true" CodeFile="~/Admin/Products/Add.aspx.cs" Inherits="Admin_Products_Add"%>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
    <i class="fa fa-plus"></i> Add a Product
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
    <form runat="server" class="form-horizontal">
        <div class="col-lg-6">
            <div class="form-group">
                <label class="control-label col-lg-4">Name</label>
                <div class="col-lg-8">
                    <asp:TextBox ID ="txtName" runat="server"
                        class="form-control" MaxLength="100" required />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-4">Category</label>
                <div class="col-lg-8">
                    <asp:DropDownList ID="ddlCategories" runat="server" 
                        class="form-control" required />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-4">Code</label>
                <div class="col-lg-8">
                    <asp:TextBox ID ="txtCode" runat="server"
                        class="form-control" MaxLength="20" required />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-4">Description</label>
                <div class="col-lg-8">
                    <CKEditor:CKEditorControl ID="txtDesc" runat="server"
                        Rows="5" BasePath="~/Scripts/ckeditor">

                    </CKEditor:CKEditorControl>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-4">Image</label>
                <div class="col-lg-8">
                    <asp:FileUpload ID="fuImage" runat="server"
                        class="form-control" required />
                </div>
            </div>
        </div>
        <div class="col-lg-6">
            <div class="form-group">
                <label class="control-label col-lg-4">Selling Price</label>
                <div class="col-lg-8">
                    <asp:TextBox ID ="txtPrice" runat="server"
                        class="form-control" type="number"
                        min="1.0" max="10000.00" step="0.01" required />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-4">Is Featured</label>
                <div class="col-lg-8">
                    <asp:DropDownList ID ="ddlFeatured" runat="server"
                        class="form-control">
                        <asp:ListItem>Yes</asp:ListItem>
                        <asp:ListItem>No</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-4">Critical Level</label>
                <div class="col-lg-8">
                    <asp:TextBox ID ="txtCritical" runat="server"
                        class="form-control" type="number"
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
            <div class="form-group">
                <div class="col-lg-offset-4 col-lg-8">
                    <asp:Button ID="btnAdd" runat="server"
                        class="btn btn-success" Text="Add" 
                        OnClick="btnAdd_Click" />
                </div>
            </div>
        </div>
    </form>
</asp:Content>

