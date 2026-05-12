<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="school_web.LMS_VC_Admin.AdminLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
   Online Test Login
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-users icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                      Online Test Login
                    </div>
                </div>
            </div>
        </div>
        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 100%; height: auto;">
                    <asp:Label ID="lblmessage" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-lg-5">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <div class="form-row">
                            <div class="col-md-12">

                                <div class="form-group">
                                    <label>
                                        Enter Username</label>
                                    <asp:TextBox ID="txt_AdminUser"  class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <div class="form-group">
                                    <label>
                                        Enter Password</label>
                                    <asp:TextBox ID="txt_adminPasswd" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <div class="form-group" style="margin: 9px 0px 0px 0px;">
                                    <asp:Button ID="BtnLogin" runat="server" OnClick="BtnLogin_Click" class="btn btn-sm btn-success" Text="Login" />

                                </div>
                                <div class="clearfix"></div>
                                <div class="form-group col-xs-10 col-sm-8 col-md-8 col-lg-8">
                                    <asp:HiddenField ID="hddId" runat="server" />
                                    <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="clearfix"></div>
                                <asp:HiddenField ID="hdfUserType" runat="server" />
                                <asp:HiddenField ID="hdfUserID" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>



    </div>





</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
