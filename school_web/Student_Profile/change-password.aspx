<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="change-password.aspx.cs" Inherits="school_web.Student_Profile.change_password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Change Password
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pagemainhh">
        <div class="container">

            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div id="success" runat="server" visible="false" style="float: left; width: 100%; height: auto;" class="alert alert-success border-0 bg-success alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-white">
                                <i class='bx bxs-check-circle'></i>
                            </div>
                            <div class="ms-3">
                                <asp:Label ID="lbl_success" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton1" class="btn-closes" runat="server" Style="color: #fff">X</asp:LinkButton>
                    </div>

                    <div id="warning" runat="server" visible="false" class="alert alert-warning border-0 bg-warning alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-dark">
                                <i class='bx bx-info-circle'></i>
                            </div>
                            <div class="ms-3">
                                <asp:Label ID="lbl_warning" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton2" class="btn-closes" runat="server" Style="color: #fff">X</asp:LinkButton>
                    </div>
                </div>
            </div>



            <div class="headingtablee">
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12"></div>
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <div class="main-card mb-3 card">
                            <div class="card-header">
                                <h4 class="card-title">Change Password</h4>
                            </div>
                            <div class="card-body" style="padding-top: 0px;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">Old Password</p>
                                            <asp:TextBox ID="txt_old_password" runat="server" CssClass="form-control pontr-non" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">New Password</p>
                                            <asp:TextBox ID="txt_new_password" runat="server" CssClass="form-control pontr-non" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">Confirm Password</p>
                                            <asp:TextBox ID="txt_confirm_password" runat="server" CssClass="form-control pontr-non" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="col-md-12">
                                        <div class="btns-dv-wpr">
                                            <asp:Button ID="btn_Submit" runat="server" Text="Change Password" class="mt-2 btn btn-primary" OnClick="btn_update_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
