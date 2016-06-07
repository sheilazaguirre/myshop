<%@ Page Title="" Language="C#" MasterPageFile="~/Customer.master" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Post_Add" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
    <i class="fa fa-plus"></i> Add Post
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
    <form runat="server" class="form-horizontal">
        <div class="col-lg-6">
            <div class="form-group">
                <label class="control-label col-lg-4">Post Date</label>
                <div class="col-lg-8">
                    <asp:TextBox ID="txtPD" runat="server" CssClass="form-control" type="date" required />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-4">Post Type</label>
                <div class="col-lg-8">
                    <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" required />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-4">Title</label>
                <div class="col-lg-8">
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" MaxLength="20" required />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-4">Post</label>
                <div class="col-lg-8">
                    <CKEditor:CKEditorControl ID="txtPost" runat="server" Rows="5" BasePath="/Scripts/ckeditor">
                    </CKEditor:CKEditorControl>

                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-4">Featured Image</label>
                <div class="col-lg-8">
                    <asp:FileUpload ID="fuImage" runat="server" CssClass="form-control" required />
                </div>
            </div>
        </div>
        <div class="col-lg-6">
            <div class="form-group">
                <label class="control-label col-lg-4">Keyword</label>
                <div class="col-lg-8">
                    <asp:TextBox ID="txtKeyword" runat="server" CssClass="form-control" required />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-lg-4">Status</label>
                <div class="col-lg-8">
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" required>
                        <asp:ListItem>Published</asp:ListItem>
                        <asp:ListItem>Draft</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div>
                <div class="col-lg-offset-4 col-lg-8">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" Text="Add" OnClick="btnAdd_Click" />
                </div>
            </div>
        </div>
    </form>
</asp:Content>

