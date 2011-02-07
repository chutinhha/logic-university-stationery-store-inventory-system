<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Disbursement.aspx.cs" Inherits="SA33.Team12.SSIS.RequestStationery.Disbursement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<fieldset>
    <legend> <h2>Disbursement</h2></legend>

    <asp:FormView runat="server" ID="DisbursementFormView" 
        DataSourceID="DisbursementObjectDataSource">
        <EditItemTemplate>
            DisbursementID:
            <asp:DynamicControl ID="DisbursementIDDynamicControl" runat="server" 
                DataField="DisbursementID" Mode="Edit" />
            <br />
            DateCreated:
            <asp:DynamicControl ID="DateCreatedDynamicControl" runat="server" 
                DataField="DateCreated" Mode="Edit" />
            <br />
            CreatedBy:
            <asp:DynamicControl ID="CreatedByDynamicControl" runat="server" 
                DataField="CreatedBy" Mode="Edit" />
            <br />
            StationeryRetrievalFormID:
            <asp:DynamicControl ID="StationeryRetrievalFormIDDynamicControl" runat="server" 
                DataField="StationeryRetrievalFormID" Mode="Edit" />
            <br />
            DepartmentID:
            <asp:DynamicControl ID="DepartmentIDDynamicControl" runat="server" 
                DataField="DepartmentID" Mode="Edit" />
            <br />
            DisbursementItems:
            <asp:DynamicControl ID="DisbursementItemsDynamicControl" runat="server" 
                DataField="DisbursementItems" Mode="Edit" />
            <br />
            User:
            <asp:DynamicControl ID="UserDynamicControl" runat="server" DataField="User" 
                Mode="Edit" />
            <br />
            UserReference:
            <asp:DynamicControl ID="UserReferenceDynamicControl" runat="server" 
                DataField="UserReference" Mode="Edit" />
            <br />
            StationeryRetrievalForm:
            <asp:DynamicControl ID="StationeryRetrievalFormDynamicControl" runat="server" 
                DataField="StationeryRetrievalForm" Mode="Edit" />
            <br />
            StationeryRetrievalFormReference:
            <asp:DynamicControl ID="StationeryRetrievalFormReferenceDynamicControl" 
                runat="server" DataField="StationeryRetrievalFormReference" Mode="Edit" />
            <br />
            Department:
            <asp:DynamicControl ID="DepartmentDynamicControl" runat="server" 
                DataField="Department" Mode="Edit" />
            <br />
            DepartmentReference:
            <asp:DynamicControl ID="DepartmentReferenceDynamicControl" runat="server" 
                DataField="DepartmentReference" Mode="Edit" />
            <br />
            EntityState:
            <asp:DynamicControl ID="EntityStateDynamicControl" runat="server" 
                DataField="EntityState" Mode="Edit" />
            <br />
            EntityKey:
            <asp:DynamicControl ID="EntityKeyDynamicControl" runat="server" 
                DataField="EntityKey" Mode="Edit" />
            <br />
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                CommandName="Update" Text="Update" ValidationGroup="Insert" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </EditItemTemplate>
        <InsertItemTemplate>
            DisbursementID:
            <asp:DynamicControl ID="DisbursementIDDynamicControl" runat="server" 
                DataField="DisbursementID" Mode="Insert" ValidationGroup="Insert" />
            <br />
            DateCreated:
            <asp:DynamicControl ID="DateCreatedDynamicControl" runat="server" 
                DataField="DateCreated" Mode="Insert" ValidationGroup="Insert" />
            <br />
            CreatedBy:
            <asp:DynamicControl ID="CreatedByDynamicControl" runat="server" 
                DataField="CreatedBy" Mode="Insert" ValidationGroup="Insert" />
            <br />
            StationeryRetrievalFormID:
            <asp:DynamicControl ID="StationeryRetrievalFormIDDynamicControl" runat="server" 
                DataField="StationeryRetrievalFormID" Mode="Insert" ValidationGroup="Insert" />
            <br />
            DepartmentID:
            <asp:DynamicControl ID="DepartmentIDDynamicControl" runat="server" 
                DataField="DepartmentID" Mode="Insert" ValidationGroup="Insert" />
            <br />
            DisbursementItems:
            <asp:DynamicControl ID="DisbursementItemsDynamicControl" runat="server" 
                DataField="DisbursementItems" Mode="Insert" ValidationGroup="Insert" />
            <br />
            User:
            <asp:DynamicControl ID="UserDynamicControl" runat="server" DataField="User" 
                Mode="Insert" ValidationGroup="Insert" />
            <br />
            UserReference:
            <asp:DynamicControl ID="UserReferenceDynamicControl" runat="server" 
                DataField="UserReference" Mode="Insert" ValidationGroup="Insert" />
            <br />
            StationeryRetrievalForm:
            <asp:DynamicControl ID="StationeryRetrievalFormDynamicControl" runat="server" 
                DataField="StationeryRetrievalForm" Mode="Insert" ValidationGroup="Insert" />
            <br />
            StationeryRetrievalFormReference:
            <asp:DynamicControl ID="StationeryRetrievalFormReferenceDynamicControl" 
                runat="server" DataField="StationeryRetrievalFormReference" Mode="Insert" 
                ValidationGroup="Insert" />
            <br />
            Department:
            <asp:DynamicControl ID="DepartmentDynamicControl" runat="server" 
                DataField="Department" Mode="Insert" ValidationGroup="Insert" />
            <br />
            DepartmentReference:
            <asp:DynamicControl ID="DepartmentReferenceDynamicControl" runat="server" 
                DataField="DepartmentReference" Mode="Insert" ValidationGroup="Insert" />
            <br />
            EntityState:
            <asp:DynamicControl ID="EntityStateDynamicControl" runat="server" 
                DataField="EntityState" Mode="Insert" ValidationGroup="Insert" />
            <br />
            EntityKey:
            <asp:DynamicControl ID="EntityKeyDynamicControl" runat="server" 
                DataField="EntityKey" Mode="Insert" ValidationGroup="Insert" />
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                CommandName="Insert" Text="Insert" ValidationGroup="Insert" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
        <ItemTemplate>
            <table>
                <tr class="odd">
                    <th>DisbursementID: </th>
                    <td>
                        <asp:DynamicControl ID="DisbursementIDDynamicControl" runat="server" 
                            DataField="DisbursementID" Mode="ReadOnly" />
                    </td>
                </tr>
                <tr>
                    <th>DateCreated: </th>
                    <td>
                        <asp:DynamicControl ID="DateCreatedDynamicControl" runat="server" 
                            DataField="DateCreated" Mode="ReadOnly" />
                    </td>
                </tr>
                <tr>
                    <th>
                        CreatedBy:
                    </th>
                    <td>
                        <asp:DynamicControl ID="CreatedByDynamicControl" runat="server" 
                            DataField="CreatedBy" Mode="ReadOnly" />
                    </td>
                </tr>
                <tr>
                    <th>
                        StationeryRetrievalFormID:
                    </th>
                    <td>
                        <asp:DynamicControl ID="StationeryRetrievalFormIDDynamicControl" runat="server" 
                            DataField="StationeryRetrievalFormID" Mode="ReadOnly" />
                    </td>
                </tr>
                <tr>
                    <th>
                        DepartmentID:
                    </th>
                    <td>
                        <asp:DynamicControl ID="DepartmentIDDynamicControl" runat="server" 
                            DataField="DepartmentID" Mode="ReadOnly" />
                    </td>
                </tr>
                <tr>
                    <th>
                        DisbursementItems:
                    </th>
                    <td>
                        <asp:DynamicControl ID="DisbursementItemsDynamicControl" runat="server" 
                            DataField="DisbursementItems" Mode="ReadOnly" />
                    </td>
                </tr>
                <tr>
                    <th>
                        User:
                    </th>
                    <td>
                        <asp:DynamicControl ID="UserReferenceDynamicControl" runat="server" 
                            DataField="UserReference" Mode="ReadOnly" />
                    </td>
                </tr>
                <tr>
                    <th>
                        UserReference:
                    </th>
                    <td>
                        <asp:DynamicControl ID="UserDynamicControl" runat="server" DataField="User" 
                            Mode="ReadOnly" />
                    </td>
                </tr>
                <tr>
                    <th>
                        StationeryRetrievalForm:
                    </th>
                    <td>
                        <asp:DynamicControl ID="StationeryRetrievalFormDynamicControl" runat="server" 
                            DataField="StationeryRetrievalForm" Mode="ReadOnly" />
                    </td>
                </tr>
                <tr>
                    <th>
                        StationeryRetrievalFormReference:
                    </th>
                    <td>
                        <asp:DynamicControl ID="StationeryRetrievalFormReferenceDynamicControl" 
                            runat="server" DataField="StationeryRetrievalFormReference" Mode="ReadOnly" />
                    </td>
                </tr>
                <tr>
                    <th>
                        Department:
                    </th>
                    <td>
                        <asp:DynamicControl ID="DepartmentDynamicControl" runat="server" 
                            DataField="Department" Mode="ReadOnly" />
                    </td>
                </tr>
                <tr>
                    <th>
                        DepartmentReference:
                    </th>
                    <td>
                        <asp:DynamicControl ID="DepartmentReferenceDynamicControl" runat="server" 
                            DataField="DepartmentReference" Mode="ReadOnly" />
                    </td>
                </tr>
                <tr>
                    <th>
                        EntityState:
                    </th>
                    <td>
                        <asp:DynamicControl ID="EntityStateDynamicControl" runat="server" 
                            DataField="EntityState" Mode="ReadOnly" />
                    </td>
                </tr>
                <tr>
                    <th>
                        EntityKey:
                    </th>
                    <td>
                        <asp:DynamicControl ID="EntityKeyDynamicControl" runat="server" 
                            DataField="EntityKey" Mode="ReadOnly" />
                    </td>
                </tr>
            </table>
            <asp:DetailsView runat="server" ID="DisbursementDetailsView"
                DataSource='<%# Eval("DisbursementItems") %>'>
            </asp:DetailsView>
        </ItemTemplate>
    </asp:FormView>
    <asp:ObjectDataSource ID="DisbursementObjectDataSource" runat="server" 
        SelectMethod="FindAllDisbursement" 
        TypeName="SA33.Team12.SSIS.BLL.DisbursementManager">
    </asp:ObjectDataSource>
</fieldset>
   

</asp:Content>
