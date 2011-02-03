﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StationeryRequestTemplate.aspx.cs" Inherits="SA33.Team12.SSIS.Test.StationeryRequestTemplate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content> 
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Stationery Request Form</h2>
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
    <asp:Panel ID="Panel1" runat="server">
        <h2>
            Add Items</h2>
        <asp:DetailsView ID="DetailsView1" runat="server" 
        AutoGenerateInsertButton="True" AutoGenerateRows="False" DefaultMode="Insert"
        Height="50px" oniteminserting="DetailsView1_ItemInserting" Width="125px" 
        EnableViewState="False">
            <Fields>
                <asp:TemplateField HeaderText="Stationery">
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList2" runat="server" 
                        DataSourceID="StationeryItemDS0" DataTextField="Description" 
                        DataValueField="StationeryID">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="StationeryItemDS" runat="server" 
                        SelectMethod="GetAllStationeries" 
                        TypeName="SA33.Team12.SSIS.BLL.CatalogManager"></asp:ObjectDataSource>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:DropDownList ID="DropDownList3" runat="server" 
                        DataSourceID="StationeryItemDS0" DataTextField="Description" 
                        DataValueField="StationeryID" SelectedValue='<%# Bind("StationeryID") %>'>
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="StationeryItemDS0" runat="server" 
                        SelectMethod="GetAllStationeries" 
                        TypeName="SA33.Team12.SSIS.BLL.CatalogManager"></asp:ObjectDataSource>
                    </InsertItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server" ValidationGroup="QuantityNeeded"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidator1" runat="server" 
                            ControlToValidate="TextBox5" Display="Dynamic" ErrorMessage="Invalid Quantity" 
                            MaximumValue="10000" MinimumValue="1" Type="Integer" 
                            ValidationGroup="QuantityNeeded"></asp:RangeValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="TextBox5" Display="Dynamic" 
                            ErrorMessage="Quantity is needed" ValidationGroup="QuantityNeeded"></asp:RequiredFieldValidator>
                    </InsertItemTemplate>
                </asp:TemplateField>
            </Fields>
        </asp:DetailsView>
    </asp:Panel>
    <br />
    <h2>Items</h2>
    <asp:GridView ID="RequestItemGridView" runat="server" 
        AutoGenerateColumns="False" AutoGenerateDeleteButton="True" 
        AutoGenerateEditButton="True" EnableViewState="False" 
        onrowcancelingedit="RequestItemGridView_RowCancelingEdit" 
        onrowdeleting="RequestItemGridView_RowDeleting" 
        onrowediting="RequestItemGridView_RowEditing" 
        onrowupdating="RequestItemGridView_RowUpdating" 
        DataKeyNames="StationeryID">
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
    <asp:Panel ID="Panel2" runat="server">
        <h2>
            ADD Special items</h2>
        <p>
            <asp:DetailsView ID="DetailsView2" runat="server" 
                AutoGenerateInsertButton="True" AutoGenerateRows="False" DefaultMode="Insert" 
                Height="50px" oniteminserting="DetailsView2_ItemInserting" Width="125px">
                <Fields>
                    <asp:TemplateField HeaderText="Item Name">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description">
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
                    <asp:TemplateField HeaderText="Quantity Needed">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reason">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                        </InsertItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit Of Measure">
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                        </InsertItemTemplate>
                    </asp:TemplateField>
                </Fields>
            </asp:DetailsView>
        </p>
    </asp:Panel>
    <h2>Special Items</h2>
    <asp:GridView ID="SpecialRequestItemGridView" runat="server" 
        AutoGenerateColumns="False" style="margin-right: 0px">
        <Columns>
            <asp:TemplateField HeaderText="Item Name">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" 
                        Text='<%# Bind("SpecialStationeryID") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" 
                        Text='<%# Bind("SpecialStationeryID") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Reason">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("RemarkByRequester") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" 
                        Text='<%# Bind("RemarkByRequester") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity Needed">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("QuantityRequested") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" 
                        Text='<%# Bind("QuantityRequested") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    <asp:Button ID="SubmitButton" runat="server" onclick="SubmitButton_Click" 
        Text="Submit Request" />
    <br />
</asp:Content>
