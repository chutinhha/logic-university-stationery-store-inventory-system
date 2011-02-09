<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Distribution.aspx.cs" Inherits="SA33.Team12.SSIS.Distribution.Distribution" %>
<%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Distribution</h2>
<fieldset>
    <legend>Distribution by item</legend>
<%--    <script type="text/javascript">
        function validateDistributedQty(sender, arg) {
            var qtyDisbursed = $("#" + sender.id).attr("qtyDisbursed");
            if (arg.Value > qtyDisbursed)
                arg.IsValid = false;
            else
                arg.IsValid = true;
        }
    </script>--%>
    <asp:Label Text="" CssClass="failureNotification" ID="ErrorMessage" runat="server" />
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
                    <%# ((User) Eval("User")).UserName %>
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
            <asp:TemplateField HeaderText="Special">
                <ItemTemplate>
                    <input type="checkbox" disabled="disabled"
                        <%# (bool) Eval("IsSpecial") ? "checked='checked'" : "" %> />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="QuantityDisbursed" HeaderText="Quantity Disbursed" 
                SortExpression="QuantityDisbursed" />
            <asp:TemplateField HeaderText="Quantity Requested">
                <ItemTemplate>
            <asp:HiddenField runat="server" ID="IsSpecialHiddenField" Value='<%# Eval("IsSpecial") %>' />
            <asp:HiddenField runat="server" ID="RequisitionIDHiddenField" Value='<%# Eval("RequisitionID") %>' />
            <asp:HiddenField runat="server" ID="StationeryIDHiddenField" Value='<%# Eval("StationeryID") %>' />
            <asp:HiddenField runat="server" ID="SpecialStationeryIDHiddenField" Value='<%# Eval("SpecialStationeryID") %>' />
            <asp:HiddenField runat="server" ID="QuantityDisbursedHiddenField" Value='<%# Eval("QuantityDisbursed") %>' />

                                    <%# (bool) Eval("IsSpecial")
                                                        ? Eval("SpecialQuantityRequested")
                                                        : Eval("StandardQuantityRequested")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity Distributed">
                <ItemTemplate>
                    <asp:TextBox runat="server" Text="1" qtyDisbursed='<%# Eval("QuantityDisbursed") %>'  ID="QuantityTextBox" />

                    <asp:RequiredFieldValidator 
                        ErrorMessage="Quantity distributed is required" ControlToValidate="QuantityTextBox"
                        runat="server" ValidationGroup="Update" Display="Dynamic" />
<%--                    <asp:CustomValidator runat="server" EnableClientScript="true" Display=Dynamic
                         ValidateEmptyText="true" 
                        ControlToValidate="QuantityTextBox" ValidationGroup="Update"
                        ClientValidationFunction='validateDistributedQty'
                        ErrorMessage="Distributing quantity should not be larger than disbursed quantity."/>
--%>                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="DisbursementID" HeaderText="DisbursementID" 
                SortExpression="DisbursementID" Visible="false" />
        </Columns>

    </asp:GridView>

    <asp:Button runat="server" Text="Update" ID="UpdateButton" ValidationGroup="Update"
        onclick="UpdateButton_Click" />
    <asp:Button runat="server" Text="Back" ID="BackButton" 
        onclick="BackButton_Click" />


</fieldset>

</asp:Content>
