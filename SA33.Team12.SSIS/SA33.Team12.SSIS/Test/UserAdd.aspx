<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="UserAdd.aspx.cs" Inherits="SA33.Team12.SSIS.UserAdministration.UserAdd" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Create a New User Account
    </h2>
    <p>
        Use the form below to create a new account.
    </p>
    <p>
        Passwords are required to be a minimum of
        <%= Membership.MinRequiredPasswordLength %>
        characters in length.
    </p>
    <span class="failureNotification">
        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
    </span>
    <asp:ValidationSummary ID="RegisterUserValidationSummary" 
        runat="server" 
        CssClass="failureNotification"
        ValidationGroup="RegisterUserValidationGroup" />
    <div class="accountInfo">
        <fieldset class="register">
            <legend>Account Information</legend>
            <p>
                <asp:Label ID="UserNameLabel" 
                    runat="server" 
                    AssociatedControlID="UserName">User Name:</asp:Label>
                <asp:TextBox ID="UserName" 
                    runat="server" 
                    CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequired" 
                    runat="server" 
                    ControlToValidate="UserName"
                    CssClass="failureNotification" 
                    ErrorMessage="User Name is required." 
                    ToolTip="User Name is required."
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>

            <p>
                <asp:Label ID="FirstNameLabel" 
                    runat="server" 
                    AssociatedControlID="FirstNameTextBox">First Name:</asp:Label>
                <asp:TextBox ID="FirstNameTextBox" 
                    runat="server" 
                    CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator ID="FirstNameRequired" 
                    runat="server" 
                    ControlToValidate="FirstNameTextBox"
                    CssClass="failureNotification" 
                    ErrorMessage="First Name is required." 
                    ToolTip="First Name is required."
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            

            <p>
                <asp:Label ID="LastNameLabel" 
                    runat="server" 
                    AssociatedControlID="LastNameTextBox">Last Name:</asp:Label>
                <asp:TextBox ID="LastNameTextBox" 
                    runat="server" 
                    CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator ID="LastNameRequired" 
                    runat="server" 
                    ControlToValidate="LastNameTextBox"
                    CssClass="failureNotification" 
                    ErrorMessage="Last Name is required." 
                    ToolTip="Last Name is required."
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="EmailLabel" 
                    runat="server" 
                    AssociatedControlID="Email">E-mail:</asp:Label>
                <asp:TextBox ID="Email" 
                    runat="server" 
                    CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator 
                    ID="EmailRequired" 
                    runat="server" 
                    ControlToValidate="Email"
                    CssClass="failureNotification" 
                    ErrorMessage="E-mail is required." 
                    ToolTip="E-mail is required."
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="PasswordLabel" 
                    runat="server" 
                    AssociatedControlID="Password">Password:</asp:Label>
                <asp:TextBox ID="Password" 
                    runat="server" 
                    CssClass="passwordEntry" 
                    TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" 
                    runat="server" 
                    ControlToValidate="Password"
                    CssClass="failureNotification" 
                    ErrorMessage="Password is required." 
                    ToolTip="Password is required."
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>

            <p>
                <asp:Label ID="ConfirmPasswordLabel" 
                    runat="server" 
                    AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                <asp:TextBox ID="ConfirmPassword" 
                    runat="server" 
                    CssClass="passwordEntry" 
                    TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword"     
                    CssClass="failureNotification"
                    Display="Dynamic" 
                    ErrorMessage="Confirm Password is required." 
                    ID="ConfirmPasswordRequired"
                    runat="server" 
                    ToolTip="Confirm Password is required." 
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="PasswordCompare" 
                    runat="server" 
                    ControlToCompare="Password"
                    ControlToValidate="ConfirmPassword" 
                    CssClass="failureNotification" 
                    Display="Dynamic"
                    ErrorMessage="The Password and Confirmation Password must match." 
                    ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>
            </p>

            <p>
                <asp:Label ID="DepartmentLabel" 
                    runat="server"
                    AssociatedControlID="DepartmentDropDownList">Select a department:</asp:Label>
                <asp:DropDownList runat="server" 
                    ID="DepartmentDropDownList" 
                    DataTextFormatString="{0: #}">
                </asp:DropDownList>
                <asp:RequiredFieldValidator 
                    ControlToValidate="DepartmentDropDownList" 
                    CssClass="failureNotification"
                    Display="Dynamic" 
                    ErrorMessage="Select a department." 
                    ID="DepartmentRequired"
                    runat="server" 
                    ToolTip="Select a department."
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>

            <p>
                <asp:Label ID="MemberRolesLabel" 
                    runat="server"
                    AssociatedControlID="MemberRolesRadioButtonList">Select a Role:</asp:Label>
                <asp:RadioButtonList runat="server" 
                    ID="MemberRolesRadioButtonList" 
                    DataTextFormatString="{0: #}">
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator 
                    ControlToValidate="MemberRolesRadioButtonList" 
                    CssClass="failureNotification"
                    Display="Dynamic" 
                    ErrorMessage="Select at least one role." 
                    ID="MemeberRoleRequied"
                    runat="server" 
                    ToolTip="Select at least one role."
                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
            </p>

        </fieldset>
        <p class="submitButton">
            <asp:Button ID="CreateUserButton" 
                runat="server" 
                CommandName="MoveNext" 
                Text="Create User"
                ValidationGroup="RegisterUserValidationGroup" 
                onclick="CreateUserButton_Click" />
        </p>
    </div>
</asp:Content>
