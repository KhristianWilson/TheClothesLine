<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ProductDisplay.aspx.cs" Inherits="TheClothesLineV2.ProductDisplay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Conent2" runat="server" ContentPlaceHolderID="products">
    <div class="prodDisplay">
        <asp:Repeater ID="rptFeatured" runat="server">
            <HeaderTemplate>
                <asp:Label ID="lblheader" runat="server" Text=""></asp:Label>
            </HeaderTemplate>
            <ItemTemplate>
                <div class="featured">
                    <div id="prodPic">
                        <a href='ProductDisplay.aspx?pId=<%#Eval("ProductID") %>'>
                            <img class="thumb" src="<%#Eval("PImage") %>" /></a><br />
                    </div>
                    <div id="prodInfo">
                        <p id="prodName">Product Name: <%#Eval("PName") %></p>
                        <p id="prodSDesc">Product Description: <%#Eval("BriefDesc") %></p>
                        <p id="prodPrice">Product Price: <%#Eval("Price", "{0:c}") %></p>
                        <p id="prodId">Product ID: <%#Eval("ProductID") %></p>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>        
    </div>


    <div class="prodDisplay">
        <asp:FormView ID="frmDetails" runat="server">
            <HeaderTemplate>
                <h2>Product Details</h2>
            </HeaderTemplate>
            <ItemTemplate>
                <img class="fullSize" src="<%#Eval("PImage") %>" />
                <div id="textfield">
                    <h3> <%#Eval("PName") %></h3>
                    <asp:Label id="lblpro" runat="server" Text="Product ID"></asp:Label>
                    <asp:Label id="prodId" runat="server" Text='<%#Eval("ProductID") %>'>Product ID:</asp:Label>
                    <p id="prodBDesc">Beief Description: <%#Eval("BriefDesc") %></p>
                    <p id="prodFDesc">Full Description:<br /> <%#Eval("FullDesc") %></p>
                    <p id="prodPrice">Price: <%#Eval("Price","{0:c}") %></p>
                    <p runat="server" id="prodFeatured">Featured: <%#Eval("Featured") %></p>
                    <div id="cart">
                        <asp:Button ID="btnCart" runat="server" Text="Add To Cart" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click"/>
                    </div>
                </div>
            </ItemTemplate>
        </asp:FormView>
    </div>
</asp:Content>
