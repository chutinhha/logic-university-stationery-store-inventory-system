<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BlackListLogs.aspx.cs" Inherits="SA33.Team12.SSIS.Administration.BlackListLogs" %>
<%@ Import Namespace="SA33.Team12.SSIS.DAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Black List Logs</h2>
<fieldset>
    <legend>Search Filter</legend>
    <table>
        <tr>
            <td>Department :</td>
            <td>
                <asp:DropDownList runat="server" ID="DepartmentDropDownList"
                    DataTextField="Name" DataValueField="DepartmentID">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Start Black Listed Date :</td>
            <td>
                <asp:TextBox runat="server" ID="StartBlackListedDateTextBox" />
                <script type="text/javascript">
					$(function() {
					    $('#<%= this.StartBlackListedDateTextBox.ClientID %>').datepicker({
							showOn: 'button',
							dateFormat: 'dd/mm/yy',
							buttonImage: '/Styles/jqui/images/calendar.gif',
							buttonImageOnly: true,
							setDate: '2/7/2011 2:11:32 PM',
							onSelect: function() { },
							maxDate: '+0d',
							showButtonPanel: false,
							changeMonth: true,
							changeYear: true,
							yearRange: '<%= DateTime.Now.Year-10 %>:<%= DateTime.Now.Year %>'
						});
					});
				</script>
            </td>
        </tr>
        <tr>
            <td>Start Black Listed Date :</td>
            <td>
                <asp:TextBox runat="server" ID="EndBlackListedDateTextBox" />
                <script type="text/javascript">
                    $(function () {
                        $('#<%= this.EndBlackListedDateTextBox.ClientID %>').datepicker({
                            showOn: 'button',
                            dateFormat: 'dd/mm/yy',
                            buttonImage: '/Styles/jqui/images/calendar.gif',
                            buttonImageOnly: true,
                            setDate: '2/7/2011 2:11:32 PM',
                            onSelect: function () { },
                            maxDate: '+0d',
                            showButtonPanel: false,
                            changeMonth: true,
                            changeYear: true,
                            yearRange: '<%= DateTime.Now.Year-10 %>:<%= DateTime.Now.Year %>'
                        });
                    });
				</script>
            </td>
        </tr>        <tr>
            <td></td>
            <td>
                <asp:Button runat="server" ID="SearchButton" Text="Search" 
                    onclick="SearchButton_Click" />
            </td>
        </tr>
    </table>
</fieldset>
<fieldset>
<legend>Black List Logs</legend>

    <asp:GridView runat="server" ID="BlackListLogGridView" AllowPaging="True" 
        AutoGenerateColumns="False" 
        onpageindexchanging="BlackListLogGridView_PageIndexChanging">
        <Columns>
            <asp:BoundField DataField="BlacklistLogID" HeaderText="BlacklistLogID" 
                SortExpression="BlacklistLogID" />
            <asp:TemplateField HeaderText="Department" SortExpression="DepartmentID">
                <ItemTemplate>
                    <%# ((Department) Eval("Department")).Name %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="DateBlacklisted" HeaderText="Date Black Listed" 
                SortExpression="DateBlacklisted" />
        </Columns>
    </asp:GridView>

</fieldset>

</asp:Content>
