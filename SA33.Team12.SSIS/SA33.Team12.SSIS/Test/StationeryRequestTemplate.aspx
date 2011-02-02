<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StationeryRequestTemplate.aspx.cs" Inherits="SA33.Team12.SSIS.Test.StationeryRequestTemplate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content> 
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Stationery Request Form</h2>
<br />
    <asp:DetailsView ID="RequestDetailsView" runat="server" Height="50px" 
        Width="282px" AutoGenerateRows="False" DataSourceID="RequisitionDetailsDS">
        <Fields>
            <asp:BoundField HeaderText="Requisition Form Number" DataField="RequisitionForm" />
            <asp:BoundField HeaderText="Employee Name" DataField="CreatedBy" />
            <asp:BoundField HeaderText="Department" DataField="DepartmentID" />
            <asp:BoundField HeaderText="Urgency" DataField="UrgencyID" />
            <asp:BoundField HeaderText="Date Requested" DataField="DateRequested" />
            <asp:BoundField HeaderText="Approved By" DataField="ApprovedBy" />
            <asp:BoundField HeaderText="Date Approved" DataField="DateApproved" />
        </Fields>
    </asp:DetailsView>
    <asp:ObjectDataSource ID="RequisitionDetailsDS" runat="server" 
        SelectMethod="GetRequisitionByID" 
        TypeName="SA33.Team12.SSIS.BLL.RequisitionManager">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="RequisitionID" 
                QueryStringField="RequestID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <h2>Items</h2>
    <asp:GridView ID="RequestItemGridView" runat="server" 
        AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField HeaderText="StationeryID" DataField="StationeryID" />
            <asp:BoundField HeaderText="ItemCode" DataField="StationeryID" />
            <asp:BoundField HeaderText="Description" DataField="StationeryID" />
            <asp:BoundField HeaderText="Quantity Needed" DataField="QuantityRequested" />
        </Columns>
    </asp:GridView>
    <h2>Special Items</h2>
    <asp:GridView ID="SpecialRequestItemGridView" runat="server" 
        AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField HeaderText="SpecialStationeryID" 
                DataField="SpecialStationeryID" />
            <asp:BoundField HeaderText="Item Name" DataField="SpecialStationeryID" />
            <asp:BoundField HeaderText="Description" DataField="SpecialStationeryID" />
            <asp:BoundField HeaderText="Reason" DataField="RemarkByRequester" >
            <ItemStyle Width="200px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Quantity Needed" DataField="QuantityRequested" />
        </Columns>
    </asp:GridView>
</asp:Content>
