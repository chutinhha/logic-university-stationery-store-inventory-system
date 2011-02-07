<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="StationeryRequest.aspx.cs" Inherits="SA33.Team12.SSIS.Test.StationeryRequest" %>

<%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Stationery Request Form</h2>
    <asp:DetailsView ID="RequestDetailsView" runat="server" AutoGenerateRows="False"
        DataSourceID="RequisitionDetailsDS">
        <Fields>
            <asp:BoundField HeaderText="Requisition Form Number" DataField="RequisitionForm" />
            <asp:TemplateField HeaderText="Employee Name">
                <ItemTemplate>
                    <%# ((User) Eval("CreatedByUser")).UserName %>
                </ItemTemplate>
                <EditItemTemplate>
                    <%# ((User) Eval("CreatedByUser")).UserName %>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Department">
                <ItemTemplate>
                    <%# ((Department) Eval("Department")).Name %>
                </ItemTemplate>
                <EditItemTemplate>
                    <%# ((Department) Eval("Department")).Name %>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Urgency">
                <ItemTemplate>
                    <%# ((Urgency) Eval("Urgency")).Name  %>
                </ItemTemplate>
                <EditItemTemplate>
                    <%# ((Urgency) Eval("Urgency")).Name  %>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Date Requested" DataField="DateRequested" />
            <asp:TemplateField HeaderText="Approved By">
                <ItemTemplate>
                    <%# ((User)Eval("ApprovedByUser")) == null ? "" : ((User)Eval("ApprovedByUser")).UserName %>
                </ItemTemplate>
                <EditItemTemplate>
                    <%# ((User)Eval("ApprovedByUser")) == null ? "" : ((User)Eval("ApprovedByUser")).UserName %>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Date Approved" DataField="DateApproved" />
        </Fields>
    </asp:DetailsView>
    &nbsp;<asp:ObjectDataSource ID="RequisitionDetailsDS" runat="server" SelectMethod="GetRequisitionByID"
        TypeName="SA33.Team12.SSIS.BLL.RequisitionManager">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="RequisitionID" QueryStringField="RequestID"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <fieldset>
        <legend>Add Requisition Items</legend>
        <fieldset>
            <legend>Urgency Level</legend>
            <asp:Panel ID="Panel3" runat="server">
                <asp:DropDownList ID="UrgencyDDL" runat="server">
                </asp:DropDownList>
            </asp:Panel>
        </fieldset>
        <fieldset>
            <legend>Add Items</legend>
            <asp:Panel ID="Panel1" runat="server">
                <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateInsertButton="True"
                    AutoGenerateRows="False" DefaultMode="Insert" Height="50px" OnItemInserting="DetailsView1_ItemInserting"
                    Width="466px" EnableViewState="False" 
                    OnModeChanging="DetailsView1_ModeChanging" 
                    oniteminserted="DetailsView1_ItemInserted">
                    <Fields>
                        <asp:TemplateField HeaderText="Stationery">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="StationeryItemDS0"
                                    DataTextField="Description" DataValueField="StationeryID">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="StationeryItemDS" runat="server" SelectMethod="GetAllStationeries"
                                    TypeName="SA33.Team12.SSIS.BLL.CatalogManager"></asp:ObjectDataSource>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:DropDownList ID="stDDL" runat="server" DataSourceID="StationeryItemDS0"
                                    DataTextField="Description" DataValueField="StationeryID" 
                                    SelectedValue='<%# Bind("StationeryID") %>'>
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="StationeryItemDS0" runat="server" SelectMethod="GetAllStationeries"
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
                                <asp:TextBox ID="stTextBox" runat="server" ValidationGroup="QuantityNeeded"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="stTextBox"
                                    Display="Dynamic" ErrorMessage="Invalid Quantity" MaximumValue="10000" MinimumValue="1"
                                    Type="Integer" ValidationGroup="QuantityNeeded" ToolTip="Invalid Quantity">Invalid Quantity</asp:RangeValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="stTextBox"
                                    Display="Dynamic" ErrorMessage="Quantity is needed" ValidationGroup="QuantityNeeded"
                                    ToolTip="Quantity is needed">Quantity is needed</asp:RequiredFieldValidator>
                            </InsertItemTemplate>
                        </asp:TemplateField>
                    </Fields>
                </asp:DetailsView>
            </asp:Panel>
        </fieldset>
        <fieldset>
            <legend>ADD Special items</legend>
            <asp:Panel ID="Panel2" runat="server">
                <p>
                    <asp:DetailsView ID="DetailsView2" runat="server" AutoGenerateInsertButton="True"
                        AutoGenerateRows="False" DefaultMode="Insert" Height="50px" OnItemInserting="DetailsView2_ItemInserting"
                        Width="600px" OnModeChanging="DetailsView2_ModeChanging">
                        <Fields>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" ValidationGroup="SplItemValidation"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox1"
                                        Display="Dynamic" ErrorMessage="Item Name Required" ValidationGroup="SplItemValidation"></asp:RequiredFieldValidator>
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
                                    <asp:TextBox ID="TextBox2" runat="server" ValidationGroup="SplItemValidation"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox2"
                                        Display="Dynamic" ErrorMessage="Description Required" ValidationGroup="SplItemValidation"></asp:RequiredFieldValidator>
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
                                    <asp:TextBox ID="TextBox3" runat="server" ValidationGroup="SplItemValidation"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox3"
                                        Display="Dynamic" ErrorMessage="Quantity Required" ValidationGroup="SplItemValidation"></asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="TextBox3"
                                        Display="Dynamic" ErrorMessage="Invalid Quantity Entered" MaximumValue="10000"
                                        MinimumValue="1" Type="Integer" ValidationGroup="SplItemValidation"></asp:RangeValidator>
                                </InsertItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reason">
                                <ItemTemplate>
                                    <span id="idControl">DetailsView2</span> - Field[3] - Reason
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server" ValidationGroup="SplItemValidation"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox4"
                                        Display="Dynamic" ErrorMessage="Reason required" ValidationGroup="SplItemValidation"></asp:RequiredFieldValidator>
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
                                    <asp:TextBox ID="TextBox5" runat="server" ValidationGroup="SplItemValidation"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBox5"
                                        Display="Dynamic" ErrorMessage="Unit of Measure Required" ValidationGroup="SplItemValidation"></asp:RequiredFieldValidator>
                                </InsertItemTemplate>
                            </asp:TemplateField>
                        </Fields>
                    </asp:DetailsView>
                </p>
            </asp:Panel>
        </fieldset>
    </fieldset>
    <fieldset>
        <legend>Items Added</legend>
            <fieldset>
                <legend>Items</legend>
                <asp:GridView ID="RequestItemGridView" runat="server" AutoGenerateColumns="False"
                    AutoGenerateEditButton="True" OnRowCancelingEdit="RequestItemGridView_RowCancelingEdit"
                    OnRowDeleting="RequestItemGridView_RowDeleting" OnRowEditing="RequestItemGridView_RowEditing"
                    OnRowUpdating="RequestItemGridView_RowUpdating" 
                    DataKeyNames="RequisitionItemID,StationeryID" AutoGenerateDeleteButton="True" 
                    onrowcommand="RequestItemGridView_RowCommand" 
                    onrowdatabound="RequestItemGridView_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="RequisitionItemID">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("RequisitionItemID") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <%# Eval("RequisitionItemID") %>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("RequisitionItemID") %>'></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="StationeryID">
                            <EditItemTemplate>
                                <asp:DropDownList ID="stationeryDDL" runat="server" DataSourceID="StationeryDS" DataTextField="Description"
                                    DataValueField="StationeryID" SelectedValue='<%# Bind("StationeryID") %>'>
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="StationeryDS" runat="server" SelectMethod="GetAllStationeries"
                                    TypeName="SA33.Team12.SSIS.BLL.CatalogManager"></asp:ObjectDataSource>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("StationeryID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity Needed">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("QuantityRequested") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="QtyTextBox" runat="server" 
                                    Text='<%# Bind("QuantityRequested") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </fieldset>
            <fieldset>
                <legend>Special Items</legend>
                <asp:GridView ID="SpecialRequestItemGridView" runat="server" AutoGenerateColumns="False"
                    Style="margin-right: 0px">
                    <Columns>
                        <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("SpecialStationeryID") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("SpecialStationeryID") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reason">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("RemarkByRequester") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("RemarkByRequester") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Width="200px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity Needed">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("QuantityRequested") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("QuantityRequested") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                 </fieldset>    
                <asp:Button ID="SubmitButton" runat="server" OnClick="SubmitButton_Click" Text="Submit Request" />
                <asp:Button ID="CancelButton" runat="server" Text="Cancel Request" OnClick="CancelButton_Click" />
               
    </fieldset>
</asp:Content>
