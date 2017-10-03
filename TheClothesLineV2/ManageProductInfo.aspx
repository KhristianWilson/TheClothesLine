<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ManageProductInfo.aspx.cs" Inherits="TheClothesLineV2.ManageProductInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function confirmAction(message) {
            if (Page_ClientValidate()) {
                return confirm(message);
            }
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Conent2" runat="server" ContentPlaceHolderID="products">
    <asp:FormView ID="frmCatDetails" runat="server">
        <HeaderTemplate>
            <h3>Manage Products Information</h3>
        </HeaderTemplate>
        <ItemTemplate>

            <asp:Image ID="prodImage" class="padRight" runat="server" Height="162px" Width="181px" ImageUrl='<%#Eval("PImage") %>' /><br />

            <div id="textfield">
                <asp:Label ID="lblProdId" runat="server" Text='Product ID: ' />
                <asp:Label ID="prodId" runat="server" Text='<%#Eval("ProductID") %>'></asp:Label><br />

                <asp:Label ID="lblCatId" runat="server" Text='Category ID: ' />
                <asp:DropDownList ID="ddlcatId" runat="server" AppendDataBoundItems="True" DataTextField="CategoryName"
                    DataValueField='CategoryID'
                    DataSourceID="categoryList"
                    SelectedValue='<%# Bind("CategoryID") %>'>
                    <asp:ListItem>New Product</asp:ListItem>
                </asp:DropDownList><br />

                <asp:SqlDataSource ID="categoryList" runat="server" ConnectionString="<%$ ConnectionStrings:cnn%>"
                    SelectCommand="spgetCategories"></asp:SqlDataSource>

                <label id="prodName">Product Name:</label>
                <asp:TextBox ID="txtProdName" runat="server" Text='<%#Eval("PName") %>'></asp:TextBox>
                <asp:RequiredFieldValidator ID="Validator2" runat="server" ErrorMessage="Product Name Cannot be empty" ControlToValidate="txtProdName" ForeColor="Red"></asp:RequiredFieldValidator><br />

                <label id="prodBDesc">Brief Product Description:</label>
                <asp:TextBox ID="txtProdBDesc" runat="server" Text='<%#Eval("BriefDesc") %>'></asp:TextBox>
                <asp:RequiredFieldValidator ID="Validator3" runat="server" ErrorMessage="Product brief description cannot be empty" ControlToValidate="txtProdBDesc" ForeColor="Red"></asp:RequiredFieldValidator><br />

                <label id="prodFDesc">Full Product Description:</label>
                <asp:TextBox ID="txtProdFDesc" runat="server" TextMode="multiline" Columns="50" Rows="5" Text='<%#Eval("FullDesc") %>'></asp:TextBox>
                <asp:RequiredFieldValidator ID="Validator4" runat="server" ErrorMessage="Product full description cannot be empty" ControlToValidate="txtProdFDesc" ForeColor="Red"></asp:RequiredFieldValidator><br />

                <label id="prodFeatured">Featured Product?</label>
                <asp:CheckBox ID="chkFeatured" runat="server" Checked='<%# Eval("Featured") %>' /><br />

                <label id="prodPrice">Product Price:</label><asp:TextBox ID="txtProdPrice" runat="server" Text='<%#Eval("Price", "{0:c}") %>'></asp:TextBox>
                <asp:RequiredFieldValidator ID="Validator5" runat="server" ErrorMessage="Product price cannot be empty" ControlToValidate="txtProdPrice" ForeColor="Red"></asp:RequiredFieldValidator><br />

                <label id="prodStatus">Product Status: </label>
                <asp:DropDownList ID="ddlProdStatus" runat="server" SelectedValue='<%# Bind("Status")%>'>
                    <asp:ListItem Value="1">Available</asp:ListItem>
                    <asp:ListItem Value="2">Out of stock</asp:ListItem>
                    <asp:ListItem Value="3">Back ordered</asp:ListItem>
                    <asp:ListItem Value="4">Temporarily available</asp:ListItem>
                    <asp:ListItem Value="5">Discontinued</asp:ListItem>
                </asp:DropDownList>
            </div>
            </div>
        </ItemTemplate>
    </asp:FormView>
    <div id="cart">
        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" OnClientClick="return confirmAction('Do you want to add this product');" />
        <asp:Button class="padRight" ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" OnClientClick="return confirmAction('Do you want to update this product');" />
        <asp:Button ID="btnRemove" runat="server" Text="Remove" OnClick="btnRemove_Click" OnClientClick="return confirmAction('Do you want to delete this product');" />
        <asp:Button ID="btnClear" runat="server" Text="Clear Product" OnClick="btnClear_Click" />
    </div>
</asp:Content>
