<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/User.Master" AutoEventWireup="true" CodeBehind="Change_password.aspx.cs" Inherits="school_web.Student_Profile.Change_password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Change Password
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ol class="breadcrumb">
        <li class="breadcrumb-item"><a href="Dashboard.aspx">Dashboard</a> <i class="fa fa-angle-right"></i></li>
        <li class="breadcrumb-item">Change Password</li>
    </ol>
    <div id="notification">
        <div id="pan" class="notificationpan">
            <div style="float: left; width: 235px; height: auto;">
                <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
            </div>
            <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                class="closenotificationpan" alt="" />
        </div>
    </div>
    <div class="grid-form">
        <div class="grid-form1">
            <div class="panel-body">
                <div class="row" style="padding: 0px 0px 0px 0px;">
                    <div class="col-md-6 col-sm-6 col-xs-6" style="margin: 0px auto; padding: 0px; float: none; height: auto;">
                        <h2 class=" blue_bg">Change Password</h2>
                        <div class="row form-group">
                            <div class="col-md-6 col-sm-6 col-xs-6">
                                Old Password :
                            </div>
                            <div class="col-md-6 col-sm-6 col-xs-6">
                                <asp:TextBox ID="txt_old_password" runat="server" class="textcolor" Style="width: 100%;" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-md-6 col-sm-6 col-xs-6">
                                New Password:
                            </div>
                            <div class="col-md-6 col-sm-6 col-xs-6">
                                <asp:TextBox ID="txt_new_password" runat="server" class="textcolor" Width="100%" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-md-6 col-sm-6 col-xs-6">
                                Confirm Password :
                            </div>
                            <div class="col-md-6 col-sm-6 col-xs-6">
                                <asp:TextBox ID="txt_confirm_password" runat="server" class="textcolor" Width="100%" TextMode="Password"></asp:TextBox>
                            </div>

                        </div>


                        <div class="row form-group">
                           <div class="col-md-4 col-sm-6 col-xs-12">
                                <asp:Button ID="btn_update" runat="server" Text="Change Password" CssClass="btn-danger" OnClick="btn_update_Click" />
                            </div>

                        </div>
                    </div>

                </div>

            </div>
        </div>
    </div>

</asp:Content>
