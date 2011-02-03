<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestStationeryPrices.aspx.cs" Inherits="SA33.Team12.SSIS.Test.TestStationeryPrices" %>
<%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Stationery Prices</h2>

<fieldset>
<legend>Stationeries</legend>

    <asp:GridView runat="server" ID="StationeriesGridView" 
        AutoGenerateColumns="False" DataKeyNames="StationeryID, LocationID" 
        onrowdatabound="StationeriesGridView_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Location" 
                SortExpression="LocationID">
                <ItemTemplate>
                    <%# ((Location) Eval("Location")).Name %>
                </ItemTemplate>
                </asp:TemplateField>
            <asp:BoundField DataField="ItemCode" HeaderText="ItemCode" 
                SortExpression="ItemCode" />
            <asp:BoundField DataField="Description" HeaderText="Description" 
                SortExpression="Description" />
            <asp:BoundField DataField="ReorderLevel" HeaderText="ReorderLevel" 
                SortExpression="ReorderLevel" />
            <asp:BoundField DataField="ReorderQuantity" HeaderText="ReorderQuantity" 
                SortExpression="ReorderQuantity" />
            <asp:BoundField DataField="QuantityInHand" HeaderText="QuantityInHand" 
                SortExpression="QuantityInHand" />
            <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" 
                SortExpression="DateCreated" />
            <asp:BoundField DataField="DateModified" HeaderText="DateModified" 
                SortExpression="DateModified" />
            <asp:TemplateField HeaderText="ApprovedBy" 
                SortExpression="ApprovedBy">
                <ItemTemplate>
                    <%# DBNull.Value.Equals(Eval("ApprovedByUser"))? 
                    ((User) Eval("ApprovedByUser")).UserName : ""
                    %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField DataField="IsApproved" HeaderText="IsApproved" 
                SortExpression="IsApproved" />
            <asp:BoundField DataField="UnitOfMeasure" HeaderText="UnitOfMeasure" 
                SortExpression="UnitOfMeasure" />
            <asp:TemplateField HeaderText="Suppliers">
                <ItemTemplate>
                    <asp:DropDownList runat="server" ID="SupplierDrowDownList"
                        DataTextField="CompanyName" DataValueField="SupplierID">
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</fieldset>
</asp:Content>
