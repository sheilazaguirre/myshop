<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Products_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
    <i class="fa fa-shopping-cart"></i> View Products
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
    <form runat="server" class="form-horizontal">
        <div class="col-lg-12">
            <table class="table table-hover">
                <thead>
                    <th>#</th>
                    <th>Category</th>
                    <th>Name</th>
                    <th>Image</th>
                    <th>Description</th>
                    <th>Price</th>
                    <th>Is Featured?</th>
                    <th>Status</th>
                    <th>Date Added</th>
                    <th>Date Modified</th>
                </thead>
                <tbody>
                    <asp:ListView ID="lvProducts" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("ProductID") %></td>
                                <td><%# Eval("Category") %></td>
                                <td>
                                    <%# Eval("Name") %>
                                    <%# Eval("Code") %>
                                </td>
                                <td>
                                    <img src='../../Content/img/products/<%# Eval("Image") %>'
                                        width="100" />
                                </td>
                                <td>
                                    <%# Server.HtmlDecode(Eval("Description").ToString()) %>
                                </td>
                                <td>
                                    <%# Eval("Price", "{0: #,###,##0.00}") %>
                                </td>
                                <td><%# Eval("IsFeatured") %></td>
                                <td><%# Eval("Status") %></td>
                                <td><%# Eval("DateAdded", "{0: MM/dd/yyyy}") %></td>
                                <td><%# Eval("DateModified", "{0: MM/dd/yyyy}") %></td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <tr>
                                <td colspan="10">
                                    <h2 class="text-center">
                                        No records found!
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

