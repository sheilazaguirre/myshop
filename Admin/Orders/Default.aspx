<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Orders_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
    <i class="fa fa-shopping-cart"></i> View Orders
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
    <form runat="server" class="form-horizontal">
        <div class="col-lg-2">
            <asp:TextBox ID="txtStart" runat="server"
                AutoPostBack="true" OnTextChanged="SearchByDate"
                class="form-control" type="date" />
        </div>
        <div class="col-lg-2">
            <asp:TextBox ID="txtEnd" runat="server"
                AutoPostBack="true" OnTextChanged="SearchByDate"
                class="form-control" type="date" />
        </div>
        <div class="row"></div>
        <div class="col-lg-12">
            <table class="table table-hover">
                <thead>
                    <th>Order No.</th>
                    <th>Date Ordered</th>
                    <th>Payment Method</th>
                    <th># of Items</th>
                    <th>Total Amount</th>
                    <th>Status</th>
                    <th></th>
                </thead>
                <tbody>
                    <asp:ListView ID="lvOrders" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("OrderNo") %></td>
                                <td><%# Eval("DateOrdered", 
                                    "{0: MMMM dd, yyyy}") %></td>
                                <td><%# Eval("PaymentMethod") %></td>
                                <td><%# Eval("TotalItems") %></td>
                                <td>Php<%# Eval("TotalAmount",
                                    "{0: #,###,##0.00}") %></td>
                                <td><%# Eval("Status") %></td>
                                <td>
                                    <a href='Details.aspx?OR=<%# Eval("OrderNo") %>'>
                                        <i class="fa fa-list"></i>
                                    </a>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <tr>
                                <td colspan="6">
                                    <h2 class="text-center">
                                        No records found.
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