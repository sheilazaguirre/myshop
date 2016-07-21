<%@ Page Title="" Language="C#" MasterPageFile="~/Customer.master" AutoEventWireup="true" CodeFile="Details.aspx.cs" Inherits="Account_Orders_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    <i class="fa fa-money"></i> Order #<asp:Literal ID="ltOrderNo" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <form runat="server" class="form-horizontal">
        <div class="col-lg-9">
            <table class="table table-hover">
                <thead>
                    <th colspan="2">Product</th>
                    <th>Price</th>
                    <th>Quantity</th>
                    <th>Amount</th>
                </thead>
                <tbody>
                    <asp:ListView ID="lvOrders" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <img runat="server" src='<%# string.Concat("~/content/img/products/", Eval("Image")) %>'
                                        width="150" class="img-responsive" />
                                </td>
                                <td>
                                    <a href='../Details.aspx?ID=<%# Eval("ProductID") %>'>
                                        <%# Eval("Name") %> (<%# Eval("Code") %>)<br />
                                        <small>Category: <%# Eval("Category") %></small>
                                    </a>
                                </td>
                                <td>Php<%# Eval("Price", "{0: #,###,###.00}") %>
                                </td>
                                <td>
                                    <%# Eval("Quantity") %>
                                </td>
                                <td>Php<%# Eval("Amount", "{0: #,###,###.00}") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </tbody>
            </table>
            <br />
            <hr />
            <h3>Billing and Delivery Details</h3>
            <div class="col-lg-6">
                <div class="form-group">
                    <label class="control-label col-lg-4">First Name</label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="txtFN" runat="server" class="form-control" MaxLength="80" disabled />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-lg-4">Last Name</label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="txtLN" runat="server" class="form-control" MaxLength="50" disabled />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-lg-4">Street</label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="txtStreet" runat="server" class="form-control" MaxLength="50" disabled />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-lg-4">Municipality</label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="txtMunicipality" runat="server" class="form-control" MaxLength="100" disabled />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-lg-4">City</label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="txtCity" runat="server" class="form-control" MaxLength="50" disabled />
                    </div>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="form-group">
                    <label class="control-label col-lg-4">Phone</label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="txtPhone" runat="server" class="form-control" MaxLength="12" disabled />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-lg-4">Mobile</label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="txtMobile" runat="server" class="form-control" MaxLength="12" disabled />
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-3">
            <div class="well">
                <h3>Order Summary</h3>
                <table class="table table-hover">
                    <tbody>
                        <tr>
                            <td>Date Ordered</td>
                            <td align="right">
                                <asp:Literal ID="ltDateOrdered" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>Payment Method</td>
                            <td align="right">
                                <asp:Literal ID="ltPayment" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>Status</td>
                            <td align="right">
                                <asp:Literal ID="ltStatus" runat="server" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="well">
                <h3>Payment Summary</h3>
                <table class="table table-hover">
                    <tbody>
                        <tr>
                            <td>Gross Amount</td>
                            <td align="right">Php
                                <asp:Literal ID="ltGross" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>VAT (12%)</td>
                            <td align="right">Php
                                <asp:Literal ID="ltVAT" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>Delivery Fee</td>
                            <td align="right">Php
                                <asp:Literal ID="ltDelivery" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>Total Amount</td>
                            <td align="right">Php
                                <asp:Literal ID="ltTotal" runat="server" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <hr />
            <a href="Default.aspx" class="btn btn-default btn-lg btn-block">
                Back to View
            </a>
        </div>
    </form>
</asp:Content>