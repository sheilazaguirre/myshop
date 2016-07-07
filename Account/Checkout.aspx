<%@ Page Title="" Language="C#" MasterPageFile="~/Customer.master" AutoEventWireup="true" CodeFile="Checkout.aspx.cs" Inherits="Account_Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
    <i class="fa fa-money"></i> Checkout
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
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
                    <asp:ListView ID="lvCart" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <img src='../content/img/products/<%# Eval("Image") %>'
                                        width="150" class="img-responsive" />
                                </td>
                                <td>
                                    <a href='../Details.aspx?ID=<%# Eval("ProductID") %>'>
                                        <%# Eval("Name") %> (<%# Eval("Code") %>)<br />
                                        <small>Category: <%# Eval("Category") %></small>
                                    </a>
                                </td>
                                <td>
                                    Php<%# Eval("Price", "{0: #,###,###.00}") %>
                                </td>
                                <td>
                                    <%# Eval("Quantity") %>
                                </td>
                                <td>
                                    Php<%# Eval("Amount", "{0: #,###,###.00}") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </tbody>
            </table><br />
            <hr />
            <h3>Billing and Delivery Details</h3>
            <div class="col-lg-6">
                <div class="form-group">
                    <label class="control-label col-lg-4">First Name</label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="txtFN" runat="server" class="form-control" MaxLength="80" required />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-lg-4">Last Name</label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="txtLN" runat="server" class="form-control" MaxLength="50" required />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-lg-4">Street</label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="txtStreet" runat="server" class="form-control" MaxLength="50" required />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-lg-4">Municipality</label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="txtMunicipality" runat="server" class="form-control" MaxLength="100" required />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-lg-4">City</label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="txtCity" runat="server" class="form-control" MaxLength="50" required />
                    </div>
                </div>
                <br />
                <br />
            </div>
            <div class="col-lg-6">
                <div class="form-group">
                    <label class="control-label col-lg-4">Phone</label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="txtPhone" runat="server" class="form-control" MaxLength="12" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-lg-4">Mobile</label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="txtMobile" runat="server" class="form-control" MaxLength="12" />
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-3 well">
            <h3>Payment Summary</h3>
            <table class="table table-hover">
                <tbody>
                    <tr>
                        <td>Gross Amount</td>
                        <td align="right">
                            Php <asp:Literal ID="ltGross" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>VAT (12%)</td>
                        <td align="right">
                            Php <asp:Literal ID="ltVAT" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Delivery Fee</td>
                        <td align="right">
                            Php <asp:Literal ID="ltDelivery" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Total Amount</td>
                        <td align="right">
                            Php <asp:Literal ID="ltTotal" runat="server" />
                        </td>
                    </tr>
                </tbody>
            </table>
            <hr />
            <div class="form-group">
                    <label class="control-label col-lg-4">Payment Method</label>
                    <div class="col-lg-8">
                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control">
                            <asp:ListItem>Cash Deposit</asp:ListItem>
                            <asp:ListItem>Check Deposit</asp:ListItem>
                            <asp:ListItem>Credit Card</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            <div class="form-group">
                <label class="control-label col-lg-3">Expected Delivery Date
                </label>
                <div class="col-lg-8">
                    <asp:TextBox ID="txtDelivery" runat="server" CssClass="form-control" disabled />
                </div>
            </div>
                <div class="form-group">
                    <div class="col-lg-12">
                        <label>
                            <asp:CheckBox ID="cboTerms" runat="server" required />
                            I have agreed to the <a href="#">Terms & Conditions.</a>
                        </label>
                    </div>
                </div>
                <asp:LinkButton ID="btnCheckout" runat="server" 
                    CssClass="btn btn-success btn-lg btn-block"
                    OnClientClick='return confirm("Are you sure?");'
                    OnClick="btnCheckout_Click">
                    <i class="fa fa-money"></i> Order Now
                </asp:LinkButton>
        </div>
    </form>
</asp:Content>