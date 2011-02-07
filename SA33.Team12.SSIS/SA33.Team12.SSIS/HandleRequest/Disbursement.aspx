<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Disbursement.aspx.cs" Inherits="SA33.Team12.SSIS.RequestStationery.Disbursement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<fieldset>
    <legend> <h2>Disbursement</h2></legend>

    <asp:FormView runat="server" ID="DisbursementFormView" 
        DataSourceID="DisbursementObjectDataSource">
        <EditItemTemplate>
            <tr><th>DisbursementID:</th>
            <td><asp:TextBox ID="DisbursementIDTextBox" runat="server" 
                Text='<%# Bind("DisbursementID") %>' /></td></tr>

            <br />
            <tr><th>DateCreated:</th>
            <td><asp:TextBox ID="DateCreatedTextBox" runat="server" 
                Text='<%# Bind("DateCreated") %>' /></td></tr>

            <br />
            <tr><th>CreatedBy:</th>
            <td><asp:TextBox ID="CreatedByTextBox" runat="server" 
                Text='<%# Bind("CreatedBy") %>' /></td></tr>

            <br />
            <tr><th>StationeryRetrievalFormID:</th>
            <td><asp:TextBox ID="StationeryRetrievalFormIDTextBox" runat="server" 
                Text='<%# Bind("StationeryRetrievalFormID") %>' /></td></tr>

            <br />
            <tr><th>DepartmentID:</th>
            <td><asp:TextBox ID="DepartmentIDTextBox" runat="server" 
                Text='<%# Bind("DepartmentID") %>' /></td></tr>

            <br />
            <tr><th>DisbursementItems:</th>
            <td><asp:TextBox ID="DisbursementItemsTextBox" runat="server" 
                Text='<%# Bind("DisbursementItems") %>' /></td></tr>

            <br />
            <tr><th>User:</th>
            <td><asp:TextBox ID="UserTextBox" runat="server" Text='<%# Bind("User") %>' /></td></tr>

            <br />
            <tr><th>UserReference:</th>
            <td><asp:TextBox ID="UserReferenceTextBox" runat="server" 
                Text='<%# Bind("UserReference") %>' /></td></tr>

            <br />
            <tr><th>StationeryRetrievalForm:</th>
            <td><asp:TextBox ID="StationeryRetrievalFormTextBox" runat="server" 
                Text='<%# Bind("StationeryRetrievalForm") %>' /></td></tr>

            <br />
            <tr><th>StationeryRetrievalFormReference:</th>
            <td><asp:TextBox ID="StationeryRetrievalFormReferenceTextBox" runat="server" 
                Text='<%# Bind("StationeryRetrievalFormReference") %>' /></td></tr>

            <br />
            <tr><th>Department:</th>
            <td><asp:TextBox ID="DepartmentTextBox" runat="server" 
                Text='<%# Bind("Department") %>' /></td></tr>

            <br />
            <tr><th>DepartmentReference:</th>
            <td><asp:TextBox ID="DepartmentReferenceTextBox" runat="server" 
                Text='<%# Bind("DepartmentReference") %>' /></td></tr>

            <br />
            <tr><th>EntityState:</th>
            <td><asp:TextBox ID="EntityStateTextBox" runat="server" 
                Text='<%# Bind("EntityState") %>' /></td></tr>

            <br />
            <tr><th>EntityKey:</th>
            <td><asp:TextBox ID="EntityKeyTextBox" runat="server" 
                Text='<%# Bind("EntityKey") %>' /></td></tr>

            <br />
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                CommandName="Update" Text="Update" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </EditItemTemplate>
        <InsertItemTemplate>
            <tr><th>DisbursementID:</th>
            <td><asp:TextBox ID="DisbursementIDTextBox" runat="server" 
                Text='<%# Bind("DisbursementID") %>' /></td></tr>

            <br />
            <tr><th>DateCreated:</th>
            <td><asp:TextBox ID="DateCreatedTextBox" runat="server" 
                Text='<%# Bind("DateCreated") %>' /></td></tr>

            <br />
            <tr><th>CreatedBy:</th>
            <td><asp:TextBox ID="CreatedByTextBox" runat="server" 
                Text='<%# Bind("CreatedBy") %>' /></td></tr>

            <br />
            <tr><th>StationeryRetrievalFormID:</th>
            <td><asp:TextBox ID="StationeryRetrievalFormIDTextBox" runat="server" 
                Text='<%# Bind("StationeryRetrievalFormID") %>' /></td></tr>

            <br />
            <tr><th>DepartmentID:</th>
            <td><asp:TextBox ID="DepartmentIDTextBox" runat="server" 
                Text='<%# Bind("DepartmentID") %>' /></td></tr>

            <br />
            <tr><th>DisbursementItems:</th>
            <td><asp:TextBox ID="DisbursementItemsTextBox" runat="server" 
                Text='<%# Bind("DisbursementItems") %>' /></td></tr>

            <br />
            <tr><th>User:</th>
            <td><asp:TextBox ID="UserTextBox" runat="server" Text='<%# Bind("User") %>' /></td></tr>

            <br />
            <tr><th>UserReference:</th>
            <td><asp:TextBox ID="UserReferenceTextBox" runat="server" 
                Text='<%# Bind("UserReference") %>' /></td></tr>

            <br />
            <tr><th>StationeryRetrievalForm:</th>
            <td><asp:TextBox ID="StationeryRetrievalFormTextBox" runat="server" 
                Text='<%# Bind("StationeryRetrievalForm") %>' /></td></tr>

            <br />
            <tr><th>StationeryRetrievalFormReference:</th>
            <td><asp:TextBox ID="StationeryRetrievalFormReferenceTextBox" runat="server" 
                Text='<%# Bind("StationeryRetrievalFormReference") %>' /></td></tr>

            <br />
            <tr><th>Department:</th>
            <td><asp:TextBox ID="DepartmentTextBox" runat="server" 
                Text='<%# Bind("Department") %>' /></td></tr>

            <br />
            <tr><th>DepartmentReference:</th>
            <td><asp:TextBox ID="DepartmentReferenceTextBox" runat="server" 
                Text='<%# Bind("DepartmentReference") %>' /></td></tr>

            <br />
            <tr><th>EntityState:</th>
            <td><asp:TextBox ID="EntityStateTextBox" runat="server" 
                Text='<%# Bind("EntityState") %>' /></td></tr>

            <br />
            <tr><th>EntityKey:</th>
            <td><asp:TextBox ID="EntityKeyTextBox" runat="server" 
                Text='<%# Bind("EntityKey") %>' /></td></tr>

            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                CommandName="Insert" Text="Insert" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
        <ItemTemplate>
            <tr><th>DisbursementID:</th>
            <td><asp:Label ID="DisbursementIDLabel" runat="server" 
                Text='<%# Bind("DisbursementID") %>' /></td></tr>

            <br />
            <tr><th>DateCreated:</th>
            <td><asp:Label ID="DateCreatedLabel" runat="server" 
                Text='<%# Bind("DateCreated") %>' /></td></tr>

            <br />
            <tr><th>CreatedBy:</th>
            <td><asp:Label ID="CreatedByLabel" runat="server" Text='<%# Bind("CreatedBy") %>' /></td></tr>

            <br />
            <tr><th>StationeryRetrievalFormID:</th>
            <td><asp:Label ID="StationeryRetrievalFormIDLabel" runat="server" 
                Text='<%# Bind("StationeryRetrievalFormID") %>' /></td></tr>

            <br />
            <tr><th>DepartmentID:</th>
            <td><asp:Label ID="DepartmentIDLabel" runat="server" 
                Text='<%# Bind("DepartmentID") %>' /></td></tr>

            <br />
            <tr><th>DisbursementItems:</th>
            <td><asp:Label ID="DisbursementItemsLabel" runat="server" 
                Text='<%# Bind("DisbursementItems") %>' /></td></tr>

            <br />
            <tr><th>User:</th>
            <td><asp:Label ID="UserLabel" runat="server" Text='<%# Bind("User") %>' /></td></tr>

            <br />
            <tr><th>UserReference:</th>
            <td><asp:Label ID="UserReferenceLabel" runat="server" 
                Text='<%# Bind("UserReference") %>' /></td></tr>

            <br />
            <tr><th>StationeryRetrievalForm:</th>
            <td><asp:Label ID="StationeryRetrievalFormLabel" runat="server" 
                Text='<%# Bind("StationeryRetrievalForm") %>' /></td></tr>

            <br />
            <tr><th>StationeryRetrievalFormReference:</th>
            <td><asp:Label ID="StationeryRetrievalFormReferenceLabel" runat="server" 
                Text='<%# Bind("StationeryRetrievalFormReference") %>' /></td></tr>

            <br />
            <tr><th>Department:</th>
            <td><asp:Label ID="DepartmentLabel" runat="server" 
                Text='<%# Bind("Department") %>' /></td></tr>

            <br />
            <tr><th>DepartmentReference:</th>
            <td><asp:Label ID="DepartmentReferenceLabel" runat="server" 
                Text='<%# Bind("DepartmentReference") %>' /></td></tr>

            <br />
            <tr><th>EntityState:</th>
            <td><asp:Label ID="EntityStateLabel" runat="server" 
                Text='<%# Bind("EntityState") %>' /></td></tr>

            <br />
            <tr><th>EntityKey:</th>
            <td><asp:Label ID="EntityKeyLabel" runat="server" Text='<%# Bind("EntityKey") %>' /></td></tr>

            <br />
        </ItemTemplate>
    </asp:FormView>
    <asp:ObjectDataSource ID="DisbursementObjectDataSource" runat="server" 
        SelectMethod="FindAllDisbursement" 
        TypeName="SA33.Team12.SSIS.BLL.DisbursementManager">
    </asp:ObjectDataSource>
</fieldset>
   

</asp:Content>
