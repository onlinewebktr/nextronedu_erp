<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="school_web.LMS_VC_Admin.test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css"
            rel="stylesheet" type="text/css" />
        <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js"
            type="text/javascript"></script>
        <script type="text/javascript">
            $(function () {
                $('[id*=lstFruits]').multiselect({
                    includeSelectAllOption: true
                });
            });
        </script>
        <asp:ListBox ID="lstFruits" runat="server" SelectionMode="Multiple">
            <asp:ListItem Text="Mango" Value="1" />
            <asp:ListItem Text="Apple" Value="2" />
            <asp:ListItem Text="Banana" Value="3" />
            <asp:ListItem Text="Guava" Value="4" />
            <asp:ListItem Text="Orange" Value="5" />
        </asp:ListBox>
        <asp:Button ID="Button1" Text="Submit" runat="server"  OnClick="Button1_Click"/>

</asp:Content>
