<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Suppliers_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
    <i class="fa fa-users"></i> View Suppliers
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
    <form runat="server" class="form-horizontal">
        <asp:Panel ID="pnlSuppliers" runat="server"
            DefaultButton="btnSearch">
        <div class="col-lg-offset-8">
            <div class="input-group">
                <asp:TextBox ID="txtKeyword" runat="server"
                    class="form-control" placeholder="Type a keyword..." />
                <span class="input-group-btn">
                    <asp:LinkButton ID="btnSearch" runat="server"
                        class="btn btn-info" OnClick="btnSearch_Click">
                        <i class="fa fa-search"></i>
                    </asp:LinkButton>
                </span>
            </div>
         </div>
            <br />
        <div class="col-lg-12">
            <table class="table table-hover">
                <thead>
                    <th>#</th>
                    <th>Company Name</th>
                    <th>Contact Person</th>
                    <th>Address</th>
                    <th>Contact Numbers</th>
                    <th>Status</th>
                    <th>Date Added</th>
                    <th>Date Modified</th>
                    <th>Actions</th>
                </thead>
                <tbody>
                    <asp:ListView ID="lvSuppliers" runat="server" OnItemCommand="lvSuppliers_ItemCommand" 
                        OnPagePropertiesChanging="lvSuppliers_PagePropertiesChanging"
                        OnItemDataBound="lvSuppliers_ItemDataBound" >
                        <ItemTemplate>
                            <tr>
                                <td><asp:Literal ID="ltSupplierID" runat ="server" 
                                    Text='<%# Eval("SupplierID") %>' /></td>
                                <td><%# Eval("CompanyName") %></td>
                                <td><%# Eval("ContactPerson") %></td>
                                <td><%# Eval("Address") %></td>
                                <td><%# Eval("Phone") %> / <%# Eval("Mobile") %></td>
                                <td><%# Eval("Status") %></td>
                                <td><%# Eval("DateAdded", "{0: MMMM dd, yyyy}") %></td>
                                <td><%# Eval("DateModified", "{0: MMMM dd, yyyy}") %></td>
                                <td>
                                    <a href='Details.aspx?ID=<%# Eval("SupplierID") %>'
                                        class="btn btn-xs btn-success" title="View Details">
                                        <i class="fa fa-edit"></i>
                                    </a>
                                    <asp:LinkButton ID="btnDelete"  runat="server"
                                        class="btn btn-xs btn-danger" title="Archive Record"
                                        CommandName="archive"
                                        OnClientClick='return confirm("Archive record?");'>
                                        <i class="fa fa-remove"></i>
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <tr>
                                <td colspan="8">
                                    <h2 class="text-center">No records found.</h2>
                                </td>
                            </tr>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </tbody>
            </table>
            <center>
                <asp:DataPager ID="dpSuppliers" runat="server"
                    PagedControlID="lvSuppliers" PageSize="10">
                    <Fields>
                        <asp:NumericPagerField
                            ButtonType="Button"
                            CurrentPageLabelCssClass="btn btn-success"
                            NumericButtonCssClass="btn btn-default"
                            NextPreviousButtonCssClass="btn btn-default"
                            ButtonCount="5" />
                    </Fields>
                </asp:DataPager>
            </center>
        </div>
        </asp:Panel>


    </form>
</asp:Content>
