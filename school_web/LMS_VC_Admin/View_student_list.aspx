<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="View_student_list.aspx.cs" Inherits="school_web.LMS_VC_Admin.View_student_list" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    View Student List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .dt-button-collection {
            margin-top: -59.4px !important;
        }
    </style>
    <style>
        .highlightRow {
            background-color: #c5c5ff !important;
        }
    </style>

    <script>
        $(function () {
            $(".trclass").click(function () {
                $(this).addClass("highlightRow").siblings().removeClass("highlightRow");
            });
        });
    </script>
    <style>
        .buttons-print {
            display: none !important;
        }

        .buttons-excel {
            display: none !important;
        }
         .buttons-collection {
            display: none !important;
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
                        <asp:Literal ID="ltUsertop" runat="server">View Student List</asp:Literal>

                    </div>
                </div>
                <div class="page-title-actions">
                    <a href="Add_student.aspx" class="btn-shadow btn btn-info" style="display: none">
                        <span class="btn-icon-wrapper pr-2 opacity-7">
                            <i class="pe-7s-plus fa-w-20"></i>
                        </span>
                        Add Student
                    </a>
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
            <div class="card-body">
                <div class="form-row">
                    <div class="col-md-2">
                        <div class="position-relative form-group">
                            <label>Select Class</label>
                            <div class="input-group input-group-icon">
                                <asp:DropDownList ID="ddl_CourseCat" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_CourseCat_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="position-relative form-group">
                            <label>Student Section Wise.</label>
                            <div class="input-group input-group-icon">
                                <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:TextBox ID="txt_student_regid" runat="server" CssClass="form-control" Style="float: left; width: 50%; display: none"></asp:TextBox>
                                <asp:Button ID="btn_section_wise" runat="server" CssClass="btn btn-primary" Text="Find" OnClick="btn_section_wise_Click" Style="float: left; width: auto; margin: 5px 0px 0px 13px;" />
                                <asp:Button ID="btn_find_dtudent_regid" runat="server" CssClass="btn btn-primary" Visible="false" Text="Find" OnClick="btn_find_dtudent_regid_Click" Style="float: left; width: auto; margin: 5px 0px 0px 13px;" />
                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Visible="true" Text="Reset" OnClick="Button1_Click" Style="float: left; width: auto; margin: 5px 0px 0px 13px;" />
                            </div>
                        </div>

                    </div>
                    <div class="col-md-2" style="display: none">
                        <div class="position-relative form-group">
                            <label>Select Class</label>
                            <div class="input-group input-group-icon">
                                <asp:DropDownList ID="ddl_CourseCat1" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2" style="display: none">
                        <asp:Button ID="btn_clear" runat="server" CssClass="btn btn-danger" Text="Delete Data" Style="margin-top: 30px" OnClick="btn_clear_Click" OnClientClick="return confirm('Are you sure want to delete data ?')" />
                    </div>

                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl. No.</th>
                                    <th>Student Name</th>

                                    <th style="width: 111px;">Admission No.</th>

                                    <th>Class</th>
                                    <th>Roll No.</th>
                                    <th>Section</th>
                                    <th>Session</th>
                                    <th>Gender</th>
                                    <th>Mobile No.</th>
                                    <th>Password</th>
                                    <th>Status</th>
                                    <th>App Version</th>
                                    <th>Edit Status</th>
                                    <th>Action</th>
                                    <th class="allCheckbox" style="width: 86px;">
                                        <asp:CheckBox ID="hdrChkBox" runat="server" />Check All
                                    </th>

                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
                                    <ItemTemplate>
                                        <tr class="trclass" id="row" runat="server">
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbl_studentname" runat="server" Font-Names="Arial" Text='<%#Bind("studentname") %>'></asp:Label>
                                            </td>


                                            <td>
                                                <asp:Label ID="lbl_admissionserialnumber" runat="server" Font-Names="Arial" Text='<%#Bind("admissionserialnumber") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_class" runat="server" Font-Names="Arial" Text='<%#Bind("class") %>'></asp:Label>
                                                <asp:Label ID="lbl_Class_id" runat="server" Font-Names="Arial" Text='<%#Bind("Class_id") %>' Visible="false"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbl_rollnumber" runat="server" Font-Names="Arial" Text='<%#Bind("rollnumber") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Section" runat="server" Font-Names="Arial" Text='<%#Bind("Section") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_session" runat="server" Font-Names="Arial" Text='<%#Bind("session") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_gender" runat="server" Font-Names="Arial" Text='<%#Bind("gender") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_mobile" runat="server" Font-Names="Arial" Style="word-break: break-all" Text='<%#Bind("mobilenumber") %>'></asp:Label>

                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_Password" runat="server" Font-Names="Arial" Text='<%#Bind("Pwd") %>'></asp:Label>
                                                <asp:Label ID="lbl_id" runat="server" Font-Names="Arial" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_status" runat="server"></asp:Label>
                                                <asp:Label ID="lbl_iStatus" runat="server" Font-Names="Arial" Text='<%#Bind("Status") %>' Visible="false"></asp:Label>

                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_app_version" runat="server" Font-Names="Arial" Text='<%#Bind("Version_name") %>'></asp:Label>

                                            </td>


                                            <td>
                                                <asp:Label ID="lbl_edit_status" runat="server"></asp:Label>
                                                <asp:Label ID="lbl_edit_Istatus" runat="server" Font-Names="Arial" Text='<%#Bind("Edit_Istatus") %>' Visible="false"></asp:Label>

                                            </td>
                                            <td>
                                                <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                    <div class="btn-group dropdown">
                                                        <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                            <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                        </button>
                                                        <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">

                                                            <asp:LinkButton ID="lnk_Delete" Visible="false" runat="server" CssClass="dropdown-item" OnClick="lnk_Delete_Click" OnClientClick='return confirm("Are you sure want to delete ?")'><i class="dropdown-icon lnr-trash"></i><span>Delete</span></asp:LinkButton>
                                                            <asp:LinkButton ID="lnk_edit_permission" runat="server" CssClass="dropdown-item" OnClick="lnk_edit_permission_Click" OnClientClick='return confirm("Are you sure want to grant edit permission?")'><i class="dropdown-icon lnr lnr-pencil"></i><span> Edit Permission</span></asp:LinkButton>


                                                            <asp:LinkButton ID="btn_active" Visible="false" runat="server" Style="display: none;" CssClass="dropdown-item" OnClick="btn_active_Click"></asp:LinkButton>
                                                            <a id="popup1" href='Student_Full_Details.aspx?regid=<%#Eval("admissionserialnumber")%>' style="color: #000; display: none" target="_blank" class="dropdown-item"><i class="dropdown-icon lnr lnr-eye"></i><span>View</span></a>
                                                            <asp:Label ID="lbl_dateofadmission" runat="server" Font-Names="Arial" Text='<%#Bind("dateofadmission") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_verification_status" runat="server" Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_verification_istatus" runat="server" Font-Names="Arial" Text='<%#Bind("Verification_Istatus") %>' Visible="false"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td class="singleCheckbox" style="width: 43px!important;">
                                                <asp:CheckBox ID="rowChkBox" runat="server" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td style="text-align: right" colspan="17">
                                        <asp:Button ID="btn_Clear_Login" runat="server" class="btn btn-primary" Text="Edit All" OnClick="btn_Clear_Login_Click" />
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
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
