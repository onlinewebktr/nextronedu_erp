<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Send_Teacher_Message.aspx.cs" Inherits="school_web.LMS_VC_Admin.Send_Teacher_Message" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Send Teacher Message
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999!important;
        }

        .calender-icon {
            margin: 0px 0px 0px 0px;
            position: relative;
        }

        .clndr-icon {
            font-size: 16px !important;
            color: #ff2956;
            position: absolute;
            top: 33px;
            right: 10px;
        }

        .notificationpan {
            display: none;
            top: 66px!important;
            right: 10px!important;
            padding: 10px 10px;
            width: 337px;
            height: auto;
            border: 1px solid rgb(162, 162, 162);
            box-shadow: 2px 7px 19px -2px rgb(154 154 154 / 80%);
        }
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="fa fa-envelope icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server"> Send Message to Teacher</asp:Literal>

                    </div>
                </div>
                <div class="page-title-actions">
                </div>
            </div>
        </div>
        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 100%; height: auto;">
                    <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
                <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                    class="closenotificationpan" alt="" />
            </div>
        </div>
        <div class="row">
            <div class="col-lg-8">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">Send Message</h5>
                        <div class="form-row">
                            <div class="col-md-12">
                                <div class="position-relative form-group">


                                    <div class="position-relative form-group">

                                        <label>Teacher List<sup>*</sup></label>
                                        <div class="input-group input-group-icon" style="width: 32%;">
                                            <asp:DropDownList ID="ddl_teacher" class="form-control" runat="server" Style="width: 100%!important"></asp:DropDownList>
                                        </div>


                                    </div>


                                    <div class="position-relative form-group">

                                        <label>Subject<sup>*</sup></label>
                                        <div class="input-group input-group-icon">
                                            <asp:TextBox ID="txt_notice_subject" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="position-relative form-group">
                                        <label>Details<sup>*</sup></label>
                                        <div class="input-group">
                                            <asp:TextBox ID="txt_notice_details" runat="server" CssClass="form-control" TextMode="MultiLine" Height="150px"></asp:TextBox>


                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="form-group">
                                        <label>Choose File (<sup> jpg,png,pdf fil size 500kb </sup>)</label>
                                        <div class="input-group input-group-icon">
                                            <asp:FileUpload ID="fl_Photo" runat="server" />
                                            <asp:HiddenField ID="Hd_Photo" runat="server" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                                runat="server" ControlToValidate="fl_Photo"
                                                ErrorMessage="Invalid File. Please upload a File with extension: png, jpg, jpeg" ForeColor="Red"
                                                ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.jpeg|JPG|.JPEG|.PNG|.pdf|.PDF)$"
                                                ValidationGroup="A" SetFocusOnError="true" Display="Dynamic" CssClass="error"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="card-footer pull-right">
                                        <asp:Button ID="btn_submit" runat="server" Text="Send" CssClass="btn btn-primary" OnClick="btn_submit_Click" />
                                    </div>
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
