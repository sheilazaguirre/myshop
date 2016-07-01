<%@ Page Title="" Language="C#" MasterPageFile="~/Customer.master" AutoEventWireup="true" CodeFile="Details.aspx.cs" Inherits="Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
    <asp:Literal ID="ltName" runat="server" /> 
    (<asp:Literal ID="ltCode" runat="server" />)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
    <form runat="server" class="form-horizontal">
        <div class="col-lg-6">
            <asp:Image ID="imgProduct" runat="server"
                class="img-responsive" />
        </div>
        <div class="col-lg-6">
            <div class="well">
                <h3>Category: 
                    <asp:HyperLink ID="hlkCategory" 
                        runat="server" />
                </h3>
                <h3>Description:</h3>
                <p>
                    <asp:Literal ID="ltDesc" runat="server" />
                </p>
                <h3>
                    Price: Php<asp:Literal ID="ltPrice" 
                        runat="server" />
                </h3>
                <div class="input-group col-lg-4">
                    <asp:TextBox ID="txtQuantity" runat="server"
                        min="1" max="99" type="number"
                        class="form-control"
                        text="1" required />
                    <span class="input-group-btn">
                        <asp:Button ID="btnAddToCart" runat="server"
                            class="btn btn-success"
                            Text="Add to Cart" OnClick="btnAddToCart_Click" />
                    </span>
                </div>
            </div>
        </div>
    </form>
</asp:Content>