﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StationeryRequestTemplate.aspx.cs" Inherits="SA33.Team12.SSIS.Test.StationeryRequestTemplate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content> 
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Stationery Request Form</h2>
<br />
    <asp:DetailsView ID="RequestDetailsView" runat="server" Height="50px" 
        Width="282px" AutoGenerateRows="False" DataSourceID="RequisitionDetailsDS">
        <Fields>
            <asp:BoundField HeaderText="Requisition Form Number" DataField="RequisitionForm" />
            <asp:TemplateField HeaderText="Employee Name">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("CreatedBy") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CreatedBy") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CreatedBy") %>'></asp:TextBox>
                </InsertItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Department" DataField="DepartmentID" />
            <asp:BoundField HeaderText="Urgency" DataField="UrgencyID" />
            <asp:BoundField HeaderText="Date Requested" DataField="DateRequested" />
            <asp:BoundField HeaderText="Approved By" DataField="ApprovedBy" />
            <asp:BoundField HeaderText="Date Approved" DataField="DateApproved" />
        </Fields>
    </asp:DetailsView>
    &nbsp;<asp:ObjectDataSource ID="RequisitionDetailsDS" runat="server" 
        SelectMethod="GetRequisitionByID" 
        TypeName="SA33.Team12.SSIS.BLL.RequisitionManager">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="RequisitionID" 
                QueryStringField="RequestID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <br />
    <asp:DetailsView ID="DetailsView1" runat="server" 
        AutoGenerateInsertButton="True" AutoGenerateRows="False" DefaultMode="Insert"
        Height="50px" oniteminserting="DetailsView1_ItemInserting" Width="125px" 
        EnableViewState="False">
        <Fields>
            <asp:TemplateField HeaderText="Stationery">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList2" runat="server" 
                        DataSourceID="StationeryItemDS" DataTextField="Description" 
                        DataValueField="StationeryID">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="StationeryItemDS" runat="server" 
                        SelectMethod="GetAllStationeries" 
                        TypeName="SA33.Team12.SSIS.BLL.CatalogManager"></asp:ObjectDataSource>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:DropDownList ID="DropDownList3" runat="server" 
                        DataSourceID="StationeryItemDS" DataTextField="Description" 
                        DataValueField="StationeryID" SelectedValue='<%# Bind("StationeryID") %>'>
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="StationeryItemDS" runat="server" 
                        SelectMethod="GetAllStationeries" 
                        TypeName="SA33.Team12.SSIS.BLL.CatalogManager"></asp:ObjectDataSource>
                </InsertItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </InsertItemTemplate>
            </asp:TemplateField>
        </Fields>
    </asp:DetailsView>
    <br />
    <h2>Items</h2>
    <asp:GridView ID="RequestItemGridView" runat="server" 
        AutoGenerateColumns="False" AutoGenerateDeleteButton="True" 
        AutoGenerateEditButton="True" EnableViewState="False" 
        onrowcancelingedit="RequestItemGridView_RowCancelingEdit" 
        onrowdeleting="RequestItemGridView_RowDeleting" 
        onrowediting="RequestItemGridView_RowEditing" 
        onrowupdating="RequestItemGridView_RowUpdating">
        <Columns>
            <asp:TemplateField HeaderText="RequisitionItemID">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("RequisitionItemID") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" 
                        Text='<%# Bind("RequisitionItemID") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="StationeryID">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="StationeryDS" 
                        DataTextField="Description" DataValueField="StationeryID" 
                        SelectedValue='<%# Bind("StationeryID") %>'>
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="StationeryDS" runat="server" 
                        SelectMethod="GetAllStationeries" 
                        TypeName="SA33.Team12.SSIS.BLL.CatalogManager"></asp:ObjectDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("StationeryID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity Needed">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("QuantityRequested") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" 
                        Text='<%# Bind("QuantityRequested") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
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
