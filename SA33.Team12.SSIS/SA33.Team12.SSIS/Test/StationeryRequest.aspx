<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StationeryRequest.aspx.cs" Inherits="SA33.Team12.SSIS.Test.StationeryRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ObjectDataSource ID="RequistionDataSource" runat="server" 
        DataObjectTypeName="SA33.Team12.SSIS.DAL.Requisition" 
        InsertMethod="CreateRequisition" SelectMethod="FindAllRequisition" 
        TypeName="SA33.Team12.SSIS.BLL.RequisitionManager" 
        UpdateMethod="UpdateRequisition"></asp:ObjectDataSource>
    <br />
    <asp:ListView ID="ListView1" runat="server" DataSourceID="RequistionDataSource" 
        InsertItemPosition="LastItem">
        <AlternatingItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="RequisitionID" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="CreatedBy" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="DepartmentID" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="ApprovedBy" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="RequisitionForm" 
                        Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="StatusID" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="UrgencyID" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="DateRequested" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="DateApproved" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Department" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="DepartmentReference" 
                        Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="RequisitionItems" 
                        Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="ApprovedByUser" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="ApprovedByUserReference" 
                        Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="CreatedByUser" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="CreatedByUserReference" 
                        Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Status" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="StatusReference" 
                        Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" 
                        DataField="StationeryRetrievalFormByRequisitions" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="SpecialRequisitionItems" 
                        Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Urgency" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="UrgencyReference" 
                        Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="EntityState" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="EntityKey" Mode="ReadOnly" />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                        Text="Update" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                        Text="Cancel" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="RequisitionID" Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="CreatedBy" Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="DepartmentID" Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="ApprovedBy" Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="RequisitionForm" Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="StatusID" Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="UrgencyID" Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="DateRequested" Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="DateApproved" Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Department" Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="DepartmentReference" 
                        Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="RequisitionItems" Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="ApprovedByUser" Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="ApprovedByUserReference" 
                        Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="CreatedByUser" Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="CreatedByUserReference" 
                        Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Status" Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="StatusReference" Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" 
                        DataField="StationeryRetrievalFormByRequisitions" Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="SpecialRequisitionItems" 
                        Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Urgency" Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="UrgencyReference" Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="EntityState" Mode="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="EntityKey" Mode="Edit" />
                </td>
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="">
                <tr>
                    <td>
                        No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" 
                        ValidationGroup="Insert" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                        Text="Clear" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="RequisitionID" Mode="Insert" 
                        ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="CreatedBy" Mode="Insert" 
                        ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="DepartmentID" Mode="Insert" 
                        ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="ApprovedBy" Mode="Insert" 
                        ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="RequisitionForm" Mode="Insert" 
                        ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="StatusID" Mode="Insert" 
                        ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="UrgencyID" Mode="Insert" 
                        ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="DateRequested" Mode="Insert" 
                        ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="DateApproved" Mode="Insert" 
                        ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Department" Mode="Insert" 
                        ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="DepartmentReference" 
                        Mode="Insert" ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="RequisitionItems" Mode="Insert" 
                        ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="ApprovedByUser" Mode="Insert" 
                        ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="ApprovedByUserReference" 
                        Mode="Insert" ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="CreatedByUser" Mode="Insert" 
                        ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="CreatedByUserReference" 
                        Mode="Insert" ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Status" Mode="Insert" 
                        ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="StatusReference" Mode="Insert" 
                        ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" 
                        DataField="StationeryRetrievalFormByRequisitions" Mode="Insert" 
                        ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="SpecialRequisitionItems" 
                        Mode="Insert" ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Urgency" Mode="Insert" 
                        ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="UrgencyReference" Mode="Insert" 
                        ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="EntityState" Mode="Insert" 
                        ValidationGroup="Insert" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="EntityKey" Mode="Insert" 
                        ValidationGroup="Insert" />
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="RequisitionID" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="CreatedBy" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="DepartmentID" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="ApprovedBy" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="RequisitionForm" 
                        Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="StatusID" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="UrgencyID" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="DateRequested" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="DateApproved" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Department" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="DepartmentReference" 
                        Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="RequisitionItems" 
                        Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="ApprovedByUser" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="ApprovedByUserReference" 
                        Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="CreatedByUser" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="CreatedByUserReference" 
                        Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Status" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="StatusReference" 
                        Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" 
                        DataField="StationeryRetrievalFormByRequisitions" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="SpecialRequisitionItems" 
                        Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Urgency" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="UrgencyReference" 
                        Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="EntityState" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="EntityKey" Mode="ReadOnly" />
                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table ID="itemPlaceholderContainer" runat="server" border="0" style="">
                            <tr runat="server" style="">
                                <th runat="server">
                                </th>
                                <th runat="server">
                                    RequisitionID</th>
                                <th runat="server">
                                    CreatedBy</th>
                                <th runat="server">
                                    DepartmentID</th>
                                <th runat="server">
                                    ApprovedBy</th>
                                <th runat="server">
                                    RequisitionForm</th>
                                <th runat="server">
                                    StatusID</th>
                                <th runat="server">
                                    UrgencyID</th>
                                <th runat="server">
                                    DateRequested</th>
                                <th runat="server">
                                    DateApproved</th>
                                <th runat="server">
                                    Department</th>
                                <th runat="server">
                                    DepartmentReference</th>
                                <th runat="server">
                                    RequisitionItems</th>
                                <th runat="server">
                                    ApprovedByUser</th>
                                <th runat="server">
                                    ApprovedByUserReference</th>
                                <th runat="server">
                                    CreatedByUser</th>
                                <th runat="server">
                                    CreatedByUserReference</th>
                                <th runat="server">
                                    Status</th>
                                <th runat="server">
                                    StatusReference</th>
                                <th runat="server">
                                    StationeryRetrievalFormByRequisitions</th>
                                <th runat="server">
                                    SpecialRequisitionItems</th>
                                <th runat="server">
                                    Urgency</th>
                                <th runat="server">
                                    UrgencyReference</th>
                                <th runat="server">
                                    EntityState</th>
                                <th runat="server">
                                    EntityKey</th>
                            </tr>
                            <tr ID="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="">
                        <asp:DataPager ID="DataPager1" runat="server">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                                    ShowLastPageButton="True" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="RequisitionID" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="CreatedBy" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="DepartmentID" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="ApprovedBy" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="RequisitionForm" 
                        Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="StatusID" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="UrgencyID" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="DateRequested" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="DateApproved" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Department" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="DepartmentReference" 
                        Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="RequisitionItems" 
                        Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="ApprovedByUser" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="ApprovedByUserReference" 
                        Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="CreatedByUser" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="CreatedByUserReference" 
                        Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Status" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="StatusReference" 
                        Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" 
                        DataField="StationeryRetrievalFormByRequisitions" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="SpecialRequisitionItems" 
                        Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="Urgency" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="UrgencyReference" 
                        Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="EntityState" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="EntityKey" Mode="ReadOnly" />
                </td>
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>
</asp:Content>
