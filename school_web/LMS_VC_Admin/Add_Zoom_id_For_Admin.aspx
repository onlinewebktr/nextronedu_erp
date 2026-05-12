<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Add_Zoom_id_For_Admin.aspx.cs" Inherits="school_web.LMS_VC_Admin.Add_Zoom_id_For_Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
      Zoom id For Super admin
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-id icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">   Zoom id For Super admin</asp:Literal>

                    </div>
                </div>
                <div class="page-title-actions">
                </div>
            </div>
        </div>
        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 235px; height: auto;">
                    <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
                <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                    class="closenotificationpan" alt="" />
            </div>
        </div>
        <div class="clearfix"></div>
        <div class="main-card mb-3 card">

            <div class="row">
                <div class="col-lg-4" style="display: block">
                    <div class="main-card card">
                        <div class="card-body">
                            <h5 class="card-title"></h5>
                            <div class="form-row">
                                <div class="col-md-12">
                                    <h5 class="card-title"></h5>
                                    <div class="position-relative form-group">
                                        <label>Name of Super Admin<sup>*</sup></label>
                                        <div class="input-group input-group-icon">
                                            <asp:TextBox ID="txt_Name" runat="server" CssClass="form-control"> </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="position-relative form-group">
                                        <label>Zoom User Id<sup>*</sup></label>
                                        <div class="input-group input-group-icon">
                                            <asp:TextBox ID="txt_zoomuserid" runat="server" CssClass="form-control"> </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="position-relative form-group">
                                        <label>Zoom Password<sup>*</sup></label>
                                        <div class="input-group input-group-icon">
                                            <asp:TextBox ID="txt_pwd" runat="server" CssClass="form-control"> </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="card-footer col-md-12">
                                            
                                            <div class="col-md-4">
                                                <asp:Button ID="btn_submit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btn_submit_Click" />

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-8">
                    <div class="main-card card">
                        <div class="card-body">
                            <%--<h5 class="card-title">All Added Teacher </h5>--%>
                            <asp:HiddenField ID="hdid" runat="server" />
                            <asp:HiddenField ID="hdpwd" runat="server" />
                            <asp:HiddenField ID="hd_UserID" runat="server" />



                            <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                                <thead>
                                    <tr>
                                        <th>Sl. No.</th>
                                        <th>Super admin</th>
                                        <th>Zoom User Id</th>
                                        <th>Password</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RPDetails" runat="server" >
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lbl_Name" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblzoomuserid" runat="server" Text='<%#Bind("User_ID") %>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_Password" runat="server" Text='<%#Bind("pwd") %>'></asp:Label>
                                                </td>


                                                <td>
                                                    <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                        <div class="btn-group dropdown">
                                                            <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                                <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                            </button>
                                                            <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">
                                                                <asp:LinkButton ID="lnk_edit" runat="server" CssClass="dropdown-item" OnClick="lnk_edit_Click"><span>Edit</span></asp:LinkButton>



                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>




    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
