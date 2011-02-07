<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Disbursement.aspx.cs" Inherits="SA33.Team12.SSIS.RequestStationery.Disbursement" %>
<%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
    <legend> <h2>Disbursement</h2></legend>

    <asp:FormView runat="server" ID="DisbursementFormView" DataKeyNames="DisbursementID"
        ondatabound="DisbursementFormView_DataBound">
        <EditItemTemplate>
            <table>
            <tr><th>DisbursementID:</th>
            <td><asp:TextBox ID="DisbursementIDTextBox" runat="server" 
                Text='<%# Bind("DisbursementID") %>' /></td></tr>

            
            <tr><th>DateCreated:</th>
            <td><asp:TextBox ID="DateCreatedTextBox" runat="server" 
                Text='<%# Bind("DateCreated") %>' /></td></tr>

            
            <tr><th>CreatedBy:</th>
            <td><asp:TextBox ID="CreatedByTextBox" runat="server" 
                Text='<%# Bind("CreatedBy") %>' /></td></tr>

            
            <tr><th>StationeryRetrievalFormID:</th>
            <td><asp:TextBox ID="StationeryRetrievalFormIDTextBox" runat="server" 
                Text='<%# Bind("StationeryRetrievalFormID") %>' /></td></tr>

            
            <tr><th>DepartmentID:</th>
            <td><asp:TextBox ID="DepartmentIDTextBox" runat="server" 
                Text='<%# Bind("DepartmentID") %>' /></td></tr>

            
            <tr><th>DisbursementItems:</th>
            <td><asp:TextBox ID="DisbursementItemsTextBox" runat="server" 
                Text='<%# Bind("DisbursementItems") %>' /></td></tr>

            
            <tr><th>User:</th>
            <td><asp:TextBox ID="UserTextBox" runat="server" Text='<%# Bind("User") %>' /></td></tr>

            
            <tr><th>UserReference:</th>
            <td><asp:TextBox ID="UserReferenceTextBox" runat="server" 
                Text='<%# Bind("UserReference") %>' /></td></tr>

            
            <tr><th>StationeryRetrievalForm:</th>
            <td><asp:TextBox ID="StationeryRetrievalFormTextBox" runat="server" 
                Text='<%# Bind("StationeryRetrievalForm") %>' /></td></tr>

            
            <tr><th>StationeryRetrievalFormReference:</th>
            <td><asp:TextBox ID="StationeryRetrievalFormReferenceTextBox" runat="server" 
                Text='<%# Bind("StationeryRetrievalFormReference") %>' /></td></tr>

            
            <tr><th>Department:</th>
            <td><asp:TextBox ID="DepartmentTextBox" runat="server" 
                Text='<%# Bind("Department") %>' /></td></tr>

            
            <tr><th>DepartmentReference:</th>
            <td><asp:TextBox ID="DepartmentReferenceTextBox" runat="server" 
                Text='<%# Bind("DepartmentReference") %>' /></td></tr>

            
            <tr><th>EntityState:</th>
            <td><asp:TextBox ID="EntityStateTextBox" runat="server" 
                Text='<%# Bind("EntityState") %>' /></td></tr>

            
            <tr><th>EntityKey:</th>
            <td><asp:TextBox ID="EntityKeyTextBox" runat="server" 
                Text='<%# Bind("EntityKey") %>' /></td></tr>

            
            </table>
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                CommandName="Update" Text="Update" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </EditItemTemplate>
        <InsertItemTemplate>
            <table>
            <tr><th>DisbursementID:</th>
            <td><asp:TextBox ID="DisbursementIDTextBox" runat="server" 
                Text='<%# Bind("DisbursementID") %>' /></td></tr>

            
            <tr><th>DateCreated:</th>
            <td><asp:TextBox ID="DateCreatedTextBox" runat="server" 
                Text='<%# Bind("DateCreated") %>' /></td></tr>

            
            <tr><th>CreatedBy:</th>
            <td><asp:TextBox ID="CreatedByTextBox" runat="server" 
                Text='<%# Bind("CreatedBy") %>' /></td></tr>

            
            <tr><th>StationeryRetrievalFormID:</th>
            <td><asp:TextBox ID="StationeryRetrievalFormIDTextBox" runat="server" 
                Text='<%# Bind("StationeryRetrievalFormID") %>' /></td></tr>

            
            <tr><th>DepartmentID:</th>
            <td><asp:TextBox ID="DepartmentIDTextBox" runat="server" 
                Text='<%# Bind("DepartmentID") %>' /></td></tr>

            
            <tr><th>DisbursementItems:</th>
            <td><asp:TextBox ID="DisbursementItemsTextBox" runat="server" 
                Text='<%# Bind("DisbursementItems") %>' /></td></tr>

            
            <tr><th>User:</th>
            <td><asp:TextBox ID="UserTextBox" runat="server" Text='<%# Bind("User") %>' /></td></tr>

            
            <tr><th>UserReference:</th>
            <td><asp:TextBox ID="UserReferenceTextBox" runat="server" 
                Text='<%# Bind("UserReference") %>' /></td></tr>

            
            <tr><th>StationeryRetrievalForm:</th>
            <td><asp:TextBox ID="StationeryRetrievalFormTextBox" runat="server" 
                Text='<%# Bind("StationeryRetrievalForm") %>' /></td></tr>

            
            <tr><th>StationeryRetrievalFormReference:</th>
            <td><asp:TextBox ID="StationeryRetrievalFormReferenceTextBox" runat="server" 
                Text='<%# Bind("StationeryRetrievalFormReference") %>' /></td></tr>

            
            <tr><th>Department:</th>
            <td><asp:TextBox ID="DepartmentTextBox" runat="server" 
                Text='<%# Bind("Department") %>' /></td></tr>

            
            <tr><th>DepartmentReference:</th>
            <td><asp:TextBox ID="DepartmentReferenceTextBox" runat="server" 
                Text='<%# Bind("DepartmentReference") %>' /></td></tr>

            
            <tr><th>EntityState:</th>
            <td><asp:TextBox ID="EntityStateTextBox" runat="server" 
                Text='<%# Bind("EntityState") %>' /></td></tr>

            
            <tr><th>EntityKey:</th>
            <td><asp:TextBox ID="EntityKeyTextBox" runat="server" 
                Text='<%# Bind("EntityKey") %>' /></td></tr>
            </table>
            
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                CommandName="Insert" Text="Insert" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
        <ItemTemplate>
            <table class="screenFriendlyGridView">
            <tr class="odd"><th>Disbursement ID:</th>
            <td><asp:Label ID="DisbursementIDLabel" runat="server" 
                Text='<%# Bind("DisbursementID") %>' /></td></tr>

            
            <tr><th>DateCreated:</th>
            <td><asp:Label ID="DateCreatedLabel" runat="server" 
                Text='<%# ((DateTime) Eval("DateCreated")).ToString("dd/MMM/yyyy") %>' /></td></tr>

            
            <tr class="odd">
<th>CreatedBy:</th>
            <td><asp:Label ID="CreatedByLabel" runat="server" Text='<%# ((User) Eval("User")).UserName %>' /></td></tr>

            
            <tr><th>Stationery Retrieval Form No. :</th>
            <td><asp:Label ID="StationeryRetrievalFormIDLabel" runat="server" 
                Text='<%# ((StationeryRetrievalForm) Eval("StationeryRetrievalForm")).StationeryRetrievalNumber %>' />
            </td></tr>

            
            <tr class="odd"><th>Department:</th>
            <td><asp:Label ID="DepartmentIDLabel" runat="server" 
                Text='<%# ((Department) Eval("Department")).Name %>' /></td></tr>
            </table>

        
        </ItemTemplate>
    </asp:FormView>
    <fieldset>
        <legend>Disbursement Items</legend>
    <asp:GridView runat="server" ID="DisbursementGridView" 
        AutoGenerateColumns="False" DataKeyNames="DisbursementItemID"
        onrowcancelingedit="DisbursementGridView_RowCancelingEdit" 
        onrowediting="DisbursementGridView_RowEditing" 
        onrowupdating="DisbursementGridView_RowUpdating">
                   <Columns>
                       <asp:BoundField DataField="DisbursementItemID" HeaderText="DisbursementItemID" 
                           SortExpression="DisbursementItemID" Visible="false" />
                       <asp:BoundField DataField="DisbursementID" HeaderText="DisbursementID" 
                           SortExpression="DisbursementID" Visible="false" />
                       <asp:BoundField DataField="StationeryRetrievalFormItemByDeptID" 
                           HeaderText="StationeryRetrievalFormItemByDeptID" 
                           SortExpression="StationeryRetrievalFormItemByDeptID" Visible="false" />
                       <asp:TemplateField HeaderText="Adjustment Voucher No." 
                            SortExpression="AdjustmentVoucherID">
                            <ItemTemplate>
                                <%# Convert.ToInt32(Eval("AdjustmentVoucherID")) == 0 ? "" : 
                                    ((AdjustmentVoucher) Eval("AdjustmentVoucher")).VoucherNumber %>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Stationery" 
                           SortExpression="StationeryID">
                           <ItemTemplate>
                                <%# Convert.ToInt32(Eval("StationeryID")) == 0 
                                    ? ((SpecialStationery) Eval("SpecialStationery")).ItemCode
                                    : ((Stationery)Eval("Stationery")).ItemCode %>
                           </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField  
                            HeaderText="Quantity Disbursed" 
                           SortExpression="QuantityDisbursed">
                           <ItemTemplate>
                            <%# Eval("QuantityDisbursed")%>
                           </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Quantity Damaged" 
                           SortExpression="QuantityDamaged">
                           <ItemTemplate>
                                <%# Eval("QuantityDamaged") %>
                           </ItemTemplate>
                           <EditItemTemplate>
                                <asp:TextBox runat="server" ID="QuantityDamagedTextBox"
                                    Text='<%# Eval("QuantityDamaged") %>' />
                           </EditItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Reason" 
                           SortExpression="Reason">
                           <ItemTemplate>
                                <%# Eval("Reason") %>
                           </ItemTemplate>
                           <EditItemTemplate>
                                <asp:TextBox runat="server" ID="ReasonTextBox"
                                    TextMode="MultiLine" 
                                    Columns="50"
                                    Rows="5"
                                    Text='<%# Eval("Reason") %>' />
                           </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="EditLinkButton" runat="server"
                                    CommandName="Edit" Text="Edit" 
                                    CommandArgument='<%# Eval("DisbursementItemID") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="UpdateLinkButton" runat="server"
                                    CommandName="Update" Text="Update" 
                                    CommandArgument='<%# Eval("DisbursementItemID") %>' />
                                <asp:LinkButton ID="CancelLinkButton" runat="server"
                                    CommandName="Cancel" Text="Cancel" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                   </Columns>
    </asp:GridView>
    </fieldset>
    <asp:Button runat="server" ID="UpdateButton" Text="Update" />
    <asp:Button runat="server" ID="BackButton" Text="Back" onclick="BackButton_Click" />
</fieldset>
</asp:Content>
