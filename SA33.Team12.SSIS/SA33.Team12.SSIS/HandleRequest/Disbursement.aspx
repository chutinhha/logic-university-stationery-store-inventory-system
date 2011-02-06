<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Disbursement.aspx.cs" Inherits="SA33.Team12.SSIS.Handle_Request.Disbursement" %>
<%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<fieldset>
<legend>Stationery Retrieval Form List</legend>
Filter By Disbursement Status:
    <asp:DropDownList ID="DDLIsDisbursed" runat="server">
        <asp:ListItem Value="true">Already Disbursed</asp:ListItem>
        <asp:ListItem Value="false">Never Disbursed</asp:ListItem>
    </asp:DropDownList>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="BtnFilter" runat="server" Text="Filter" 
        onclick="BtnFilter_Click" />
    <asp:GridView ID="SRFGridView" runat="server" AutoGenerateColumns="False"
         AllowPaging="True" onpageindexchanging="SRFGridView_PageIndexChanging" 
        onrowdatabound="SRFGridView_RowDataBound" 
        onrowcommand="SRFGridView_RowCommand">
        <Columns>
            <asp:BoundField DataField="StationeryRetrievalFormID" HeaderText="SRFID" 
                SortExpression="StationeryRetrievalFormID" />
            <asp:BoundField DataField="StationeryRetrievalNumber" 
                HeaderText="StationeryRetrievedNumber" 
                SortExpression="StationeryRetrievalNumber" />
            <asp:BoundField DataField="DateRetrieved" HeaderText="DateRetrieved" 
                SortExpression="DateRetrieved" />
            <asp:TemplateField HeaderText="RetrievedBy">
                 <ItemTemplate>
                     <asp:Literal runat="server" ID="RetrievedByLiteral" />
                 </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="IsDistributed" HeaderText="IsDisbursed" 
                SortExpression="IsDistributed" />
            <asp:CommandField ShowSelectButton="True" SelectText="Disburse" />
        </Columns>
    </asp:GridView>
    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
</fieldset>

<fieldset>
<legend>Disbursement List</legend>
<asp:GridView ID="DisbursementGridView" runat="server" 
               AutoGenerateColumns="False" 
       AllowPaging="True" 
        onpageindexchanging="DisbursementGridView_PageIndexChanging" 
        DataKeyNames="DisbursementID" onrowcommand="DisbursementGridView_RowCommand" onrowdatabound="DisbursementGridView_RowDataBound" 
     >
    <Columns>
        <asp:BoundField DataField="DisbursementID" HeaderText="DisbursementID" 
            SortExpression="DisbursementID" />
        <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" 
            SortExpression="DateCreated" />
        <asp:TemplateField HeaderText="CreatedBy">
             <ItemTemplate>
                 <asp:Literal runat="server" ID="CreatedByLiteral" />
             </ItemTemplate>
        </asp:TemplateField>
        <asp:CommandField ShowSelectButton="True" />
    </Columns>
    </asp:GridView>
</fieldset>

<fieldset>
<legend>Disbursement Items</legend>
    <asp:GridView ID="DisbursementItemGridView" runat="server" 
        AutoGenerateColumns="False" DataKeyNames="DisbursementItemID" 
        onrowediting="DisbursementItemGridView_RowEditing" 
        onrowupdating="DisbursementItemGridView_RowUpdating" 
        AutoGenerateEditButton="True" 
        onrowcancelingedit="DisbursementItemGridView_RowCancelingEdit" 
        EnableViewState="False" 
        onrowdatabound="DisbursementItemGridView_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="ItemID">
                <ItemTemplate>
                    <asp:Label ID="ItemIDLabel" runat="server" Text='<%# Bind("DisbursementItemID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Stationery">
                <ItemTemplate>
                    <asp:Literal ID="StationeryIDLiteral" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SpecialStationery">
                <ItemTemplate>
                    <asp:Literal ID="SpecialStationeryIDLiteral" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="QuantityDamaged">
                <ItemTemplate>
                    <asp:Label ID="QuantityDamagedLabel" runat="server" Text='<%# Bind("QuantityDamaged") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="QuantityDisbursed">
                <ItemTemplate>
                    <asp:Label ID="QuantityDisbursedLabel" runat="server" Text='<%# Bind("QuantityDisbursed") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="QuantityDisbursedtxb" runat="server" Text='<%# Bind("QuantityDisbursed") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
      <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
      Update Disbursed Quantity:<asp:TextBox ID="tbxQuantity" runat="server"></asp:TextBox>
    <asp:Button ID="BtnUpdateQuantity" runat="server" Text="Update" 
        onclick="BtnUpdateQuantity_Click" style="height: 26px" />
</fieldset>
</asp:Content>
