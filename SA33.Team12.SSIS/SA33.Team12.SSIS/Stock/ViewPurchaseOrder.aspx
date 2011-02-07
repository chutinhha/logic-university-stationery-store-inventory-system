<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ViewPurchaseOrder.aspx.cs" Inherits="SA33.Team12.SSIS.Stock.ViewPurchaseOrder" %>

<%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 112px;
        }
        .style2
        {
            width: 276px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        View Purchase Order</h1>
    <fieldset>
        <legend>Filter</legend>
        <table style="width: 100%;">
            <tr>
                <td class="style1">
                    Supplier
                </td>
                <td class="style2">
                    <asp:DropDownList ID="ddlSupplier" runat="server" DataSourceID="ObjectDataSource1"
                        DataTextField="CompanyName" DataValueField="SupplierID">
                        <asp:ListItem Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAllSuppliers"
                        TypeName="SA33.Team12.SSIS.BLL.CatalogManager"></asp:ObjectDataSource>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Order Date
                </td>
                <td class="style2">
                    From
                    <asp:TextBox ID="txtStartDateOfOrder" runat="server"></asp:TextBox>
                      <script type="text/javascript">
                          $(function () {
                              $('#<%= this.txtStartDateOfOrder.ClientID %>').datepicker({
                                  showOn: 'button',
                                  dateFormat: 'dd/mm/yy',
                                  buttonImage: '/Styles/jqui/images/calendar.gif',
                                  buttonImageOnly: true,
                                  setDate: '2/7/2011 2:11:32 PM',
                                  onSelect: function () { },
                                  maxDate: '+0d',
                                  showButtonPanel: false,
                                  changeMonth: true,
                                  changeYear: true,
                                  yearRange: '<%= DateTime.Now.Year-10 %>:<%= DateTime.Now.Year %>'
                              });
                          });
				</script>
                </td>
                <td>
                    To
                    <asp:TextBox ID="txtEndDateOfOrder" runat="server"></asp:TextBox>
                    <script type="text/javascript">
                        $(function () {
                            $('#<%= this.txtEndDateOfOrder.ClientID %>').datepicker({
                                showOn: 'button',
                                dateFormat: 'dd/mm/yy',
                                buttonImage: '/Styles/jqui/images/calendar.gif',
                                buttonImageOnly: true,
                                setDate: '2/7/2011 2:11:32 PM',
                                onSelect: function () { },
                                maxDate: '+0d',
                                showButtonPanel: false,
                                changeMonth: true,
                                changeYear: true,
                                yearRange: '<%= DateTime.Now.Year-10 %>:<%= DateTime.Now.Year %>'
                            });
                        });
				</script>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    PO Number</td>
                <td class="style2">
                    <asp:TextBox ID="txtPONumber" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnView" runat="server" OnClick="Button1_Click" Text="View" />
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend>Purchase Orders</legend>
        <asp:GridView ID="gvPurchaseOrder" runat="server" AutoGenerateColumns="False" 
        >
            <Columns>
                <asp:BoundField DataField="PurchaseOrderID" HeaderText="PO ID" 
                    SortExpression="PurchaseOrderID" />
                <asp:TemplateField HeaderText="PO Number">
                    <ItemTemplate>
                        <a href='PurchaseOrderDetail.aspx?ID=<%# Eval("PurchaseOrderID") %>'>
                            <%# Eval("PONumber")%></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="DateOfOrder" HeaderText="Order Date" SortExpression="DateOfOrder" />
                <asp:BoundField DataField="DateToSupply" HeaderText="Supply By" SortExpression="DateToSupply" />
                <asp:TemplateField HeaderText="Supplier" SortExpression="SupplierID">
                    <ItemTemplate>
                        <%# ((Supplier) Eval("Supplier")).CompanyName %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="IsDelivered" HeaderText="Delivered" SortExpression="IsDelivered" />
            </Columns>
        </asp:GridView>
    </fieldset>
</asp:Content>
