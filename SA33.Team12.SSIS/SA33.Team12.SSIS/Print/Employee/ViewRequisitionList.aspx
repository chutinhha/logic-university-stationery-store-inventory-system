<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ViewRequisitionList.aspx.cs" Inherits="SA33.Team12.SSIS.Print.Employee.ViewRequisitionList" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        View Requisition List</h2>
    <br />
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" DataKeyNames="RequisitionID" OnPageIndexChanging="GridView1_PageIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="RequisitionID">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" PostBackUrl='<%# String.Format("~/RequestStationery/StationeryRequest.aspx?RequestID={0}", Eval("RequisitionID")) %>'
                        runat="server"><%# Eval("RequisitionID") %></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CreatedBy">
                <ItemTemplate>
                    <%# ((SA33.Team12.SSIS.DAL.User)Eval("CreatedByUser")).UserName %>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Department">
                <ItemTemplate>
                    <%# ((SA33.Team12.SSIS.DAL.Department)Eval("Department")).Name %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ApprovedBy" HeaderText="ApprovedBy" SortExpression="ApprovedBy" />
            <asp:BoundField DataField="RequisitionForm" HeaderText="RequisitionForm" SortExpression="RequisitionForm" />
            <asp:BoundField DataField="StatusID" HeaderText="StatusID" SortExpression="StatusID" />
            <asp:BoundField DataField="UrgencyID" HeaderText="UrgencyID" SortExpression="UrgencyID" />
            <asp:BoundField DataField="DateRequested" HeaderText="DateRequested" SortExpression="DateRequested" />
            <asp:BoundField DataField="DateApproved" HeaderText="DateApproved" SortExpression="DateApproved" />
        </Columns>
    </asp:GridView>
</asp:Content>
