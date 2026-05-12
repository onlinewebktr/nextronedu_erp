<%@ Page Title="" Language="C#" MasterPageFile="~/Dvlpr_Prof/Site1.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="school_web.Dvlpr_Prof.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin: 0px; float: left; height: 624px; width: 100%;">

        <h2 style="margin: 0px; padding: 30px 0px 0px 0px; text-align: center;">Welcome to Developer Profile
        </h2>
        <div style="margin: 20px 0px 0px 0px; padding: 0px; float: left;width: 100%;text-align: center;" id="updaate" runat="server">
            <div  style="margin: 5px 0px 5px 0px; padding: 0px; float: left;width: 100%;text-align: center;font-size: 18px;">
                New update available  here please click  to update 
            </div>

           <a href="Check_Update.aspx">
            <img src="js/update_gif.gif" style="  width: 200px; border:1px solid #000" />

               </a>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
