<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ManageCategoryInformation.aspx.cs" Inherits="TheClothesLineV2.ManageCategoryInformation" %>

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
    <div id="productUpdateHeader">
        <h2>Manage Category Information</h2>
    </div>
    <div id="catDetails">
        <asp:FormView ID="frmCatDetails" runat="server">
            <ItemTemplate>
                <div id="textfield">
                     <asp:Label ID="lblCatId" runat="server" Text='Category ID:'/><asp:Label ID="catId" runat="server" Text='<%#Eval("CategoryID") %>' /><br />
                    <label id="catName">Category Name:</label>
                    <asp:TextBox ID="txtCatName" runat="server" Text='<%#Eval("CategoryName") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="Validator2" runat="server" ErrorMessage="Category Name Cannot be empty" ControlToValidate="txtCatName" ForeColor="Red"></asp:RequiredFieldValidator><br />
                    <label id="catdesc">Category Description:</label>
                    <asp:TextBox ID="txtAreaDesc" TextMode="multiline" Columns="50" Rows="5" runat="server" Text='<%#Eval("CategoryDesc") %>'></asp:TextBox>
                </div>
            </ItemTemplate>
        </asp:FormView>
        <div id="cart">
            <asp:Button classp="adRight" ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" OnClientClick="return confirmAction('Do you want to add this category');" />
            <asp:Button class="padRight" ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" OnClientClick="return confirmAction('Do you want to update this category');" />
            <asp:Button ID="btnRemove" runat="server" Text="Remove" OnClick="btnRemove_Click" OnClientClick="return confirmAction('Do you want to delete this category');" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
        </div>
    </div>

</asp:Content>
