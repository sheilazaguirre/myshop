<%@ Page Title="" Language="C#" MasterPageFile="~/Customer.master" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
    <i class="fa fa-shopping-cart"></i> Products
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
    <form runat="server" class="form-horizontal">
        <div class="col-lg-3">
            <div class="list-group">
                <a href="Products.aspx" class="list-group-item active">
                    <span class="badge">
                        <asp:Literal ID="ltTotal" runat="server" />
                    </span>
                    All Categories
                </a>
                <asp:ListView ID="lvCategories" runat="server">
                    <ItemTemplate>
                        <a href='Products.aspx?c=<%# Eval("CatID") %>'
                            class="list-group-item">
                            <span class="badge">
                                <%# Eval("TotalCount") %>
                            </span>
                            <%# Eval("Category") %>
                        </a>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
        <div class="col-lg-9">
            <asp:ListView ID="lvProducts" runat="server"
                OnItemCommand="lvProducts_ItemCommand">
                <ItemTemplate>
                    <asp:Literal ID="ltProductID" runat="server"
                        Text='<%# Eval("ProductID") %>' Visible="false" />
                    <div class="col-lg-4">
                        <a href='Details.aspx?ID=<%# Eval("ProductID") %>'
                            style="text-decoration: none;">
                        <div class="thumbnail">
                        <div title='<%# Eval("Name") %>' 
                            style="position:relative; width: 100%; height: 0; 
                            padding-bottom: 100% ; background-repeat: no-repeat;
                            background-position: top center; background-size: cover;
                            background-image: url('content/img/products/<%# Eval("Image") %>');">  
                        </div>
                            <div class="caption">
                                <h3 style="height: 50px;">
                                    <%# Eval("Name") %>
                                </h3>
                                <h4>Category: <%# Eval("Category") %></h4>
                                <p>
                                    Php<%# Eval("Price", "{0: #,###,###.00}") %>
                                </p>
                                <asp:LinkButton ID="btnAddToCart" runat="server"
                                    class="btn btn-block btn-success btn-lg"
                                    CommandName="addtocart">
                                    <i class="fa fa-plus"></i> Add to Cart
                                </asp:LinkButton>
                            </div>
                        </div>
                        </a>
                    </div>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <br />
                    <h2 class="text-center">
                        No products found.
                    </h2>
                    <br />
                </EmptyDataTemplate>
            </asp:ListView>
        </div>
    </form>
</asp:Content>