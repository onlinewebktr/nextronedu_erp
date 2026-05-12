<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Send-message-to-teacher.aspx.cs" Inherits="school_web.LMS_VC_Admin.Send_message_to_teacher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Send Message  to Teacher
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .find-sec {
            margin: 5px 0px 5px 0px;
            padding: 5px 10px;
            width: 100%;
            height: auto;
            float: left;
            background: #c3eafc;
            border-radius: 2px;
            box-shadow: 0 3px 3px 0 rgba(0, 0, 0, 0.14), 0 1px 7px 0 rgba(0, 0, 0, 0.12), 0 3px 1px -1px rgba(0, 0, 0, 0.2);
        }

        .find-inr-bg {
            margin: 0px 0px 0px 0px;
            padding: 5px 7px;
            width: 100%;
            height: auto;
            float: left;
        }

        .panel-body {
            padding: 0px;
        }
    </style>
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
                        <asp:Literal ID="ltUsertop" runat="server"> Send Message  to Teacher</asp:Literal>

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
        <div class="row">
            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <asp:HiddenField ID="hdid" runat="server" />
                        <asp:HiddenField ID="hdpwd" runat="server" />
                        <asp:HiddenField ID="hd_UserID" runat="server" />
                        <div class="find-sec" style="padding: 0px 0px;">
                            <div class="row">
                                <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
                                    <div class="find-inr-bg" style="background: #a4e2ff;">
                                        <div class="row">
                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12" style="padding-right: 0px">
                                                <p class="find-para">Teacher Name<sup>*</sup></p>
                                            </div>

                                            <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
                                                <div class="find-txtbx-wpr">
                                                    <asp:TextBox ID="txt_teacher_name" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-lg-3 col-md-3 col-sm-4 col-xs-4">
                                                <div class="find-txtbx-btn-sec">
                                                    <asp:Button ID="btn_find_by_teacher_name" class="btn btn-primary" OnClick="btn_find_by_teacher_name_Click" runat="server" Style="font-size: 12px;" Text="Find" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
                                    <div class="find-inr-bg">
                                        <div class="row">
                                            <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
                                                <p class="find-para">Teacher Mobile<sup>*</sup></p>
                                            </div>

                                            <div class="col-lg-5 col-md-5 col-sm-8 col-xs-8">
                                                <div class="find-txtbx-wpr">
                                                    <asp:TextBox ID="txt_teacher_mobile" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4" style="padding-left: 0px;">
                                                <div class="find-txtbx-btn-sec">
                                                    <asp:Button ID="btn_find_by_mobile" class="btn btn-primary" OnClick="btn_find_by_mobile_Click" runat="server" Style="font-size: 12px;" Text="Find" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-1 col-md-1 col-sm-12 col-xs-12">
                                    <div class="find-inr-bg">
                                        <div class="find-txtbx-btn-sec">
                                            <asp:Button ID="btn_show_all" class="btn btn-primary" OnClick="btn_show_all_Click" runat="server" Style="font-size: 12px;" Text="Find All" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl No.</th>
                                    <th class="allCheckbox">
                                        <asp:CheckBox ID="hdrChkBox" runat="server" />
                                    </th>
                                    <th>Name</th>
                                    <th>User ID</th>
                                    <th>Password</th>
                                    
                                    <th>Phone No</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                            <td class="singleCheckbox">
                                                <asp:CheckBox ID="rowChkBox" runat="server" />
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_Name" runat="server" Text='<%#Bind("name") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_UserID" runat="server" Text='<%#Bind("user_id") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Password" runat="server" Text='<%#Bind("password") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_PhoneNo" runat="server" Text='<%#Bind("mobile") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>


                        <asp:Button ID="btn_send_message" Style="margin: 10px 0px 0px 0px;" runat="server" class="btn btn-primary" Text="Send Message" OnClick="btn_send_message_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            var $allCheckbox = $('.allCheckbox :checkbox');
            var $checkboxes = $('.singleCheckbox :checkbox');
            $allCheckbox.change(function () {
                if ($allCheckbox.is(':checked')) {
                    $checkboxes.attr('checked', 'checked');
                }
                else {
                    $checkboxes.removeAttr('checked');
                }
            });
            $checkboxes.change(function () {
                if ($checkboxes.not(':checked').length) {
                    $allCheckbox.removeAttr('checked');
                }
                else {
                    $allCheckbox.attr('checked', 'checked');
                }
            });
        });
    </script>
    <script type="text/javascript" language="javascript">
        function checkAllRows(obj) {

            var objGridview = obj.parentNode.parentNode.parentNode;
            var list = objGridview.getElementsByTagName("input");

            for (var i = 0; i < list.length; i++) {
                var objRow = list[i].parentNode.parentNode;
                if (list[i].type == "checkbox" && obj != list[i]) {
                    if (obj.checked) {

                        //If the header checkbox is checked then check all 
                        //checkboxes and highlight all rows.

                        objRow.style.backgroundColor = "#99E5E5";
                        list[i].checked = true;
                    }
                    else {
                        objRow.style.backgroundColor = "#FFFFFF";
                        list[i].checked = false;
                    }
                }
            }
        }

        function checkUncheckHeaderCheckBox(obj) {
            var objRow = obj.parentNode.parentNode;

            if (obj.checked) {
                objRow.style.backgroundColor = "#99E5E5";
            }
            else {
                objRow.style.backgroundColor = "#FFFFFF";
            }
            var objGridView = objRow.parentNode;

            //Get all input elements in Gridview
            var list = objGridView.getElementsByTagName("input");
            for (var i = 0; i < list.length; i++) {
                var objHeaderChkBox = list[0];

                //Based on all or none checkboxes are checked check/uncheck Header Checkbox
                var checked = true;

                if (list[i].type == "checkbox" && list[i] != objHeaderChkBox) {
                    if (!list[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            objHeaderChkBox.checked = checked;
        }
    </script>
</asp:Content>
