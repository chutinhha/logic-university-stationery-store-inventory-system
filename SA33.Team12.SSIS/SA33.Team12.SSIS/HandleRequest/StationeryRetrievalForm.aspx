<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="StationeryRetrievalForm.aspx.cs" Inherits="SA33.Team12.SSIS.StationeryRetrieval.UpdateStationeryRetrievalForm" %>

<%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<fieldset>
<legend><h2>Stationery Retrieval Form</h2></legend>
    <script type="text/javascript">
        function validateQuantityRetrieved(sender, arg) {
            var qty = $("#" + sender.id).attr("QuantityNeeded");
            if (parseInt(arg.Value) > parseInt(qty)) arg.IsValid = false;
            else arg.IsValid = true;
        }
    </script>
    <asp:Label runat="server" ID="ErrorMessage" CssClass="failureNotification" />
    <asp:FormView runat="server" ID="StationeryRetrievalFormView" 
        DataKeyNames="StationeryRetrievalFormID"
        AllowPaging="false" OnDataBound="StationeryRetrievalFormView_DataBound">
        <ItemTemplate>
            <table class="screenFriendlyGridView">
                <tr class="odd">
                    <th align="left">
                        StationeryRetrievalFormID :
                    </th>
                    <td>
                        <%# Eval("StationeryRetrievalFormID") %>
                    </td>
                </tr>
                <tr>
                    <th align="left">
                        StationeryRetrievalNumber :
                    </th>
                    <td>
                        <%# Eval("StationeryRetrievalNumber") %>
                    </td>
                </tr>
                <tr class="odd">
                    <th align="left">
                        DateRetrieved :
                    </th>
                    <td>
                        <%# Eval("DateRetrieved") %>
                    </td>
                </tr>
                <tr>
                    <th align="left">
                        Retrieved By :
                    </th>
                    <td>
                        <%# Convert.ToInt32(Eval("RetrievedBy")) == 0 ? "" : ((User) Eval("RetrievedByUser")).UserName %>
                    </td>
                </tr>
            </table>
            <fieldset>
                <legend>Item List</legend>
            <asp:GridView runat="server" ID="StationeryRetrievalFormItemGridView" AutoGenerateColumns="False"
                DataKeyNames="StationeryRetrievalFormItemID">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Stationery">
                        <ItemTemplate>
                            <%# Convert.ToInt32(Eval("StationeryID")) == 0 ? "" : ((Stationery) Eval("Stationery")).Description  %>
                            <%# Convert.ToInt32(Eval("SpecialStationeryID")) == 0 ? "" : ((SpecialStationery)Eval("SpecialStationery")).Description%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Is Special">
                        <ItemTemplate>
                            <input type="checkbox" <%# Convert.ToInt32(Eval("StationeryID")) == 0 ? "checked='checked'" : ""  %> disabled="disabled" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="QuantityNeeded" HeaderText="QuantityNeeded" SortExpression="QuantityNeeded" />
                    <asp:TemplateField HeaderText="Quantity Retrieved">
                        <ItemTemplate>
                            <asp:HiddenField runat="server" 
                                ID="StationeryRetrievalFormItemIDHiddenField" 
                                Value='<%# Eval("StationeryRetrievalFormItemID") %>' />
                            <asp:TextBox runat="server" Visible='<%# !this.IsRetrieved || !this.IsCollected %>'
                                ID="QuantityRetrievedTextBox"
                                CssClass="numericEntry">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator errormessage="The retrieved quantity is required." 
                                controltovalidate="QuantityRetrievedTextBox" runat="server"
                                Display="Dynamic"
                                ValidationGroup="Update" />
                            <asp:RegularExpressionValidator errormessage="Invalid quantity value."
                                ValidationExpression="\d{1,5}" ValidationGroup="Update" 
                                Display="Dynamic"
                                controltovalidate="QuantityRetrievedTextBox" runat="server"/>
                            <asp:CustomValidator errormessage="Quantity retrieved cannot be larger than quantity needed."
                                QuantityNeeded='<%# Eval("QuantityNeeded") %>'
                                controltovalidate="QuantityRetrievedTextBox"
                                EnableClientScript="True" ValidationGroup="Update"
                                ClientValidationFunction="validateQuantityRetrieved" 
                                Display="Dynamic"
                                runat="server"/>
                            <asp:Literal runat="server" 
                                ID="QuantityRetrievedLabel" Visible='<%# this.IsRetrieved %>'
                                Text='<%# Eval("QuantityRetrieved") %>'>
                            </asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:GridView 
                runat="server"
                ID="StationeryRetrievalFormItemByDeptGridView"
                AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Stationery">
                        <ItemTemplate>
                            <%# Convert.ToBoolean(Eval("IsSpecial")) ? ((SpecialStationery)Eval("SpecialStationery")).Description : ((Stationery)Eval("Stationery")).Description%>          
                            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <input type="checkbox" disabled="disabled"
                                <%# ((bool) Eval("IsSpecial"))? "checked='checked'":"" %> />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="QuantityNeededByItem" HeaderText="Quantity Needed" />
                    <asp:TemplateField HeaderText="Quantity Retrieved">
                        <ItemTemplate>
                            <%# Eval("QuantityRetrieved") %>
                            <asp:HiddenField runat="server" 
                                ID="StationeryRetrievalFormItemIDHiddenField" 
                                Value='<%# Eval("StationeryRetrievalFormItemID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Department" SortExpression="DepartmentID">
                        <ItemTemplate>
                            <%# Convert.ToInt32(Eval("DepartmentID")) == 0 
                                ? "" : ((Department) Eval("Department")).Name %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Urgency" SortExpression="Urgency">
                        <ItemTemplate>
                            <%# Eval("Urgency") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="QuantityNeeded" 
                        HeaderText="Quantity Needed" SortExpression="QuantityNeeded" />
                    <asp:TemplateField HeaderText="Quantity Recommended">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="QuantityRetrievedLabel" Text='<%# Eval("QuantityRecommended") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity Actual">
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="srfByDeptIDHiddenField" 
                                Value='<%# Eval("StationeryRetrievalFormItemByDeptID") %>' />
                            <asp:TextBox runat="server" ID="QuantityActualTextBox" 
                                Text='<%# Eval("QuantityRecommended") %>'  Visible='<%# !this.IsCollected %>' />
                            <asp:RequiredFieldValidator errormessage="The retrieved quantity is required." 
                                 ValidationGroup="Update"
                                Display="Dynamic"
                                controltovalidate="QuantityActualTextBox" runat="server" />
                            <asp:RegularExpressionValidator errormessage="Invalid quantity value."
                                ValidationExpression="\d{1,5}" ValidationGroup="Update"
                                Display="Dynamic"
                                controltovalidate="QuantityActualTextBox" runat="server"/>
                            <asp:CustomValidator errormessage="Actual quantity cannot be larger than quantity needed." 
                                QuantityNeeded='<%# Eval("QuantityNeeded") %>'
                                controltovalidate="QuantityActualTextBox"
                                EnableClientScript="True" ValidationGroup="Update"
                                ClientValidationFunction="validateQuantityRetrieved" 
                                Display="Dynamic"
                                runat="server"/>
                            <asp:Label runat="server" ID="QuantityActualLabel"
                                Text='<%# Eval("QuantityActual") %>' Visible='<%# this.IsCollected %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </fieldset>
        </ItemTemplate>
    </asp:FormView>
    <asp:Button runat="server" ID="BackButton" Text="Back" OnClick="BackButton_Click" />
    <asp:Button runat="server" ID="UpdateRetrievedQuantityButton" Text="Update" 
        oncommand="UpdateButton_Command" CommandName="Retrieved" ValidationGroup="Update" />
    <asp:Button runat="server" ID="UpdateActualQuantityButton" Text="Update" 
        oncommand="UpdateButton_Command" CommandName="Actual" ValidationGroup="Update" />

</fieldset>
</asp:Content>
