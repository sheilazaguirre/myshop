<%@ Page Title="" Language="C#" MasterPageFile="~/Customer.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Post_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
    <i class="fa fa-icon"></i> View Posts
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
    <form runat="server" class="form-horizontal">
        <div class="col-lg-12">
            <table class="table table-hover">
                <thead>
                    <th>#ID</th>
                    <th>Date</th>
                    <th>Type</th>
                    <th>Title</th>
                    <th>Featured Image</th>
                    <th>Post</th>
                    <th>Keywords</th>
                    <th>Status</th>
                </thead>
                <tbody>
                    <asp:ListView ID="lvPosts" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("PostID") %></td>
                                <td><%# Eval("PostDate", "{0: MM/dd/yyyy}") %></td>
                                <td><%# Eval("PostType") %></td>
                                <td><%# Eval("Title") %></td>
                                <td><img src='../../content/img/products/<%# Eval("FeaturedImage") %>' width="150" /></td>
                                <td><%# Server.HtmlDecode(Eval("Post").ToString()) %></td>
                                <td><%# Eval("Keywords") %></td>
                                <td><%# Eval("Status") %></td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <tr>
                                <td colspan="10">
                                    <h2 class="text-center">
                                        No posts found!
                                    </h2>
                                </td>
                            </tr>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </tbody>
            </table>
        </div>
    </form>
</asp:Content>