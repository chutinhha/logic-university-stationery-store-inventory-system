<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PlacePurchaseOrderSpecial.aspx.cs" Inherits="SA33.Team12.SSIS.Stock.PlacePurchaseOrderSpecial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 129px;
        }
        .style5
        {
            width: 175px;
        }
        .style7
        {
            width: 396px;
        }
        .style8
        {
            width: 324px;
        }
        .style10
        {
            width: 141px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1> Special Stationery Purchase Order</h1>
    <fieldset>

    <legend> Information </legend>
    <table style="width: 100%;">
        <tr>
            <td class="style1">
                Created By:
            </td>
            <td class="style5">
                <asp:Label ID="lblCreatedBy" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="style10">
                Attention To:</td>
            <td>
                <asp:DropDownList ID="ddlAttentionTo" runat="server" 
                    DataSourceID="UserDataSource" DataTextField="UserName" DataValueField="UserID">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="UserDataSource" runat="server" 
                    SelectMethod="GetAllUsers" TypeName="SA33.Team12.SSIS.BLL.UserManager">
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Date:</td>
            <td class="style5">
                <asp:Label ID="lblDate" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="style10">
                Supply By</td>
            <td>
                <asp:TextBox ID="txtDateToSupply" runat="server"></asp:TextBox>
                 <script type="text/javascript">
                     $(function () {
                         $('#<%= this.txtDateToSupply.ClientID %>').datepicker({
                             showOn: 'button',
                             dateFormat: 'mm/dd/yy',
                             buttonImage: '/Styles/jqui/images/calendar.gif',
                             buttonImageOnly: true,
                             setDate: '<%= DateTime.Now%>',
                             onSelect: function () { },
                             minDate: '-0d',
                             showButtonPanel: false,
                             changeMonth: true,
                             changeYear: true,
                             yearRange: '<%= DateTime.Now.Year-10 %>:<%= DateTime.Now.Year %>'
                         });
                     });
				</script>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtDateToSupply" ErrorMessage="Supply Date is required." 
                    ValidationGroup="input"></asp:RequiredFieldValidator>
</td>
        </tr>
    </table>
    </fieldset>
    
    <fieldset>
    <legend>Items To Order</legend>
        <asp:GridView ID="gvPOItems" runat="server" AutoGenerateColumns="False" 
            onrowdatabound="gvPOItems_RowDataBound" DataKeyNames="SpecialStationeryID">
            <Columns>
                <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:TemplateField HeaderText="Requested Quantity">
                    <ItemTemplate>
                        <asp:Literal ID="ltlQuantity" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Order Quantity">
                    <ItemTemplate>
                        <asp:TextBox ID="txtOrderQuantity" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Supplier">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlSupplier" runat="server" 
                            DataSourceID="SupplierDataSource" DataTextField="CompanyName" 
                            DataValueField="SupplierID">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="SupplierDataSource" runat="server" 
                            SelectMethod="GetAllSuppliers" TypeName="SA33.Team12.SSIS.BLL.CatalogManager">
                        </asp:ObjectDataSource>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </fieldset>
    
    <table style="width: 100%;">
        <tr>
            <td class="style8">
                &nbsp;</td>
            <td class="style7">
                &nbsp;</td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" 
                    Text="Submit" Height="44px" Width="98px" ValidationGroup="input" />
            </td>
        </tr>
        </table>
</asp:Content>
