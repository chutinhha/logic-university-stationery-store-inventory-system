<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Distribution.aspx.cs" Inherits="SA33.Team12.SSIS.Distribution.Distribution" %>
<%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Distribution</h2>
<fieldset>
    <legend>Distribution by item</legend>

    <asp:GridView runat="server" ID="DistributionGridView" 
        AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Employee">
                <ItemTemplate>
                    <%# ((User) Eval("Employee")).UserName %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Requisition No." 
                SortExpression="RequisitionID">
                <ItemTemplate>
                    <%# ((Requisition) Eval("Requisition")).RequisitionForm %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="DateRequested" HeaderText="DateRequested" 
                SortExpression="DateRequested" DataFormatString="{0:dd/MMM/yyyy}" />
            <asp:BoundField DataField="StationeryRetrievalFormItemByDeptID" 
                HeaderText="StationeryRetrievalFormItemByDeptID" Visible="false"
                SortExpression="StationeryRetrievalFormItemByDeptID" />
            <asp:TemplateField HeaderText="Department" Visible="false" 
                SortExpression="DepartmentID">
                <ItemTemplate>
                    <%# ((Department) Eval("Department")).Name %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="StationeryID" 
                SortExpression="StationeryID">
                <ItemTemplate>
                    <%# Convert.ToInt32(Eval("StationeryID")) == 0
                                                ? ((SpecialStationery)Eval("SpecialStationery")).ItemCode + ", " + ((SpecialStationery)Eval("SpecialStationery")).Description
                                                    : ((Stationery)Eval("Stationery")).ItemCode + ", " + ((Stationery)Eval("Stationery")).Description%>
                </ItemTemplate>    
            </asp:TemplateField>
            <asp:CheckBoxField DataField="IsSpecial" HeaderText="Is Special" />
            <asp:BoundField DataField="QuantityActual" HeaderText="QuantityActual" 
                SortExpression="QuantityActual" />
            <asp:BoundField DataField="QuantityDisbursed" HeaderText="QuantityDisbursed" 
                SortExpression="QuantityDisbursed" />
            <asp:TemplateField HeaderText="Quantity">
                <ItemTemplate>
                    <asp:TextBox runat="server" ID="QuantityTextBox" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="DisbursementID" HeaderText="DisbursementID" 
                SortExpression="DisbursementID" Visible="false" />
        </Columns>
    </asp:GridView>

    <asp:Button runat="server" Text="Update" ID="UpdateButton" />
    <asp:Button runat="server" Text="Back" ID="BackButton" />


</fieldset>

</asp:Content>
