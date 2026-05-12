<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Send-userid-password-to-student.aspx.cs" Inherits="school_web.LMS_VC_Admin.Send_userid_password_to_student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Send User Id & Password to Student
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
        .dt-button-collection {
            margin-top: 9.6px!important;
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
                        <asp:Literal ID="ltUsertop" runat="server">Send User Id & Password to Student</asp:Literal>

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
        <div class="row">
            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <div class="form-row">
                            <asp:HiddenField ID="hdid" runat="server" />
                            <asp:HiddenField ID="hdpwd" runat="server" />
                            <asp:HiddenField ID="hd_UserID" runat="server" />
                            <div class="find-sec" style="padding: 0px 0px; margin: 5px 0px -2px 0px; background: #c3ffc5;">
                                <div class="row">
                                    <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
                                        <div class="find-inr-bg">
                                            <div class="row">
                                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12" style="padding-right: 0px">
                                                    <p class="find-para">Class<sup>*</sup></p>
                                                </div>

                                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                    <div class="find-txtbx-wpr">
                                                        <asp:DropDownList ID="ddl_find_class" runat="server" class="form-control" OnSelectedIndexChanged="ddl_find_class_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                    <div class="find-txtbx-wpr">
                                                        <asp:DropDownList ID="ddl_section" runat="server" class="form-control" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                        <div class="find-inr-bg">
                                            <asp:Label ID="lbl_total_student" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="find-sec" style="padding: 0px 0px;">
                                <div class="row">
                                    <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
                                        <div class="find-inr-bg" style="background: #a4e2ff;">
                                            <div class="row">
                                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12" style="padding-right: 0px">
                                                    <p class="find-para">Student Name<sup>*</sup></p>
                                                </div>

                                                <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
                                                    <div class="find-txtbx-wpr">
                                                        <asp:TextBox ID="tt_student_name" runat="server" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-lg-2 col-md-2 col-sm-4 col-xs-4" style="padding-left: 0px;">
                                                    <div class="find-txtbx-btn-sec">
                                                        <asp:Button ID="btn_fnd_by_student_name" class="btn btn-primary" OnClick="btn_fnd_by_student_name_Click" runat="server" Style="font-size: 12px;" Text="Find" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>



                                    <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
                                        <div class="find-inr-bg">
                                            <div class="row">
                                                <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
                                                    <p class="find-para">Student Mobile<sup>*</sup></p>
                                                </div>

                                                <div class="col-lg-5 col-md-5 col-sm-8 col-xs-8">
                                                    <div class="find-txtbx-wpr">
                                                        <asp:TextBox ID="txt_student_mobile" runat="server" class="form-control"></asp:TextBox>
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
                            </div>
                            <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                                <thead>
                                    <tr>
                                        <th>Sl No.</th>
                                        <th class="allCheckbox">
                                            <asp:CheckBox ID="hdrChkBox" runat="server" />
                                        </th>
                                        <th>Student Name</th>
                                        <th>Class</th>
                                        <th>Session</th>
                                        <th>Section</th>
                                        <th>Roll</th>
                                        <th>Father's Name</th>
                                        <th>Mobile No.</th>
                                        <th>User Id</th>
                                        <th>Password</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RPDetails" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                                <td class="singleCheckbox" style="width: 43px!important;">
                                                    <asp:CheckBox ID="rowChkBox" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_studentname" runat="server" Font-Names="Arial" Text='<%#Bind("studentname") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_class" runat="server" Font-Names="Arial" Text='<%#Bind("class") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_session" runat="server" Font-Names="Arial" Text='<%#Bind("session") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_Section" runat="server" Font-Names="Arial" Text='<%#Bind("Section") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_rollnumber" runat="server" Font-Names="Arial" Text='<%#Bind("rollnumber") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_fathername" runat="server" Font-Names="Arial" Text='<%#Bind("fathername") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_mobile" runat="server" Font-Names="Arial" Text='<%#Bind("mobilenumber") %>'></asp:Label>
                                                    <asp:Label ID="lbl_id" runat="server" Font-Names="Arial" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label ID="lbl_userid" runat="server" Font-Names="Arial" Text='<%#Bind("admissionserialnumber") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_password" runat="server" Font-Names="Arial" Text='<%#Bind("Pwd") %>'></asp:Label>
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                            <div class="card-footer">
                                   <div style="margin: 9px 0px 0px 0px; float: right; width: 100%;">
                            <asp:Button ID="btn_send_message"  runat="server" class="btn btn-primary" Text="Send Message" OnClick="btn_send_message_Click" /></div>
                                </div>
                        
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
