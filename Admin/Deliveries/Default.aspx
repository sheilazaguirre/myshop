<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Deliveries_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
    <i class="fa fa-truck"></i> View Deliveries
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
    <form runat="server" class="form-horizontal">
        <div class="col-lg-12">
            <table class="table table-hover">
                <thead>
                    <th>#</th>
                    <th>Order #</th>
                    <th>Date Ordered</th>
                    <th>Deadline</th>
                    <th>Total Quantity</th>
                    <th>Date Delivered</th>
                    <th>Status</th>
                    <th></th>
                </thead>
                <tbody>
                    <asp:ListView ID="lvDeliveries" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("DeliveryNo") %></td>
                                <td>
                                    <a href='../Orders/Details.aspx?OR=<%# Eval("OrderNo") %>'>
                                        <asp:Literal ID ="ltOrderNo" runat="server"
                                            Text='<%# Eval("OrderNo") %>' />
                                    </a>
                                </td>
                                <td><%# Eval("DateOrdered", "{0: MM/dd/yyyy}") %></td>
                                <td><%# Eval("Deadline", "{0: MM/dd/yyyy}") %></td>
                                <td><%# Eval("TotalQuantity") %></td>
                                <td><%# Eval("DateDelivered", "{0: MM/dd/yyyy}") %></td>
                                <td><%# Eval("Status") %></td>
                                <td>
                                    <asp:LinkButton ID="btnDeliver" runat="server"
                                        CommandName="deliver" 
                                        OnClientClick='return confirm("Deliver items?");'>
                                        <i class="fa fa-motorcycle"></i>
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </tbody>
            </table>
        </div>
    </form>
</asp:Content>

