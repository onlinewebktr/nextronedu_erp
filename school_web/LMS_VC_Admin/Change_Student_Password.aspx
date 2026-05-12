<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Change_Student_Password.aspx.cs" Inherits="school_web.LMS_VC_Admin.Change_Student_Password" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">Student Change Password
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
                     Student Change Password
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
                        <h5 class="card-title">Student Change Password </h5>
                        <div class="form-row">
                            <div class="col-md-12">

                                <div class="form-group col-xs-10 col-sm-8 col-md-8 col-lg-8">
                                    <label>
                                        Student Admission No.</label>
                                    <asp:TextBox ID="txtstudent_admission_no"   class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                <div class="form-group col-xs-10 col-sm-8 col-md-8 col-lg-8">
                                    <label>
                                        New Password</label>
                                    <asp:TextBox ID="txtnewpassword"  class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="clearfix"></div>
                                
                                <div class="clearfix"></div>
                                <div class="form-group col-xs-10 col-sm-8 col-md-8 col-lg-8">
                                    <asp:Button ID="BtnChangePassword" runat="server" OnClick="BtnChangePassword_Click" class="btn btn-sm btn-success" Text="Change Password" style="margin: 17px 0px 0px 0px;" />

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
